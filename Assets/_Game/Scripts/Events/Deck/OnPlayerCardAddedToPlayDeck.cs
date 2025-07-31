using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Deck
{
    using Interfaces.Players;

    public struct OnPlayerCardAddedToPlayDeck : IEvent
    {
        public readonly IPlayerDeck PlayerDeck;
        public OnPlayerCardAddedToPlayDeck(IPlayerDeck playerDeck)
        {
            PlayerDeck = playerDeck;
        }
    }
}
