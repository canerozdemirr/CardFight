using System;
using System.Collections.Generic;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Factories;
using _Game.Scripts.Gameplay.Deck.DeckController;
using _Game.Scripts.Interfaces.Events;
using GenericEventBus;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckBuilders
{
    public abstract class BaseDeckBuilder : MonoBehaviour
    {
        [SerializeField]
        protected BaseDeckController _deckController;
        
        [Inject]
        protected CardFactory _cardFactory;
        
        [Inject] 
        protected CardListConfig _cardListConfig;

        [Inject] 
        protected GenericEventBus<IEvent> _eventBus;
        
        protected List<Cards.Card> _cardList;
        
        public virtual void PrepareDeck()
        {
            _deckController.Initialize();
        }

        protected virtual void OnDisable()
        {
            
        }
    }
}
