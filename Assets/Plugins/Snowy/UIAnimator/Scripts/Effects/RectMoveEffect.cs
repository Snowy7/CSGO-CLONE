using System.Collections;
using UnityEngine;

namespace Snowy.UI.Effects.Effects
{
    public class RectMoveEffect : SnEffect
    {
        [SerializeField] private Vector2 to;
        [SerializeField] float duration = 0.1f;
        [SerializeField] private bool forceFrom;
        [SerializeField, ShowIf(nameof(forceFrom), true)] private Vector2 fFrom;
        
        private RectTransform m_rectTransform;
        private Vector2 m_originalPosition;
        
        public override void Initialize(IEffectsManager manager)
        {
            base.Initialize(manager);

            m_rectTransform = manager.Transform as RectTransform;
            if (m_rectTransform == null)
            {
                Debug.LogError("RectMoveEffect: Transform is not a RectTransform");
                return;
            }
            m_originalPosition = m_rectTransform.anchoredPosition;
        }

        public override IEnumerator Apply(IEffectsManager manager)
        {
            if (m_rectTransform == null)
            {
                Debug.LogError("RectMoveEffect: RectTransform is null");
                yield break;
            }
            
            IsPlaying = true;
            if (forceFrom)
                m_rectTransform.anchoredPosition = fFrom;
            
            Vector2 from = m_rectTransform.anchoredPosition; 
            Vector2 dest = from + to;
            float time = 0f;
            while (time < duration)
            {
                m_rectTransform.anchoredPosition = Vector2.Lerp(from, dest, time / duration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            
            m_rectTransform.anchoredPosition = dest;
            IsPlaying = false;
        }

        public override IEnumerator Cancel(IEffectsManager manager)
        {
            // go back to the original position
            IsPlaying = true;
            Vector3 from = m_rectTransform.anchoredPosition;
            float time = 0f;
            while (time < duration)
            {
                m_rectTransform.anchoredPosition = Vector3.Lerp(from, m_originalPosition, time / duration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            
            m_rectTransform.anchoredPosition = m_originalPosition;
            IsPlaying = false;
        }

        public override void ImmediateCancel(IEffectsManager manager)
        {
            m_rectTransform.anchoredPosition = m_originalPosition;
        }
    }
}