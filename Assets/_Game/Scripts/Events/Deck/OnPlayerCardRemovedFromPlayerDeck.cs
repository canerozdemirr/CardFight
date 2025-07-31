using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Deck
{
    using Interfaces.Players;

    public struct OnPlayerCardRemovedFromPlayerDeck : IEvent
    {
        public readonly IPlayerDeck PlayerDeck;
        public OnPlayerCardRemovedFromPlayerDeck(IPlayerDeck playerDeck)
        {
            PlayerDeck = playerDeck;
        }
    }
}
