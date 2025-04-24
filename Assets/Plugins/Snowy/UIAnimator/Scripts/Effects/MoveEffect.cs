using System.Collections;
using UnityEngine;

namespace Snowy.UI.Effects.Effects
{
    public class MoveEffect : SnEffect
    {
        [SerializeField] private Vector3 to;
        [SerializeField] float duration = 0.1f;
        [SerializeField] private bool moveFromCurrent;
        [SerializeField] private bool forceFrom;
        [SerializeField, ShowIf(nameof(forceFrom), true)] private Vector3 fFrom;
        
        private Transform m_transform;
        private Vector3 m_originalPosition;
        
        public override void Initialize(IEffectsManager manager)
        {
            base.Initialize(manager);
            
            
            m_transform = customGraphicTarget ? graphicTarget.transform : manager.Transform;
            
            if (m_transform == null)
            {
                Debug.LogError("MoveEffect: Transform is not a RectTransform");
                return;
            }

            m_originalPosition = m_transform.localPosition;
        }

        public override IEnumerator Apply(IEffectsManager manager)
        {
            IsPlaying = true;
            if (forceFrom)
                m_transform.localPosition = fFrom;
            
            Vector3 from = m_transform.localPosition;
            float time = 0f;
            while (time < duration)
            {
                m_transform.localPosition = Vector3.Lerp(from, to, time / duration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            m_transform.localPosition = to;
            IsPlaying = false;
        }

        public override IEnumerator Cancel(IEffectsManager manager)
        {
            // go back to the original position
            IsPlaying = true;
            float time = 0f;
            var from = m_transform.localPosition;
            float dur = duration * Vector3.Distance(from, m_originalPosition) / Vector3.Distance(to, m_originalPosition);
            while (time < dur)
            {
                m_transform.localPosition = Vector3.Lerp(from, m_originalPosition, time / dur);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            m_transform.localPosition = m_originalPosition;
            IsPlaying = false;
        }

        public override void ImmediateCancel(IEffectsManager manager)
        {
            if (m_transform) m_transform.localPosition = m_originalPosition;
        }
    }
}