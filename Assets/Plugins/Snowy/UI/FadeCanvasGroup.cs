using System.Collections;
using UnityEngine;
using Utils.Attributes;

namespace Snowy.UI
{
    enum FadeDirection
    {
        In,
        Out
    }
    
    [InspectorHeader("Fade Canvas Group")]
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeCanvasGroup : MonoBehaviour
    {
        [Title("References")]
        [SerializeField, NotNull] private CanvasGroup fadeGroup;
        
        [Title("Settings")]
        [SerializeField, Min(0)] private float fadeSpeed = 2.5f;
        
        private bool m_isFading;
        
        # if UNITY_EDITOR
        
        private void Reset()
        {
            fadeGroup = GetComponent<CanvasGroup>();
        }
        
        void OnValidate()
        {
            if (!fadeGroup)
            {
                fadeGroup = GetComponent<CanvasGroup>();
            }
            
            fadeSpeed = Mathf.Max(0, fadeSpeed);
        }
        
        # endif
        
        public void FadeIn()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(FadeDirection.In));
        }
        
        public void FadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(FadeDirection.Out));
        }
        
        IEnumerator Fade(FadeDirection fadeDirection)
        {
            float startAlpha = fadeDirection == FadeDirection.In ? 0 : 1;
            float endAlpha = fadeDirection == FadeDirection.In ? 1 : 0;
            float currentAlpha;
            
            fadeGroup.blocksRaycasts = true;
            fadeGroup.interactable = true;
            
            float timer = 0;
            while (timer < fadeSpeed)
            {
                timer += Time.deltaTime;
                currentAlpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeSpeed);
                fadeGroup.alpha = currentAlpha;
                yield return null;
            }
            
            fadeGroup.blocksRaycasts = fadeDirection == FadeDirection.In;
            fadeGroup.interactable = fadeDirection == FadeDirection.In;
        }

        public void Hide()
        {
            fadeGroup.alpha = 0;
            fadeGroup.blocksRaycasts = false;
            fadeGroup.interactable = false;
        }
    }
}