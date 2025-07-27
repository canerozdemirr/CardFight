using System;
using _Game.Scripts.Gameplay.Card.Data;
using _Game.Scripts.Gameplay.Cards;
using Zenject;

namespace _Game.Scripts.Factories
{
    [Serializable]
    public class CardFactory : PlaceholderFactory<CardData, Card>
    {
        
    }
}
