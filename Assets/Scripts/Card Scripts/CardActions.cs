using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardActions : MonoBehaviour
{
    public static event Action<CardData> OnCardSelected;

    private Dictionary<CardAction, Action<Action>> _cardActions;

    private CardData _cardData;
    private CardAnimation _cardAnimation;
    private CardHandler _cardHandler;
    private EventsMediator _eventsMediator;
    private bool _init = false;

   public void Init(CardData cardData, CardAnimation cardAnimation, CardHandler cardHandler, EventsMediator eventsMediator)
    {
        if(_init) return;

        _cardData = cardData;
        _cardAnimation = cardAnimation;
        _cardHandler = cardHandler;
        _eventsMediator = eventsMediator;
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
        _cardAnimation.CardToBoardAnimation(SelectCard);
        _cardData.SetCardToBoardState();

        void SelectCard()
        {
            callback?.Invoke();
            _eventsMediator.InvokeEvent(EventKey.PLAY_CARD, _cardData);
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
