using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Commands.UICommands
{
    [Serializable]
    public class CloseUICommand<T> : ICommand where T : MonoBehaviour, IUIElement
    {
        [Inject] private IUIManagementSystem _uiManagementSystem;

        public UniTask Execute()
        {
            _uiManagementSystem.CloseUI<T>();
            return UniTask.CompletedTask;
        }
    }
}