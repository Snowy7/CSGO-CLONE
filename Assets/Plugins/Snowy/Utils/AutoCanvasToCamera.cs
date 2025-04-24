using UnityEngine;

namespace Utils
{
    public class AutoCanvasToCamera : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private int planeDistance = 100;
        
        private void Start()
        {
            Debug.Log($"Canvas render mode: {canvas.renderMode} | Camera: {canvas.worldCamera} | Main Camera: {Camera.main}");
            
            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                canvas.worldCamera = Camera.main;
                canvas.planeDistance = planeDistance;
            }
        }
        
        private void OnValidate()
        {
            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                canvas.worldCamera = Camera.main;
                canvas.planeDistance = planeDistance;
            }
        }
        
        private void OnEnable()
        {
            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                canvas.worldCamera = Camera.main;
                canvas.planeDistance = planeDistance;
            }
        }
        
        private void OnDisable()
        {
            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                canvas.worldCamera = Camera.main;
                canvas.planeDistance = planeDistance;
            }
        }
    }
}