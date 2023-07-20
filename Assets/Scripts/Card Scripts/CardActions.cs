using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardActions : MonoBehaviour, ICardModule
{
    public static event Action<CardData> OnCardSelected;

    public static event Action<CardData> OnUpdateCardSuit;

    private Dictionary<CardAction, Action<Action>> _cardActions;

    private CardData _cardData;
    private CardAnimation _cardAnimation;
    private CardHandler _cardHandler;
    private bool _init = false;

   public void InitModule(CardHandler cardHandler)
    {
        if(_init) return;

        _cardHandler = cardHandler;
        _cardData = cardHandler.CardData;
        _cardAnimation = cardHandler.CardAnimation;
        InitDict();
        _init = true;
    }

    public void CallAction(CardAction cardAction, Action callback = null)
    {
        _cardActions[cardAction]?.Invoke(callback);
    }

    private void InitDict()
    {
        _cardActions = new()
        {
            { CardAction.DRAW,      DrawCard},
            { CardAction.BOARD,     CardToBoard },
            { CardAction.HAND,      BackCardToHand },
            { CardAction.EXIT,      ExitCard},
            { CardAction.REMOVE,    RemoveCard }
        };
    }

    private void DrawCard(Action callback)
    {
        _cardAnimation.DrawAnimation(callback);
    }

    private void CardToBoard(Action callback)
    {
        _cardAnimation.CardToBoardAnimation(callback: SelectCard);
        _cardData.SetCardToBoardState();
        OnUpdateCardSuit?.Invoke(_cardData);

        void SelectCard()
        {
            callback?.Invoke();
            OnCardSelected?.Invoke(_cardData);
        }
    }

    private void BackCardToHand(Action callback)
    {
        _cardAnimation.BackCardToHandAnimation(callback);
    }

    private void ExitCard(Action callback)
    {
        _cardAnimation.ExitAnimation(callback);
    }

    private void RemoveCard(Action callback) 
    {
        Destroy(_cardHandler.gameObject);

        callback?.Invoke();
    }

}
