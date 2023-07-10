using KingGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardViewer : MonoBehaviour
{
    private CardData _cardData;

    private Image _imageComponent;

    private bool _init = false;

    public void Init(CardData cardData, Image imageComponent)
    {
        if (_init) return;

        _cardData = cardData;

        _imageComponent = imageComponent;

        ShowSprite(_cardData.IsMainPlayer);

        _init = true;
    }

    public void ShowSprite(bool showSprite)
    {
        _imageComponent.sprite = showSprite ? _cardData.Sprite : _cardData.VerseSprite;
    }
}
