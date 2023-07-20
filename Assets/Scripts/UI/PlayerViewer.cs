using KingGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PlayerViewer : SubscriberBase
{
    [Header("Components")]
    [SerializeField] private HorizontalLayoutGroup _cardsOnHand;
    [SerializeField] private RectTransform _boardArea;
    [SerializeField] private TMP_Text _nameArea;
    [SerializeField] private TMP_Text _scoreArea;

    private Canvas _canvas;

    private PlayerData _playerData;

    private ScoreController _scoreController;

    public PlayerData PlayerData => _playerData;

    public RectTransform RectTransform => this.GetComponent<RectTransform>();

    public RectTransform BoardArea => _boardArea;

    public Canvas Canvas => _canvas;

    public void InitPlayerViewer(PlayerData playerData, Canvas canvas, King_ServiceLocator serviceLocator)
    {
        if (_init) return;

        _playerData = playerData;

        _canvas = canvas;

        _scoreController = serviceLocator.GetController<ScoreController>();

        SetPlayerName(_playerData.Name);

        UpdateScoreValue(_playerData, $"{ScoreController.DEFAULT_SCORE_TEXT} 0");

        SubscribeToEvents();

        _init = true;
    }

    public void SortCardsOnHandByValue()
    {
        List<CardData> sortedCards = _playerData.CardsOnHand.
            OrderBy(c => c.Suit).ThenByDescending(c => c.Value).ToList();

        List<CardHandler> cardInstances = new();

        for (int i = 0; i < _cardsOnHand.transform.childCount; i++)
        {
            var currentCard = _cardsOnHand.transform.GetChild(i).GetComponent<CardHandler>();

            if (currentCard == null)
                continue;

            cardInstances.Add(currentCard);
        }

        for (int i = 0; i < sortedCards.Count; i++)
        {
            var cardInstance = cardInstances.FirstOrDefault(c => c.GetComponent<CardHandler>().CardData == sortedCards[i]);

            cardInstance.transform.SetAsFirstSibling();
        }
    }

    protected override void SubscribeToEvents()
    {
        _scoreController.OnScoreUpdated += UpdateScoreValue;
    }

    protected override void UnsubscribeToEvents()
    {
        _scoreController.OnScoreUpdated -= UpdateScoreValue;
    }

    private void SetPlayerName(string name)
    {
        _nameArea.text = name;
    }

    private void UpdateScoreValue(PlayerData playerData, string newValue)
    {
        if (playerData != _playerData) return;

        _scoreArea.text = newValue;
    }
}
