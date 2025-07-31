using _Game.Scripts.Gameplay.Deck.DeckController;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Systems;
using _Game.Scripts.UI;
using GenericEventBus;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        [SerializeField] private UIManagementSystem _uiManagementSystem;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeckBuildingSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GenericEventBus<IEvent>>().AsSingle();
            Container.BindInterfacesAndSelfTo<TurnSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CombatSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIManagementSystem>().FromInstance(_uiManagementSystem).AsSingle();
            
            Container.BindExecutionOrder<GenericEventBus<IEvent>>(0);
            Container.BindExecutionOrder<GameInputSystem>(1);
            Container.BindExecutionOrder<DeckBuildingSystem>(2);
            Container.BindExecutionOrder<CombatSystem>(3);
            Container.BindExecutionOrder<TurnSystem>(4);
            Container.BindExecutionOrder<TimeSystem>(5);
            Container.BindExecutionOrder<SkillSystem>(6);
            Container.BindExecutionOrder<UIManagementSystem>(7);
        }
    }
}
