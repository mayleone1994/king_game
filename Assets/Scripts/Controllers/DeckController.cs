using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField] private DeckDataSO _currDeck;

    private Stack<CardDataSO> _cards;

    public DeckDataSO CurrDeck => _currDeck;

    public Stack<CardDataSO> Deck => _cards;

    public bool HasDeck()
    {
        return _currDeck != null;
    }

    public void ShuffleDeck()
    {
        _currDeck.Cards.Shuffle();

        _cards = new Stack<CardDataSO>(_currDeck.Cards);
    }

    public Sprite GetCurrentDeckVerse()
    {
        if(_currDeck == null)
        {
            Debug.LogError("Deck information not found");
            return null;
        }

        return _currDeck.VerseSprite;
    }
}
