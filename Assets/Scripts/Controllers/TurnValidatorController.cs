using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnValidatorController : MonoBehaviour
{
    private List<CardData> _cardsOnBoard;

    private PlayerData _playerWinner;

    public event Action OnPlayerTurnChanged;

    public event Action<PlayerWinnerData> OnPlayerWinnerUpdated;

    public event Action<PlayerData> OnTurnEnded;

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
        _cardsOnBoard = new List<CardData>();
    }

    private void OnCardSelected(CardData cardData)
    {
        _cardsOnBoard.Add(cardData);

        // All players has selected one card at this turn
        if(_cardsOnBoard.Count == GameConstants.MAX_PLAYERS)
        {
            ValidateCardWinner();
        } else
        {
            // Otherwise, it's next player's turn
            OnPlayerTurnChanged?.Invoke();
        }
    }

    private void ValidateCardWinner()
    {
        CardData cardWinner = _cardsOnBoard.OrderByDescending(c => c.Value).ToList()[0];

        _playerWinner = cardWinner.PlayerData;

        PlayerWinnerData playerWinnerData = new PlayerWinnerData
        {
            playerViewer = cardWinner.CardHandler.PlayerViewer,
            cardData = cardWinner,
        };

        OnPlayerWinnerUpdated?.Invoke(playerWinnerData);

        ProcessEndValidation();
    }

    private void ProcessEndValidation()
    {
        Action endCallback = EndValidation;

        for (int i = 0; i < _cardsOnBoard.Count; i++)
        {
            bool isLastCardOnList = i == _cardsOnBoard.Count - 1;

            CardActions cardAction = _cardsOnBoard[i].CardHandler.CardAction;

            cardAction.CallAction(CardAction.EXIT, isLastCardOnList ? endCallback : null);
        }
    }

    private void EndValidation()
    {
        _cardsOnBoard.Clear();

        SetNewStep();
    }

    private void SetNewStep()
    {
        // TODO:
        // if the current rule is not ended, set the last winner player to restart the turn
        // if the current rule has ended, start a new rule and randomize the new player who will
        // starts the new turn

        OnTurnEnded?.Invoke(_playerWinner);
    }
}
