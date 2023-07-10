using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardValidator : MonoBehaviour
{
    private Image _imageComponent;
    private CardData _cardData;

    private bool _init = false;

    private void Awake()
    {
        TurnController.OnPlayerTurnUpdated += ValidateByTurn;
    }

    private void OnDestroy()
    {
        TurnController.OnPlayerTurnUpdated -= ValidateByTurn;

        _cardData.OnCardStateUpdated -= ValidateByStateChanges;
    }

    public void Init(Image imageComponent, CardData cardData)
    {
        if (_init) return;

        _imageComponent = imageComponent;
        _cardData = cardData;
        _cardData.OnCardStateUpdated += ValidateByStateChanges;

        _init = true;
    }
    private void ValidateByTurn(PlayerData playerData = null)
    {
        SetCardInteraction(CardIsAvailableToPlay());

        if(OnHand())
            SetCardColor(IsPlayerTurn() && HasValidSuit());
    }

    private void ValidateByStateChanges()
    {
        SetCardInteraction(CardIsAvailableToPlay());
    }

    private void SetCardInteraction(bool state)
    {
        _imageComponent.raycastTarget = state;
    }

    private void SetCardColor(bool state)
    {
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
        return true; // TODO
    }
}
