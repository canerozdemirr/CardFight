namespace _Game.Scripts.Interfaces.Systems
{
    using System.Collections.Generic;
    using Players;

    public interface IDeckBuildingSystem
    {
        void AddPlayerDeck(IPlayerDeck playerDeck);
        IReadOnlyList<IPlayerDeck> AllRegisteredDecks { get; }
    }
}
