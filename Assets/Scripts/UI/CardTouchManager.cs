using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CardViewer))]  
public class CardTouchManager : MonoBehaviour
{
    [SerializeField] private float _tweenDuration, _tweenValue;

    private CardViewer _cardViewer;

    private float _initPosY;

    private void Awake()
    {
        _cardViewer = GetComponent<CardViewer>();
        _initPosY = _cardViewer.ImageComponent.GetComponent<RectTransform>().anchoredPosition.y;
    }

    public void OnTouchEnter()
    {
        return;

        if (!_cardViewer.CardData.IsMainPlayer)
            return;

        _cardViewer.LayoutElement.layoutPriority = 1;

        _cardViewer.ImageComponent.GetComponent<RectTransform>().DOAnchorPosY(_tweenValue, _tweenDuration);
    }

    public void OnTouchExit()
    {
        return;

        _cardViewer.LayoutElement.layoutPriority = 0;

        _cardViewer.ImageComponent.DOKill();

        _cardViewer.ImageComponent.GetComponent<RectTransform>().DOAnchorPosY(_initPosY, 0);
    }
}
