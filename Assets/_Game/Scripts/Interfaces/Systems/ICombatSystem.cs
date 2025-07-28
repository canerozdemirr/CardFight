using _Game.Scripts.Gameplay.Cards;

namespace _Game.Scripts.Interfaces.Systems
{
    public interface ICombatSystem
    {
        void ResolveCombat(Card playerCard, Card botCard);
    }
}
