using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KingGame;

public class CardPosition : MonoBehaviour
{
    private RectTransform _cardRect;
    private Canvas _canvas;
    private float _initialPosition_Y;
    private float _initDragPosition_Y;
    private DragDirection _dragDirection;

    private bool _init = false;

    public void Init(RectTransform cardRect, Canvas canvas)
    {
        if (_init) return;

        _cardRect = cardRect;
        _canvas = canvas;
        _initialPosition_Y = _cardRect.transform.position.y;

        _init = true;
    }

    public void SetInitDragPosition(float positionY)
    {
        _initDragPosition_Y = positionY;
    }

    public void SetDragPosition(float positionY)
    {
        _cardRect.transform.position = ConvertScreenPosition(new Vector3(
           _cardRect.transform.position.x, positionY, 0));

        float currentPosition = positionY;

        float delta = currentPosition - _initDragPosition_Y;

       _dragDirection = delta > 0 ? DragDirection.UP : DragDirection.DOWN;

        _initDragPosition_Y = currentPosition;
    }

    public DragDirection GetDragDirection()
    {
        return _dragDirection;
    }

    private Vector3 ConvertScreenPosition(Vector3 pointerPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform, pointerPosition,
            _canvas.worldCamera, out var position);

        Vector3 newPosition = _canvas.transform.TransformPoint(position);

        return new Vector3(newPosition.x, Mathf.Clamp(newPosition.y, _initialPosition_Y,
            GetMaxPositionY()), 0);
    }

    private float GetMaxPositionY()
    {
        return Screen.height / 2;
    }
}
