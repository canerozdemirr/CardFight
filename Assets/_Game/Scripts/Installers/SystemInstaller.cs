using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Systems;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeckBuildingSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GenericEventBus<IEvent>>().AsSingle();
            
            Container.BindExecutionOrder<GameInputSystem>(0);
            Container.BindExecutionOrder<DeckBuildingSystem>(1);
        }
    }
}
