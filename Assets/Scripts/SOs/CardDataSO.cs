using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KingGame;

[CreateAssetMenu(fileName = "New Card", menuName = "Create New Card")]
public class CardDataSO : ScriptableObject
{
    // EDITOR DATA
    [SerializeField] private Sprite _sprite;
    [SerializeField] private CardValue _value;
    [SerializeField] private CardSuit _suit;

    public Sprite Sprite => _sprite;
    public CardValue Value => _value;
    public CardSuit Suit => _suit;
}
