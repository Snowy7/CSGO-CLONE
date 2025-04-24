using System.Collections;
using System.Threading.Tasks;
using Mirror;
using Networking;
using SnNotification;
using Snowy.Utils;
using SnTerminal;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Networking
{
    public class SteamLobby : MonoSingleton<SteamLobby>
    {
        public override bool DestroyOnLoad => false;

        // Callbacks
        protected Callback<LobbyCreated_t> LobbyCreated;
        protected Callback<GameLobbyJoinRequested_t> JoinRequested;
        protected Callback<LobbyEnter_t> LobbyEnter;
        protected Callback<LobbyChatUpdate_t> LobbyChatUpdate;
        protected Callback<LobbyChatMsg_t> LobbyChatMsg;
        protected Callback<LobbyDataUpdate_t> LobbyDataUpdate;
        protected Callback<LobbyInvite_t> LobbyInvite;
        protected Callback<GameRichPresenceJoinRequested_t> RichPresenceJoinRequested;
        protected Callback<LobbyKicked_t> LobbyKicked;
        protected Callback<LobbyMatchList_t> LobbyMatchList;

        // Public Variables
        public bool IsInLobby => currentLobbyId != 0;
        public ulong CurrentLobbyId => currentLobbyId;

        // Private Variables
        private ulong currentLobbyId;
        private const string HostAddressKey = "HostAddress";
        private const string Key = "TheCubeLobby";
        private const string Value = "v0.1";

        private SteamNetworkManager networkManager;

        public UnityEvent OnJoinSuccess;
        public UnityEvent OnJoinFailed;
        public UnityEvent<int, string, string> OnMessageReceived;
        
        private bool autoMatchmaking = false;

        # region Unity Callbacks

        private void Start()
        {
            if (!SteamManager.Initialized) return;

            networkManager = GetComponent<SteamNetworkManager>();

            LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
            JoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequested);
            LobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
            LobbyInvite = Callback<LobbyInvite_t>.Create(OnLobbyInvite);
            LobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMsg);
            LobbyKicked = Callback<LobbyKicked_t>.Create(OnLobbyKicked);
            LobbyDataUpdate = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdate);
            RichPresenceJoinRequested = Callback<GameRichPresenceJoinRequested_t>.Create(OnRichPresenceJoinRequested);
            LobbyChatUpdate = Callback<LobbyChatUpdate_t>.Create(OnLobbyChatUpdate);
            LobbyMatchList = Callback<LobbyMatchList_t>.Create(OnLobbyMatchList);
        }

        private void OnApplicationQuit()
        {
            LeaveLobby();
        }

        # endregion

        # region Events

        private void OnLobbyCreated(LobbyCreated_t pCallback)
        {
            if (pCallback.m_eResult != EResult.k_EResultOK) return;

            Debug.Log($"Lobby created: {pCallback.m_ulSteamIDLobby}");
            networkManager.StartHost();

            SteamMatchmaking.SetLobbyData(new CSteamID(pCallback.m_ulSteamIDLobby), HostAddressKey,
                SteamUser.GetSteamID().ToString());
            SteamMatchmaking.SetLobbyData(new CSteamID(pCallback.m_ulSteamIDLobby), Key, Value);
            SteamMatchmaking.SetLobbyData(new CSteamID(pCallback.m_ulSteamIDLobby), "name",
                (SteamFriends.GetPersonaName() ?? "Player") + "'s Lobby");
            
            // allows invites
            SteamMatchmaking.SetLobbyType(new CSteamID(pCallback.m_ulSteamIDLobby), ELobbyType.k_ELobbyTypeFriendsOnly);
            SteamFriends.SetRichPresence("status", "In a lobby");
            SteamFriends.ActivateGameOverlayInviteDialog(new CSteamID(pCallback.m_ulSteamIDLobby));
            SteamFriends.SetRichPresence("connect", $"lobby:{pCallback.m_ulSteamIDLobby}");
            SteamFriends.SetRichPresence("steam_display", "lobby");
            

            currentLobbyId = pCallback.m_ulSteamIDLobby;
            
            // load the Level scene
            networkManager.ServerChangeScene("Level");
        }

        private void OnJoinRequested(GameLobbyJoinRequested_t pCallback)
        {
            Debug.Log($"Join requested: {pCallback.m_steamIDLobby}");
            StopAllCoroutines();
            StartCoroutine(JoinLobbyAfterLeaving(pCallback.m_steamIDLobby));
        }

        private void OnLobbyEntered(LobbyEnter_t pCallback)
        {
            if (pCallback.m_EChatRoomEnterResponse != 1)
            {
                Debug.Log($"Failed to enter lobby: {pCallback.m_ulSteamIDLobby}");
                OnJoinFailed.Invoke();
                if (LoadingPanel.Instance)
                {
                    LoadingPanel.Instance.Hide();
                }

                return;
            }

            Debug.Log($"Lobby entered: {pCallback.m_ulSteamIDLobby}");
            currentLobbyId = pCallback.m_ulSteamIDLobby;

            if (NetworkServer.active)
            {
                OnJoinSuccess.Invoke();
                return;
            }
            
            Debug.Log("Starting client");

            networkManager.networkAddress =
                SteamMatchmaking.GetLobbyData(new CSteamID(pCallback.m_ulSteamIDLobby), HostAddressKey);
            networkManager.StartClient();

            if (string.IsNullOrEmpty(networkManager.networkAddress))
            {
                OnJoinFailed.Invoke();
            }
            
            OnJoinSuccess.Invoke();
        }

        private void OnLobbyInvite(LobbyInvite_t pCallback)
        {
            var friend = SteamFriends.GetFriendPersonaName(new CSteamID(pCallback.m_ulSteamIDUser));
            var lobby = new CSteamID(pCallback.m_ulSteamIDLobby);

            Debug.Log($"Lobby invite: {pCallback.m_ulSteamIDLobby}");
            
            // todo: show invite dialog
            SnNotManager.ShowNotification(NotificationTypeNames.TopLeftModal, new SnNotData()
            {
                title = $"{friend} has invited you to their lobby",
                duration = 10,
                buttons = new[]
                {
                    new SnButtonData()
                    {
                        text = "Accept",
                        onClick = () => OnInviteAccepted(lobby),
                        closeOnClick = true
                    },
                    new SnButtonData()
                    {
                        text = "Decline",
                        onClick = () => OnInviteDeclined(lobby),
                        closeOnClick = true
                    }
                },
            });
        }
        
        
        private void OnLobbyChatMsg(LobbyChatMsg_t pCallback)
        {
            byte[] data = new byte[4096];
            int ret = SteamMatchmaking.GetLobbyChatEntry(new CSteamID(pCallback.m_ulSteamIDLobby),
                (int)pCallback.m_iChatID,
                out _, data, data.Length, out _);

            if (ret > 0)
            {
                string encoded = System.Text.Encoding.UTF8.GetString(data);
                var split = encoded.Split(':');
                var message = split[1];
                var type = int.Parse(split[0]);
                var senderName = SteamFriends.GetFriendPersonaName(new CSteamID(pCallback.m_ulSteamIDUser));
                
                OnMessageReceived.Invoke(type, senderName, message);
            }

            Debug.Log($"Lobby chat message: {pCallback.m_ulSteamIDLobby}");
            Debug.Log($"Message: {System.Text.Encoding.UTF8.GetString(data)}");
            Debug.Log($"Message Length: {ret}");
        }
        
        private void OnLobbyKicked(LobbyKicked_t pCallback)
        {
            Debug.Log($"Kicked from lobby: {pCallback.m_ulSteamIDLobby}");
            currentLobbyId = 0;
            networkManager.StopHost();
            SteamFriends.SetRichPresence("status", "In the menu");
            SteamFriends.SetRichPresence("connect", "");
        }
        
        private void OnLobbyDataUpdate(LobbyDataUpdate_t pCallback)
        {
            Debug.Log($"Lobby data updated: {pCallback.m_ulSteamIDLobby}");
            if (pCallback.m_bSuccess == 1)
            {
                Debug.Log($"Lobby data updated successfully: {pCallback.m_ulSteamIDLobby}");
            }
            else
            {
                Debug.Log($"Failed to update lobby data: {pCallback.m_ulSteamIDLobby}");
            }
        }
        
        private void OnRichPresenceJoinRequested(GameRichPresenceJoinRequested_t pCallback)
        {
            Debug.Log($"Rich presence join requested: {pCallback.m_steamIDFriend}");
            StopAllCoroutines();
            StartCoroutine(JoinLobbyAfterLeaving(pCallback.m_steamIDFriend));
        }
        
        private void OnLobbyChatUpdate(LobbyChatUpdate_t pCallback)
        {
            Debug.Log($"Lobby chat update: {pCallback.m_ulSteamIDLobby}");
            if (pCallback.m_rgfChatMemberStateChange == 1)
            {
                Debug.Log($"User joined lobby: {pCallback.m_ulSteamIDUserChanged}");
            }
            else if (pCallback.m_rgfChatMemberStateChange == 2)
            {
                Debug.Log($"User left lobby: {pCallback.m_ulSteamIDUserChanged}");
            }
        }
        
        private void OnLobbyMatchList(LobbyMatchList_t pCallback)
        {
            // if auto matchmaking is enabled, join the first lobby
            if (autoMatchmaking)
            {
                AutoMatch(pCallback.m_nLobbiesMatching);
            }
        }

        # endregion

        # region Private Methods

        IEnumerator JoinLobbyAfterLeaving(CSteamID lobbyId)
        {
            if (currentLobbyId != 0 || NetworkServer.active || NetworkClient.active)
            {
                LeaveLobby(false);
            }

            yield return new WaitUntil(() => currentLobbyId == 0 && !NetworkServer.active && !NetworkClient.active);
            // wait a second
            yield return new WaitForSeconds(1);
            JoinLobby(lobbyId);
        }

        private void OnInviteAccepted(CSteamID lobbyID)
        {
            Debug.Log($"Accepted invite to lobby: {lobbyID}");
            // if in a lobby, leave it first
            if (currentLobbyId != 0)
            {
                StopAllCoroutines();
                StartCoroutine(JoinLobbyAfterLeaving(lobbyID));
            } 
            else
            {
                JoinLobby(lobbyID);
            }
        }

        private void OnInviteDeclined(CSteamID lobbyID)
        {
            Debug.Log($"Declined invite to lobby: {lobbyID}");
            // Hide the notification
        }

        private void AutoMatch(uint lobbiesAmount)
        {
              
            // Wait for the lobby list to be populated
            
            var lobby = SteamMatchmaking.GetLobbyByIndex(0);
            
            if (lobby.m_SteamID != 0)
            {
                Debug.Log($"Found lobby: {lobby.m_SteamID}");
                JoinLobby(new CSteamID(lobby.m_SteamID));
            }
            else
            {
                // Create a new lobby
                Debug.Log("No lobby found, creating a new one");
                CreateLobby();
            }
        }

        # endregion

        # region Public Methods

        private async Task ShowLoadingPanel()
        {
            if (LoadingPanel.Instance)
            {
                LoadingPanel.Instance.Show("");
                await LoadingPanel.Instance.LoadBlackScreen(0);
            }
            
            await Task.Delay(500);
        }

        public async void CreateLobby()
        {
            await ShowLoadingPanel();
            Debug.Log("Creating lobby");
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
        }

        public async void JoinLobby(CSteamID lobby)
        {
            await ShowLoadingPanel();
            
            SteamMatchmaking.JoinLobby(lobby);
        }
        
        public async void AutoMatchmaking() 
        {
            await ShowLoadingPanel();
            Debug.Log("Auto matchmaking");
            autoMatchmaking = true;
            SteamMatchmaking.AddRequestLobbyListStringFilter(Key, Value, ELobbyComparison.k_ELobbyComparisonEqual);
            SteamMatchmaking.AddRequestLobbyListResultCountFilter(1);
            SteamMatchmaking.RequestLobbyList();
        }

        public void LeaveLobby(bool loadMenu = true)
        {
            if (currentLobbyId != 0)
            {
                // destroy the game manager
                if (SteamNetworkManager.Instance) SteamNetworkManager.Instance.Disconnect(currentLobbyId, loadMenu);
                else SteamMatchmaking.LeaveLobby(new CSteamID(currentLobbyId));
                currentLobbyId = 0;
            }
        }

        public bool SendChatMessage(int id = 2, string msg = "")
        {
            if (currentLobbyId == 0)
            {
                Debug.LogWarning("Not in a lobby");
                return false;
            }

            CSteamID lobbyID = new CSteamID(currentLobbyId);
            byte[] pvMessage = System.Text.Encoding.UTF8.GetBytes($"{id}:{msg}");
            return SteamMatchmaking.SendLobbyChatMsg(lobbyID, pvMessage, pvMessage.Length);
        }

        # endregion
        
        # region Commands
        
        [RegisterCommand(Name = "create_lobby", Help = "Create a lobby", MinArgCount = 0, MaxArgCount = 0)]
        public static void CommandCreateLobby(CommandArg[] args)
        {
            SteamLobby.Instance.CreateLobby();
        }

        [RegisterCommand(Name = "join_lobby", Help = "Join a lobby", MinArgCount = 1, MaxArgCount = 1,
            Hint = "<lobby_id>")]
        public static void CommandJoinLobby(CommandArg[] args)
        {
            if (args.Length == 0)
            {
                Terminal.Log("Usage: join_lobby <lobby_id>");
                return;
            }

            if (!ulong.TryParse(args[0].String, out ulong lobbyId))
            {
                Terminal.Log("Invalid lobby id");
                return;
            }

            SteamLobby.Instance.JoinLobby(new CSteamID(lobbyId));
        }

        [RegisterCommand(Name = "leave_lobby", Help = "Leave the current lobby", MinArgCount = 0, MaxArgCount = 0)]
        public static void CommandLeaveLobby(CommandArg[] args)
        {
            SteamLobby.Instance.LeaveLobby();
        }
        
        # endregion
    }
}