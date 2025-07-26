using _Game.Scripts.Factories;
using _Game.Scripts.Gameplay.Card.Data;
using UnityEngine;
using Zenject;

public class PoolTest : MonoBehaviour
{
    [Inject]
    private CardFactory _cardFactory;

    private void Start()
    {
        _cardFactory.Create(new CardData("Heyy", new CardAttackData()));
    }
}
