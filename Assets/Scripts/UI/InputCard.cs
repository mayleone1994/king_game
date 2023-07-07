using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardViewer))]
public class InputCard : MonoBehaviour
{
    private static event Action<CardData> _onCardSelected;
    private static event Action _onCardUnselected;

    [SerializeField] private float _lerpTime;

    private CardViewer _cardViewer;

    private CardData _currentCardSelected;

    private Vector2 _initialPosition;

    private float _initDragPositionY;

    private bool _draggingToUp;

    private bool _thisCardWasChoiced;

    private void Awake()
    {
        _cardViewer = GetComponent<CardViewer>();

        _onCardSelected += SetSelectedCard;
        _onCardUnselected += SetUnselectedCard;

        _initialPosition = _cardViewer.ImageTransform.position;
    }

    private void OnDestroy()
    {
        _onCardSelected -= SetSelectedCard;
        _onCardUnselected -= SetUnselectedCard;
    }

    public void OnSelect()
    {
        if (!IsMainPlayer() || _thisCardWasChoiced)
            return;

        if (_currentCardSelected == null)
        {
            _onCardSelected?.Invoke(_cardViewer.CardData);
        }
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        _initDragPositionY = (eventData as PointerEventData).position.y;
    }

    public void OnDrag(BaseEventData eventData)
    {
        if (!IsMainPlayer() ||
            _thisCardWasChoiced ||
            _currentCardSelected == null ||
            !ThisCardIsOnStartDrag())
            return;

        PointerEventData pointerData = (PointerEventData)eventData;

        _cardViewer.ImageTransform.transform.position = ConvertPosition(new Vector3(
            _cardViewer.ImageTransform.position.x, pointerData.position.y, 0));

        float currentPosition = pointerData.position.y;

        if (_initDragPositionY != 0)
        {
            float delta = currentPosition - _initDragPositionY;

            _draggingToUp = delta > 0;
        }

        _initDragPositionY = currentPosition;
    }

    public void OnDrop()
    {
        if (!ThisCardIsOnStartDrag() || _thisCardWasChoiced)
            return;

        var imageRect = _cardViewer.ImageTransform;

        if (_draggingToUp && imageRect.position.y >= imageRect.sizeDelta.y)
        {
            imageRect.DOMove(_cardViewer.PlayerViewer.SelectedCardArea.position, _lerpTime).OnComplete(
                () => ChoiceCard());
        }
        else
        {
            imageRect.DOMoveY(_initialPosition.y, _lerpTime).
                OnComplete(() => _onCardUnselected?.Invoke());
        }
    }

    private void ChoiceCard()
    {
        _thisCardWasChoiced = true;
        _cardViewer.ImageTransform.parent = _cardViewer.PlayerViewer.SelectedCardArea.transform;
        _onCardUnselected?.Invoke();
    }

    private void SetSelectedCard(CardData cardData)
    {
        _currentCardSelected = cardData;
    }

    private void SetUnselectedCard()
    {
        _currentCardSelected = null;
    }

    private bool ThisCardIsOnStartDrag()
    {
        return _currentCardSelected != null && _currentCardSelected == _cardViewer.CardData;
    }

    private bool IsMainPlayer()
    {
        return _cardViewer.CardData.IsMainPlayer;
    }

    private Vector3 ConvertPosition(Vector3 pointerPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_cardViewer.PlayerViewer.Canvas.transform, pointerPosition,
            _cardViewer.PlayerViewer.Canvas.worldCamera, out var position);

        Vector3 newPosition = _cardViewer.PlayerViewer.Canvas.transform.TransformPoint(position);

        return new Vector3(newPosition.x, Mathf.Clamp(newPosition.y, _initialPosition.y,
            GetMaxPositionY()), 0);
    }

    private float GetMaxPositionY()
    {
        return _cardViewer.PlayerViewer.SelectedCardArea.offsetMax.y;
    }
}
