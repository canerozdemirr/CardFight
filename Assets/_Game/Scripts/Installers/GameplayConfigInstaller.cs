using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Configs.PlayerConfigs;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    [CreateAssetMenu(fileName = "Gameplay Config Installer", menuName = "Installers/Gameplay Config Installer")]
    public class GameplayConfigInstaller : ScriptableObjectInstaller<GameplayConfigInstaller>
    {
        [SerializeField]
        private CardListConfig _cardListConfig;
        
        [SerializeField]
        private CardPlayerListConfig _cardPlayerListConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<CardListConfig>().FromInstance(_cardListConfig).AsSingle();
            Container.Bind<CardPlayerListConfig>().FromInstance(_cardPlayerListConfig).AsSingle();
        }
    }
}