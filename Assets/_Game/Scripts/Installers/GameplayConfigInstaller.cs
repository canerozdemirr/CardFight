using _Game.Scripts.Configs.CardConfigs;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    [CreateAssetMenu(fileName = "Gameplay Config Installer", menuName = "Installers/Gameplay Config Installer")]
    public class GameplayConfigInstaller : ScriptableObjectInstaller<GameplayConfigInstaller>
    {
        [SerializeField]
        private CardListConfig _cardListConfig;
        public override void InstallBindings()
        {
            Container.Bind<CardListConfig>().FromInstance(_cardListConfig).AsSingle();
        }
    }
}