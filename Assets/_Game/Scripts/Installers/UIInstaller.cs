using _Game.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private TurnCanvas _turnCanvas;
        
        [SerializeField]
        private DeckSelectionCanvas _deckSelectionCanvas;

        public override void InstallBindings()
        {
            Container.Bind<DeckSelectionCanvas>().FromInstance(_deckSelectionCanvas).AsSingle();
            Container.Bind<TurnCanvas>().FromInstance(_turnCanvas).AsSingle();
        }
    }
}