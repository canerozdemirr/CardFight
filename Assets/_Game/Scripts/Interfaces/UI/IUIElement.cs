namespace _Game.Scripts.Interfaces.UI
{
    public interface IUIElement
    {
        void Show();
        void Hide();
        bool IsVisible { get; }
        void Initialize();
        void Cleanup();
    }
}

