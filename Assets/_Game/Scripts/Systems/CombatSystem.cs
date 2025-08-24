using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Events.Game;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Systems;
using GenericEventBus;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class CombatSystem : ICombatSystem, ICombatRegister, IInitializable, IDisposable
    {
        private List<ICardPlayer> _registeredPlayers = new();
        private Dictionary<ICardPlayer, Card> _cardsInCombat = new();

        [Inject] private ITurnSystem _turnSystem;

        [Inject] private GenericEventBus<IEvent> _genericEventBus;
        
        public void ResolveCombat()
        {
            // Only resolve combat if all registered players have cards in combat
            if (_cardsInCombat.Count < _registeredPlayers.Count)
            {
                Debug.Log($"Combat skipped: Not all players have cards. Cards in combat: {_cardsInCombat.Count}, Registered players: {_registeredPlayers.Count}");
                return;
            }

            for (int i = 0; i < _cardsInCombat.Count; i++)
            {
                Card attackingCard = _cardsInCombat.ElementAt(i).Value;
                int randomIndex = UnityEngine.Random.Range(0, _cardsInCombat.Count);
                while (randomIndex == i)
                {
                    randomIndex = UnityEngine.Random.Range(0, _cardsInCombat.Count);
                }

                (ICardPlayer defendingPlayer, Card defendingCard) = _cardsInCombat.ElementAt(randomIndex);
                ExecuteAttack(attackingCard, defendingPlayer, defendingCard);
            }
        }

        private void ExecuteAttack(Card attackingCard, ICardPlayer defendingPlayer, Card defendingCard)
        {
            int overflowDamage = attackingCard.AttackPoint - defendingCard.DefensePoint;
            if (overflowDamage > 0)
            {
                defendingPlayer.TakeDamage(overflowDamage);
                if (!defendingPlayer.Health.IsAlive)
                {
                    _genericEventBus.Raise(new OnGameEnded(GetWinner()));
                }
            }

            attackingCard.OnDespawned();
            defendingCard.OnDespawned();
        }

        private void CleanupCards()
        {
            // Create a copy of the keys to avoid modification during iteration
            var playersWithCards = _cardsInCombat.Keys.ToList();
            foreach (ICardPlayer player in playersWithCards)
            {
                if (_cardsInCombat.ContainsKey(player))
                {
                    _cardsInCombat[player].OnDespawned();
                    _cardsInCombat.Remove(player);
                }
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

        public ICardPlayer GetWinner()
        {
            ICardPlayer winner = _registeredPlayers[0];
            int highestHealth = winner.Health.CurrentHealth;

            for (int i = 1; i < _registeredPlayers.Count; i++)
            {
                int currentPlayerHealth = _registeredPlayers[i].Health.CurrentHealth;
                if (currentPlayerHealth <= highestHealth)
                    continue;
                
                highestHealth = currentPlayerHealth;
                winner = _registeredPlayers[i];
            }

            return highestHealth > 0 ? winner : null;
        }
        
        public Card GetCardInCombat(ICardPlayer ownerPlayer) => _cardsInCombat[ownerPlayer];

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
            if (player == null || _registeredPlayers.Contains(player))
                return;

            _registeredPlayers.Add(player);
            Debug.Log($"Player registered to combat system. Total players: {_registeredPlayers.Count}");
        }

        public void UnregisterPlayer(ICardPlayer player)
        {
            if (player == null || !_registeredPlayers.Contains(player))
                return;

            _registeredPlayers.Remove(player);
            Debug.Log($"Player unregistered from combat system. Total players: {_registeredPlayers.Count}");
        }

        public IReadOnlyList<ICardPlayer> RegisteredPlayers => _registeredPlayers;
    }
}