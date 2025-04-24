using System.Collections;
using Networking;
using Snowy.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class Init : MonoBehaviour
    {
        private void Start()
        {
            LoadingPanel.Instance.Show();
            StartCoroutine(WaitForServices());
        }
        
        IEnumerator WaitForServices()
        {
            yield return new WaitUntil(() => 
                SteamNetworkManager.Instance?.IsReady == true
            );
            
            // 2 seconds delay to show loading screen
            yield return new WaitForSeconds(2f);
            
            LoadingPanel.Instance.Hide();
            
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }
    }
}