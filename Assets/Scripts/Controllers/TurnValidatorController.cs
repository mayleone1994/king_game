using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnValidatorController : SubscriberBase, IController
{
    private List<CardData> _cardsOnBoard;

    private PlayerData _playerWinner;

    private King_ServiceLocator _serviceLocator;

    private SuitController _suitController;

    public event Action OnNextPlayer;

    public event Action<PlayerWinnerData> OnChangePlayerWinner;

    public event Action<PlayerData> OnTurnEnded;
    public List<CardData> CardsOnBoard => _cardsOnBoard;

    public void Init(King_ServiceLocator serviceLocator)
    {
        _cardsOnBoard = new List<CardData>();

        if (_init) return;

        _serviceLocator = serviceLocator;

        _suitController = _serviceLocator.GetController<SuitController>();

        SubscribeToEvents();

        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        CardActions.OnCardSelected += OnCardSelected;
    }

    protected override void UnsubscribeToEvents()
    {
        CardActions.OnCardSelected -= OnCardSelected;
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
            OnNextPlayer?.Invoke();
        }
    }

    private void ValidateCardWinner()
    {
        List<CardData> cardsWithTargetSuit = _cardsOnBoard.Where
            (c => c.Suit == _suitController.CurrentSuit).ToList();

        CardData cardWinner = cardsWithTargetSuit.OrderByDescending(c => c.Value).ToList()[0];

        _playerWinner = cardWinner.PlayerData;

        PlayerWinnerData playerWinnerData = new PlayerWinnerData
        {
            playerViewer = cardWinner.CardHandler.PlayerViewer,
            cardData = cardWinner,
        };

        OnChangePlayerWinner?.Invoke(playerWinnerData);

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
