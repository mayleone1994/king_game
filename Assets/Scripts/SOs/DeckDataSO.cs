using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KingGame;

[CreateAssetMenu(fileName = "New Deck", menuName = "Create New Deck")]
public class DeckDataSO : ScriptableObject
{
    // EDITOR DATA:

    [SerializeField] private Sprite _verseSprite;
    [SerializeField] private DeckType _deckType;
    [SerializeField] List<CardDataSO> _cards;

    public Sprite VerseSprite => _verseSprite;
    public DeckType DeckType => _deckType;
    public List<CardDataSO> Cards => _cards;
}
