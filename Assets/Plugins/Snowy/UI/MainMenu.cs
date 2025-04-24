using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snowy.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] CanvasGroup fadeGroup;
        [SerializeField] float fadeDuration = 1f;
        
        private void Start()
        {
            StartCoroutine(FadeIn());
        }
        
        private IEnumerator FadeIn()
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                fadeGroup.alpha = 1 - timer / fadeDuration;
                timer += Time.deltaTime;
                yield return null;
            }
            fadeGroup.alpha = 0;
            fadeGroup.blocksRaycasts = false;
        }
        
        private IEnumerator FadeOut()
        {
            float timer = 0f;
            fadeGroup.blocksRaycasts = true;
            while (timer < fadeDuration)
            {
                fadeGroup.alpha = timer / fadeDuration;
                timer += Time.deltaTime;
                yield return null;
            }
            fadeGroup.alpha = 1;
        }
        
        public void Play()
        {
            StartCoroutine(FadeOut());
        }
        
        public void Quit()
        {
            # if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            # else
            Application.Quit();
            #endif
        }

        public void Settings()
        {
            // TODO: Implement settings
        }
    }
}