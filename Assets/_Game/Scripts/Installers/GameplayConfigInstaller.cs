using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Configs.SkillConfigs;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    using Configs.TurnConfigs;

    [CreateAssetMenu(fileName = "Gameplay Config Installer", menuName = "Installers/Gameplay Config Installer")]
    public class GameplayConfigInstaller : ScriptableObjectInstaller<GameplayConfigInstaller>
    {
        [SerializeField]
        private CardListConfig _cardListConfig;
        
        [SerializeField]
        private CardPlayerListConfig _cardPlayerListConfig;

        [SerializeField] 
        private TurnConfig _turnConfig;
        
        [SerializeField]
        private SkillListConfig _skillListConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<CardListConfig>().FromInstance(_cardListConfig).AsSingle();
            Container.Bind<CardPlayerListConfig>().FromInstance(_cardPlayerListConfig).AsSingle();
            Container.Bind<TurnConfig>().FromInstance(_turnConfig).AsSingle();
            Container.Bind<SkillListConfig>().FromInstance(_skillListConfig).AsSingle();
        }
    }
}