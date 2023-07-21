using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardValidator : SubscriberBase, ICardModule
{
    private Image _imageComponent;
    private RaycastTarget _raycastTarget;
    private CardData _cardData;
    private SuitController _suitController;

    public bool IsValidaSuitToPlay => HasValidSuit();

    public void InitModule(CardHandler cardHandler)
    {
        if (_init) return;

        _imageComponent = cardHandler.ImageComponent;
        _cardData = cardHandler.CardData;
        _raycastTarget = cardHandler.RaycastTarget;
        _suitController = cardHandler.ServiceLocator.GetController<SuitController>();
        SubscribeToEvents();

        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        TurnController.OnNextPlayer += ValidateByTurn;
        _cardData.OnCardStateUpdated += ValidateByStateChanges;
    }

    protected override void UnsubscribeToEvents()
    {
        TurnController.OnNextPlayer -= ValidateByTurn;
        _cardData.OnCardStateUpdated -= ValidateByStateChanges;
    }

    private void ValidateByTurn(PlayerData playerData = null)
    {
        SetCardInteraction(CardIsAvailableToPlay());

        if(_cardData.IsMainPlayer)
        {
            SetCardColor(IsPlayerTurn() && HasValidSuit());
        } else
        {
            SetCardColor(IsPlayerTurn());
        }
    }

    private void ValidateByStateChanges()
    {
        SetCardInteraction(CardIsAvailableToPlay());
    }

    private void SetCardInteraction(bool state)
    {
        _raycastTarget.raycastTarget = state;
    }

    private void SetCardColor(bool state)
    {
        if (!OnHand()) return;

        _imageComponent.color = state ? Color.white : Color.grey;
    }

    private bool CardIsAvailableToPlay()
    {
        if (!IsMainPlayerCard()) return false;
        if (!IsPlayerTurn()) return false;
        if(!HasValidSuit()) return false;
        if (!OnHand()) return false;

        return true;
    }

    private bool IsMainPlayerCard()
    {
        return _cardData.PlayerData.IsMainPlayer;
    }

    private bool IsPlayerTurn()
    {
        return _cardData.PlayerData.IsPlayerTurn;
    }

    private bool OnHand()
    {
        return _cardData.CardState == CardState.ON_HAND;
    }

    private bool HasValidSuit()
    {
        return _suitController.IsValidSuitToPlay(_cardData);
    }
}
