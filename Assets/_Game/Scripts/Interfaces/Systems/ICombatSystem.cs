using _Game.Scripts.Gameplay.Cards;

namespace _Game.Scripts.Interfaces.Systems
{
    using Players;

    public interface ICombatSystem
    {
        void ResolveCombat();
        void AddCardToCombat(ICardPlayer cardPlayer, Card card);
        void RemoveCardFromCombat(ICardPlayer cardPlayer);
        bool AllPlayersHavePlayedCards();
    }
}
