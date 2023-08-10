using KingGame;
using System.Collections;
using System.Collections.Generic;
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

    public PlayerData PlayerData => _playerData;

    public RectTransform RectTransform => this.GetComponent<RectTransform>();

    public RectTransform BoardArea => _boardArea;

    public Canvas Canvas => _canvas;

    public void InitPlayerViewer(PlayerData playerData, Canvas canvas)
    {
        if (_init) return;

        _playerData = playerData;

        _canvas = canvas;

        SetPlayerName(_playerData.Name);

        UpdateScoreValue(_playerData, $"{ScoreController.DEFAULT_SCORE_TEXT} 0");

        SubscribeToEvents();

        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        ScoreController.OnScoreUpdated += UpdateScoreValue;
    }

    protected override void UnsubscribeToEvents()
    {
        ScoreController.OnScoreUpdated -= UpdateScoreValue;
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
