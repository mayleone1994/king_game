using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SuitController : SubscriberBase, IController
{
    private King_ServiceLocator _serviceLocator;
    private TurnValidatorController _turnValidatorController;
    private List<CardData> _cardsOnBoard;
    private CardSuit _suitRestriction;
    private CardSuit _currentSuit;

    public CardSuit CurrentSuit => _currentSuit;

    public void Init(King_ServiceLocator serviceLocator)
    {
        if(_init) return;

        _cardsOnBoard = new();
        _serviceLocator = serviceLocator;
        _turnValidatorController = _serviceLocator.GetController<TurnValidatorController>();
        ResetCurrentSuit();
        SetSuitRestriction();
        SubscribeToEvents();

        _init = true;
    }

    public void SetSuitRestriction()
    {
        // TODO
        // Update according rule's suit restriction

        _suitRestriction= CardSuit.NONE;
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
        _turnValidatorController.OnNextPlayer += SetCardsOnBoard;
        _turnValidatorController.OnTurnEnded += ResetCurrentSuit;
    }

    protected override void UnsubscribeToEvents()
    {
        CardActions.OnUpdateCardSuit -= SetCurrentSuit;
        _turnValidatorController.OnNextPlayer -= SetCardsOnBoard;
        _turnValidatorController.OnTurnEnded -= ResetCurrentSuit;
    }

    private void SetCardsOnBoard()
    {
        _cardsOnBoard = _turnValidatorController.CardsOnBoard;
    }

    private void SetCurrentSuit(CardData data)
    {
        if (_currentSuit != CardSuit.NONE)
            return;

        _currentSuit = data.Suit;
    }

    private void ResetCurrentSuit(PlayerData data = null)
    {
        _currentSuit = CardSuit.NONE;
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
