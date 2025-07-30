namespace _Game.Scripts.Interfaces.Deck
{
    public interface IPlayerDeckBuilder
    {
        void PreparePlayerDeck();
        void ClearUnusedCards();
        void CloseDeckSlots();
    }
}
