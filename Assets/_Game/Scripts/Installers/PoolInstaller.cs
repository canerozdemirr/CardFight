using _Game.Scripts.Factories;
using _Game.Scripts.Gameplay.Card;
using _Game.Scripts.Gameplay.Card.Data;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class PoolInstaller : MonoInstaller
    {
        [BoxGroup("Card Pool")] [SerializeField]
        private GameObject _cardPrefab;

        [BoxGroup("Card Pool")] [SerializeField]
        private int _poolSize;

        [BoxGroup("Card Pool")] [SerializeField]
        private int _maxPoolSize;

        [BoxGroup("Card Pool")] [SerializeField]
        private Transform _cardPoolParent;

        public override void InstallBindings()
        {
            Container.BindFactory<CardData, Card, CardFactory>()
                .FromPoolableMemoryPool(poolBinder => poolBinder
                    .WithInitialSize(_poolSize)
                    .WithMaxSize(_maxPoolSize)
                    .FromComponentInNewPrefab(_cardPrefab)
                    .UnderTransform(_cardPoolParent));
        }
    }
}