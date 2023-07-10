using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnValidatorController : MonoBehaviour
{
    private List<CardData> _cardOnBoard;

    public event Action OnPlayerTurnChanged;

    public event Action OnTurnEnded;

    private void Awake()
    {
        CardActions.OnCardSelected += OnCardSelected;
    }

    private void OnDestroy()
    {
        CardActions.OnCardSelected -= OnCardSelected;
    }

    public void Init()
    {
        _cardOnBoard = new List<CardData>();
    }

    private void OnCardSelected(CardData cardData)
    {
        _cardOnBoard.Add(cardData);

        // All players has selected one card at this turn
        if(_cardOnBoard.Count == GameConstants.MAX_PLAYERS)
        {
            ValidateCardsValue();
        } else
        {
            // Otherwise, it's next player's turn
            OnPlayerTurnChanged?.Invoke();
        }
    }

    private void ValidateCardsValue()
    {
        OnTurnEnded?.Invoke(); // 1) exit cards animation; 2) update the score
        _cardOnBoard.Clear();

        SetNewStep();
    }

    private void SetNewStep()
    {
        // TODO:
        // if the current rule is not ended, set the last winner player to restart the turn
        // if the current rule has ended, start a new rule and randomize the new player who will
        // starts the new turn
    }
}
