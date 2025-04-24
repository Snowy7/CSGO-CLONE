using SnTerminal;
using UnityEngine.InputSystem;
using Utils.Attributes;

namespace SnInput
{
    [InspectorHeader("Input Manager")]
    public class InputManager : InputManagerBase
         {
             // ignore never used warning
     #pragma warning disable 67
     
             // static events
             public static event OnContextDelegate OnChatToggle;
     
             // events
             public static event OnContextDelegate OnDrive;
             public static event OnContextDelegate OnBackward;
             public static event OnContextDelegate OnSteer;
             public static event OnContextDelegate OnHandbrake;
             public static event OnContextDelegate OnBoost;
             public static event OnContextDelegate OnJump;
             public static event OnContextDelegate OnLockCursor;
             public static event OnContextDelegate OnToggleNextUI;
             public static event OnContextDelegate OnToggleInventory;
             public static event OnContextDelegate OnTogglePreviousUI;
             public static event OnContextDelegate OnToggleFriendsListUI;
     
             // end warning ignore
     #pragma warning restore 67
     
             protected override void OnActionTriggered(InputAction.CallbackContext context)
             {
                 if (Terminal.HasReference && !Terminal.Instance.IsClosed)
                     return;
     
                 switch (context.action.name)
                 {
                     case "Drive":
                         OnDrive?.Invoke(context);
                         break;
    
                     case "Backward":
                         OnBackward?.Invoke(context);
                         break;
    
                     case "Steer":
                         OnSteer?.Invoke(context);
                         break;
    
                     case "Handbrake":
                         OnHandbrake?.Invoke(context);
                         break;
    
                     case "Boost":
                         OnBoost?.Invoke(context);
                         break;
    
                     case "Jump":
                         OnJump?.Invoke(context);
                         break;
                
                     case "ChatToggle":
                         OnChatToggle?.Invoke(context);
                         break;
                     case "ToggleInventory":
                         OnToggleInventory?.Invoke(context);
                         break;
                     case "Next":
                         OnToggleNextUI?.Invoke(context);
                         break;
                     case "Previous":
                         OnTogglePreviousUI?.Invoke(context);
                         break;
                     case "Friends List":
                         OnToggleFriendsListUI?.Invoke(context);
                         break;
                     case "OnChatToggle":
                         OnChatToggle?.Invoke(context);
                         break;
                 }
             }
         }
}