using KingGame;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]

public class CardInput : SubscriberBaseMonoBehaviourBase, ICardModule
{
    private static event Action<CardData> _onCardSelected;
    private static event Action _onCardUnselected;

    private CardData _cardData;

    private CardData _currentCardSelected;

    private CardActions _cardActions;

    private CardPosition _cardPosition;

    protected override void SubscribeToEvents()
    {
        _onCardSelected += SetSelectedCard;
        _onCardUnselected += SetUnselectedCard;
    }

    protected override void UnsubscribeToEvents()
    {
        _onCardSelected -= SetSelectedCard;
        _onCardUnselected -= SetUnselectedCard;
    }

    public void InitModule(CardHandler cardHandler)
    {
        if (_init) return;

        _cardData = cardHandler.CardData;
        _cardActions = cardHandler.CardAction;
        _cardPosition = cardHandler.CardPosition;
        SubscribeToEvents();
        _init = true;
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        if (!CanDragCard()) return;

        _onCardSelected?.Invoke(_cardData);

        PointerEventData pointerData = (PointerEventData)eventData;

        _cardPosition.SetInitDragPosition(pointerData.position.y);
    }

    public void OnDrag(BaseEventData eventData)
    {
        if (!CanDragCard()) return;

        PointerEventData pointerData = (PointerEventData)eventData;

        _cardPosition.SetDragPosition(pointerData.position.y);
    }

    public void OnDrop()
    {
        if (!CanDragCard()) return;

        DragDirection dragDirection = _cardPosition.GetDragDirection();

        if (dragDirection == DragDirection.UP)
        {
            PlayCardToBoard();
        }
        else
        {
            ReturnCardToHand();
        }
    }

    private bool CanDragCard()
    {
        return (_currentCardSelected == null || _currentCardSelected == _cardData);
    }

    private void PlayCardToBoard()
    {
        _cardActions.CallAction(CardAction.BOARD, () => _onCardUnselected?.Invoke());
    }

    private void ReturnCardToHand()
    {
        _cardActions.CallAction(CardAction.HAND, () => _onCardUnselected?.Invoke());
    }

    private void SetSelectedCard(CardData cardData)
    {
        _currentCardSelected = cardData;
    }
    private void SetUnselectedCard()
    {
        _currentCardSelected = null;
    }
}
