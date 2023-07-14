using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour, IController
{
    [SerializeField] private DeckDataSO _currDeck;

    private Stack<CardDataSO> _cards;

    public DeckDataSO CurrDeck => _currDeck;

    public Stack<CardDataSO> Deck => _cards;

    public void Init(King_ServiceLocator serviceLocator)
    {
        ShuffleDeck();
    }

    public bool HasDeck()
    {
        return _currDeck != null;
    }

    private void ShuffleDeck()
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
