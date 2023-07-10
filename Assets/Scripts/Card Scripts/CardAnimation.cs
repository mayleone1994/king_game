using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private float _cardToBoardTimeAnimation;
    [SerializeField] private float _cardToHandTimeAnimation;

    private RectTransform _cardRect;
    private PlayerViewer _playerViewer;

    private float _initialPosition_Y;

    private bool _init = false;

    public void Init(RectTransform cardRect, PlayerViewer playerViewer)
    {
        if (_init) return;

        _cardRect = cardRect;
        _playerViewer = playerViewer;
        _initialPosition_Y = _cardRect.position.y;

        _init = true;
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
        // TODO
    }

    public void DrawAnimation(Action callback)
    {
        // TODO
    }
}
