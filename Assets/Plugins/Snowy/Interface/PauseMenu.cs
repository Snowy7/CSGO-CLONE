using SnInput;
using Snowy.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interface
{
    public class PauseMenu : MonoSingleton<PauseMenu>
    {
        private float m_triggerDelay = 0.2f;
        private float m_triggerTime;

        [Title("References")]
        [SerializeField] private GameObject pauseMenu;
        
        public bool IsActive => pauseMenu.activeSelf;
        
        private void Start()
        {
            InputManager.OnLockCursor += OnLockCursor;
            
            // Disable
            Deactivate();
        }

        private void OnDestroy()
        {
            if (InputManager.Instance)
            {
                InputManager.OnLockCursor -= OnLockCursor;
            }
        }

        private void OnLockCursor(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (Time.time < m_triggerTime + m_triggerDelay) return;
                m_triggerTime = Time.time;
                bool isActive = !pauseMenu.activeSelf;
                if (isActive)
                {
                    Activate();
                }
                else
                {
                    Deactivate();
                }
            }
        }
        
        private void Activate()
        {
            if (!UIManager.CanToggle) return;
            
            pauseMenu.SetActive(true);
            
            // Deactivate the chat if it's active
            // Make sure the cursor is unlocked
            //if (Player.Instance)
            //{
            //    Player.Instance.SetCursorLock(false);
            //}
            
            InputManager.Instance.SwitchToUIControls();
        }

        public void Deactivate()
        {
            if (!UIManager.CanToggle) return;
            
            pauseMenu.SetActive(false);
            // Make sure the cursor is locked
            //if (Player.Instance)
            //{
            //    Player.Instance.SetCursorLock(true);
            //}
            
            
            InputManager.Instance.SwitchToGameControls();
        }
    }
}