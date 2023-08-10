using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour, IController, IDependent<DeckDataSO>
{
    private DeckDataSO _deckData;

    private Stack<CardDataSO> _deck;

    public Stack<CardDataSO> Deck => _deck;

    public void Init(King_ServiceLocator serviceLocator)
    {
        ShuffleDeck();
    }

    public void SetDependency(DeckDataSO dependency)
    {
        _deckData = dependency;
    }

    private void ShuffleDeck()
    {
        _deckData.Cards.Shuffle();

        _deck = new Stack<CardDataSO>(_deckData.Cards);
    }

    public Sprite GetCurrentDeckVerse()
    {
        if(_deckData == null)
        {
            Debug.LogError("Deck information not found");
            return null;
        }

        return _deckData.VerseSprite;
    }
}
