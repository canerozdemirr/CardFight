using System;
using _Game.Scripts.Gameplay.Card;
using _Game.Scripts.Gameplay.Card.Data;
using Zenject;

namespace _Game.Scripts.Factories
{
    [Serializable]
    public class CardFactory : PlaceholderFactory<CardData, Card>
    {
        
    }
}
