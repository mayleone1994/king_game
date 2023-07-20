using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    // Dependencies

    [Header ("Dependencies")]
    [SerializeField] private CardViewer        _cardViewer;
    [SerializeField] private CardInput         _cardInput;
    [SerializeField] private CardActions       _cardActions;
    [SerializeField] private CardAnimation     _cardAnimation;
    [SerializeField] private CardValidator     _cardValidator;
    [SerializeField] private CardPosition      _cardPosition;

    [Header("Components")]
    [SerializeField] private Image _imageComponent;
    [SerializeField] private RaycastTarget _raycastTarget;
    [SerializeField] private RectTransform _cardRect;

    private bool _init = false;

    private PlayerViewer _playerViewer;
    private CardData _cardData;
    private King_ServiceLocator _serviceLocator;

    public King_ServiceLocator ServiceLocator => _serviceLocator;
    public Image ImageComponent => _imageComponent;
    public RaycastTarget RaycastTarget => _raycastTarget;
    public RectTransform CardRect => _cardRect; 
    public PlayerViewer PlayerViewer => _playerViewer;
    public CardData CardData => _cardData;
    public CardViewer CardViewer => _cardViewer;
    public CardInput CardInput => _cardInput;
    public CardActions CardAction => _cardActions;
    public CardAnimation CardAnimation => _cardAnimation;
    public CardValidator CardValidator => _cardValidator;
    public CardPosition CardPosition => _cardPosition;

    public void Init(CardData cardData, PlayerViewer playerViewer, King_ServiceLocator serviceLocator)
    {
        if (_init) return;

        _cardData = cardData;

        _playerViewer = playerViewer;

        _serviceLocator = serviceLocator;

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
        _cardViewer.InitModule(this);
    }

    private void InitAnimation()
    {
        _cardAnimation.InitModule(this);
    }

    private void InitActions()
    {
        _cardActions.InitModule(this);
    }

    private void InitValidator()
    {
        _cardValidator.InitModule(this);
    }

    private void InitPosition()
    {
        _cardPosition.InitModule(this);
    }

    private void InitInput()
    {
        _cardInput.InitModule(this);
    }
}
