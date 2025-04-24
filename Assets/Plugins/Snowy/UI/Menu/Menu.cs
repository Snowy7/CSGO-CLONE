using System.Collections;
using UnityEngine;
using Utils.Attributes;

namespace Snowy.UI
{
    enum MenuType
    {
        Single,
        Multi
    }
    
    [InspectorHeader("Menu")]
    public class Menu : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] private FadeCanvasGroup fadeCanvasGroup;
        
        [Title("Settings")]
        [SerializeField] private MenuType menuType;
        
        public void FadeIn()
        {
            fadeCanvasGroup.FadeIn();
        }

        public void FadeOut()
        {
            fadeCanvasGroup.FadeOut();
        }
        
        public void Hide()
        {
            fadeCanvasGroup.Hide();
        }
    }
}