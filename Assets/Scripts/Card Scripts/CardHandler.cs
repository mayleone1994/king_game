using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    // Dependencies

    [Header ("Dependencies")]
    [SerializeField] private CardViewer _cardViewer;
    [SerializeField] private CardInput _cardInput;
    [SerializeField] private CardActions _cardActions;
    [SerializeField] private CardAnimation _cardAnimation;
    [SerializeField] private CardValidator _cardValidator;
    [SerializeField] private CardPosition _cardPosition;

    [Header("Components")]
    [SerializeField] private Image _imageComponent;
    [SerializeField] private RaycastTarget _raycastTarget;
    [SerializeField] private RectTransform _cardRect;

    private bool _init = false;

    private PlayerViewer _playerViewer;

    private CardData _cardData;

    public CardData CardData => _cardData;
    public PlayerViewer PlayerViewer => _playerViewer;
    public CardActions CardAction => _cardActions;

    public void Init(CardData cardData, PlayerViewer playerViewer)
    {
        if (_init) return;

        _cardData = cardData;

        _playerViewer = playerViewer;

        InitViewer();

        InitAnimation();

        InitActions();

        InitValidator();

        InitPosition();

        InitInput();

        _init = true;
    }

    private void InitViewer()
    {
        _cardViewer.Init(cardData: _cardData, imageComponent: _imageComponent);
    }

    private void InitAnimation()
    {
        _cardAnimation.Init(cardRect: _cardRect, playerViewer: _playerViewer);
    }

    private void InitActions()
    {
        _cardActions.Init(cardData: _cardData, cardAnimation: _cardAnimation, this);
    }

    private void InitValidator()
    {
        _cardValidator.Init(cardData: _cardData, imageComponent: _imageComponent, raycastTarget: _raycastTarget);
    }

    private void InitPosition()
    {
        _cardPosition.Init(cardRect: _cardRect, canvas: _playerViewer.Canvas);
    }

    private void InitInput()
    {
        _cardInput.Init(cardData: _cardData, cardActions: _cardActions, cardPosition: _cardPosition);
    }
}
