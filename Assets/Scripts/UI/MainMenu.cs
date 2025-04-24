using Networking;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SteamLobby.Instance.AutoMatchmaking();
        }
    }
}