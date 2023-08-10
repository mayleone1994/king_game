using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SuitController : SubscriberBase, IController
{
    public static event Action<CardSuit> OnCurrentSuitChanged;

    private King_ServiceLocator _serviceLocator;
    private List<CardData> _cardsOnBoard;
    private CardSuit _suitRestriction;
    private CardSuit _currentSuit;

    public void Init(King_ServiceLocator serviceLocator)
    {
        if(_init) return;

        _cardsOnBoard = new();
        ResetCurrentSuit();
        SetSuitRestriction();
        SubscribeToEvents();

        _init = true;
    }

    public void SetSuitRestriction()
    {
        // TODO
        // Update according rule's suit restriction

        _suitRestriction = CardSuit.NONE;
    }

    public bool IsValidSuitToPlay(CardData cardData)
    {
        // TODO
        // I must improve this validation, maybe using a smart flag validator

        if(!HasCardsOnBoard())
        {
            if (!HasSuitRestriction())
            {
                return true;
            } 
            else
            {
                if (PlayerOnlyHasSuitRestriction(cardData.PlayerData))
                {
                    ResetCurrentSuit();
                    return true;
                } 
                else
                {
                    return cardData.Suit != _suitRestriction;
                }
            }
        }

        if (!PlayerHasTargetSuitToPlay(cardData.PlayerData))
        {
            return true;
        }

        return cardData.Suit == _currentSuit;
    }

    protected override void SubscribeToEvents()
    {
        CardActions.OnUpdateCardSuit += SetCurrentSuit;
        TurnValidatorController.OnCardsOnBoardUpdated += SetCardsOnBoard;
        TurnValidatorController.OnTurnEnded += ResetCurrentSuit;
    }

    protected override void UnsubscribeToEvents()
    {
        CardActions.OnUpdateCardSuit -= SetCurrentSuit;
        TurnValidatorController.OnCardsOnBoardUpdated -= SetCardsOnBoard;
        TurnValidatorController.OnTurnEnded -= ResetCurrentSuit;
    }

    private void SetCardsOnBoard(List<CardData> cardsOnBoard)
    {
        _cardsOnBoard = cardsOnBoard;
    }

    private void SetCurrentSuit(CardData data)
    {
        if (_currentSuit != CardSuit.NONE)
            return;

        _currentSuit = data.Suit;

        OnCurrentSuitChanged?.Invoke(_currentSuit);
    }

    private void ResetCurrentSuit(PlayerData data = null)
    {
        _currentSuit = CardSuit.NONE;

        OnCurrentSuitChanged?.Invoke(_currentSuit);
    }

    private bool HasCardsOnBoard()
    {
        return _cardsOnBoard.Count > 0;
    }

    private bool HasSuitRestriction()
    {
        return _suitRestriction != CardSuit.NONE;
    }

    private bool PlayerHasTargetSuitToPlay(PlayerData playerData)
    {
        return playerData.CardsOnHand.Count(c => c.Suit == _currentSuit) > 0;
    }

    private bool PlayerOnlyHasSuitRestriction(PlayerData playerData)
    {
        return playerData.CardsOnHand.Count(c => c.Suit == _suitRestriction) == 
            playerData.CardsOnHand.Count;
    }
}
