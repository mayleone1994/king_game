using KingGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PlayerViewer : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup _cardsOnHand;
    [SerializeField] private RectTransform _selectedCardArea;
    [SerializeField] private TMP_Text _nameArea;

    private Canvas _canvas;

    private PlayerData _playerData;

    public PlayerData PlayerData => _playerData;

    public RectTransform RectTransform => this.GetComponent<RectTransform>();

    public RectTransform BoardArea => _selectedCardArea;

    public Canvas Canvas => _canvas;

    public void InitPlayerViewer(PlayerData playerData, Canvas canvas)
    {
        _playerData = playerData;

        _canvas = canvas;

        SetPlayerName(_playerData.Name);
    }
    private void SetPlayerName(string name)
    {
        _nameArea.text = name;
    }
}
