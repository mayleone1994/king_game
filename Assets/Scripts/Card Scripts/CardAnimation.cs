using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System;
using KingGame;

public class CardAnimation : SubscriberBase, ICardModule
{
    [SerializeField] private float _cardToBoardTimeAnimation;
    [SerializeField] private float _cardToHandTimeAnimation;

    private RectTransform _cardRect;
    private PlayerViewer _playerViewer;
    private CardHandler _cardHandler;

    private float _initialPosition_Y;

    private PlayerWinnerData _playerWinnerData;

    private TurnValidatorController _turnValidator;

    public void InitModule(CardHandler cardHandler)
    {
        if (_init) return;

        _cardHandler = cardHandler;
        _cardRect = cardHandler.CardRect;
        _playerViewer = cardHandler.PlayerViewer;
        _initialPosition_Y = _cardRect.position.y;
        _turnValidator = _cardHandler.ServiceLocator.GetController<TurnValidatorController>();
        SubscribeToEvents();
        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        _turnValidator.OnPlayerWinnerUpdated += SetExitAnimationDestination;
    }

    protected override void UnsubscribeToEvents()
    {
        _turnValidator.OnPlayerWinnerUpdated -= SetExitAnimationDestination;
    }

    public void DrawAnimation(Action callback)
    {
        // TODO
    }

    public void CardToBoardAnimation(Action callback)
    {
        _cardRect.DOMove(_playerViewer.BoardArea.position, _cardToBoardTimeAnimation)
           .OnComplete(() => SetCardToBoard(callback));

        void SetCardToBoard(Action callback)
        {
            _cardRect.transform.SetParent(_playerViewer.BoardArea, true);
            callback?.Invoke();
        }
    }

    public void BackCardToHandAnimation(Action callback)
    {
        _cardRect.DOMoveY(_initialPosition_Y, _cardToHandTimeAnimation).
               OnComplete(() => callback?.Invoke());
    }

    public void ExitAnimation(Action callback)
    {
        if(_playerViewer != _playerWinnerData.playerViewer)
        {
            _cardHandler.transform.SetParent(_playerWinnerData.playerViewer.BoardArea, true);
        }

        _playerWinnerData.cardData.CardHandler.CardViewer.ChangeUIOrderPriority();

        Vector2 _exitAnimationDestination = _playerWinnerData.playerViewer.
            GetComponent<RectTransform>().position;

        StartCoroutine(StartExitAnimation(callback));

        IEnumerator StartExitAnimation(Action callback)
        {
            yield return new WaitForSeconds(0.9f);

            Vector2 screenCenter = new(Screen.width / 2, Screen.height / 2);

            _cardRect.DOMove(screenCenter, 0.5F).OnComplete
                (() => StartCoroutine(ExitAnimationAsync(callback)));

            IEnumerator ExitAnimationAsync(Action callback)
            {
                yield return new WaitForSeconds(0.8f);

                _cardRect.DOMove(_exitAnimationDestination, 0.4F).OnComplete
                    (() => _cardHandler.CardAction.CallAction(CardAction.REMOVE, callback));
            }
        }
    }

    private void SetExitAnimationDestination(PlayerWinnerData playerWinnerData)
    {
        _playerWinnerData = playerWinnerData;
    }
}
