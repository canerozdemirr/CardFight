using _Game.Scripts.Gameplay.Cards;

namespace _Game.Scripts.Interfaces.Players
{
    public interface IPlayerDeck
    {
        void AddCard(Card card);
        void RemoveCard(Card card);
    }
}
