using Snowy.Utils;

namespace Interface
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public bool CanToggleMenu { get; set; } = true;

        public static bool CanToggle
        {
            get => Instance.CanToggleMenu;
            set => Instance.CanToggleMenu = value;
        }
    }
}