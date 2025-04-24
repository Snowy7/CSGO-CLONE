using Snowy.Utils;
using UnityEngine;
using Utils.Attributes;

namespace Snowy.UI
{
    [InspectorHeader("Menu Manager")]
    public class MenuManager : MonoSingleton<MenuManager>
    {
        [Title("References")]
        [SerializeField, ReorderableList] private Menu[] menus;
        
        private Menu m_currentMenu;

        private void Start()
        {
            if (menus == null || menus.Length == 0)
                return;
            
            OpenMenu(menus[0]);
        }

        public void OpenMenu(Menu menu)
        {
            if (menus == null || menus.Length == 0)
                return;
            
            if (m_currentMenu == menu) return;
            
            if (m_currentMenu != null)
                m_currentMenu.FadeOut();
            
            foreach (var m in menus)
            {
                if (m == menu)
                    continue;
                
                m.Hide();
            }
            
            m_currentMenu = menu;
            m_currentMenu.FadeIn();
        }
    }
}