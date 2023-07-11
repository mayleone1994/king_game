using KingGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class CardViewer : MonoBehaviour
{
    private CardData _cardData;

    private Image _imageComponent;

    private bool _init = false;

    private void Awake()
    {
        CardActions.OnCardSelected += SelectedCard;
    }

    private void OnDestroy()
    {
        CardActions.OnCardSelected -= SelectedCard;
    }

    public void Init(CardData cardData, Image imageComponent)
    {
        if (_init) return;

        _cardData = cardData;

        _imageComponent = imageComponent;

        ShowSprite(_cardData.IsMainPlayer);

        _init = true;
    }

    private void ShowSprite(bool showSprite)
    {
        _imageComponent.sprite = showSprite ? _cardData.Sprite : _cardData.VerseSprite;
    }

    private void SelectedCard(CardData cardData)
    {
        if (cardData != _cardData) return;

        ShowSprite(true);
    }
}
