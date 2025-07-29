using UnityEngine;

namespace _Game.Scripts.Interfaces.UI
{
    public interface IUIManagementSystem
    {
        void OpenUI<T>() where T : MonoBehaviour, IUIElement;
        void CloseUI<T>() where T : MonoBehaviour, IUIElement;
        bool IsUIOpen<T>() where T : MonoBehaviour, IUIElement;
        T GetUI<T>() where T : MonoBehaviour, IUIElement;
    }
}
