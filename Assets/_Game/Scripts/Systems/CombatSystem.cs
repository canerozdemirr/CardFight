using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Systems;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class CombatSystem : ICombatSystem, ICombatRegister, IInitializable, IDisposable
    {
        private List<ICardPlayer> _registeredPlayers = new();
        private Dictionary<ICardPlayer, Card> _cardsInCombat = new();

        public void ResolveCombat()
        {
            for (int i = 0; i < _cardsInCombat.Count; i++)
            {
                Card attackingCard = _cardsInCombat.ElementAt(i).Value;
                int randomIndex = UnityEngine.Random.Range(0, _cardsInCombat.Count);
                (ICardPlayer defendingPlayer, Card defendingCard) = _cardsInCombat.ElementAt(randomIndex);
                ExecuteAttack(attackingCard, defendingPlayer, defendingCard);
            }

            CleanupDefeatedCards();
        }

        private void ExecuteAttack(Card attackingCard, ICardPlayer defendingPlayer, Card defendingCard)
        { 
            int attackDamage = attackingCard.AttackPoint;

            defendingCard.Health.TakeDamage(attackDamage);
            
            int overflowDamage = defendingCard.CalculateOverflowDamage(attackDamage);
            if (overflowDamage > 0)
            {
                defendingPlayer.TakeDamage(overflowDamage);
            }

            Debug.Log($"Combat Result: {defendingCard.CardData.CardName} health: {defendingCard.Health.CurrentHealth}/{defendingCard.Health.MaxHealth}");
            
            if (!defendingCard.Health.IsAlive)
            {
                Debug.Log($"{defendingCard.CardData.CardName} was defeated!");
            }
        }

        private void CleanupDefeatedCards()
        {
            foreach (ICardPlayer player in _registeredPlayers)
            {
                _cardsInCombat.Remove(player);
            }
        }

        public void AddCardToCombat(ICardPlayer cardPlayer, Card card)
        {
            if (cardPlayer != null && card != null)
            {
                _cardsInCombat[cardPlayer] = card;
            }
        }

        public void RemoveCardFromCombat(ICardPlayer cardPlayer)
        {
            _cardsInCombat.Remove(cardPlayer);
        }

        public bool IsInCombat(ICardPlayer cardPlayer)
        {
            return _cardsInCombat.ContainsKey(cardPlayer);
        }

        public Card GetCardInCombat(ICardPlayer cardPlayer)
        {
            return _cardsInCombat.TryGetValue(cardPlayer, out Card card) ? card : null;
        }

        public int GetCombatantCount()
        {
            return _cardsInCombat.Count;
        }

        private ICardPlayer GetCardOwner(Card card)
        {
            return _cardsInCombat.FirstOrDefault(kvp => kvp.Value == card).Key;
        }

        public void Initialize()
        {
            Debug.Log("Combat System initialized");
        }

        public void Dispose()
        {
            _registeredPlayers.Clear();
        }
        
        public void RegisterPlayer(ICardPlayer player)
        {
            if (player != null && !_registeredPlayers.Contains(player))
            {
                _registeredPlayers.Add(player);
                Debug.Log($"Player registered to combat system. Total players: {_registeredPlayers.Count}");
            }
        }

        public void UnregisterPlayer(ICardPlayer player)
        {
            if (player != null && _registeredPlayers.Contains(player))
            {
                _registeredPlayers.Remove(player);
                Debug.Log($"Player unregistered from combat system. Total players: {_registeredPlayers.Count}");
            }
        }

        public IReadOnlyList<ICardPlayer> RegisteredPlayers => _registeredPlayers;
    }
}
