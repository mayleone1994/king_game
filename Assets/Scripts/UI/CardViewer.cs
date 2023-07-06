using KingGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class CardViewer : MonoBehaviour
{
    private Image _imageComponent;

    private CardData _cardData;

    private bool _init = false;

    public CardData CardData => _cardData;

    public Image ImageComponent => _imageComponent;

    private void Awake()
    {
        _imageComponent = this.GetComponent<Image>();
    }

    public void SetData(CardData cardData)
    {
        if (_init) return;

        _cardData = cardData;

        ShowSprite(_cardData.IsMainPlayer);

        _init = true;
    }

    public void ShowSprite(bool showSprite)
    {
        _imageComponent.sprite = showSprite ? _cardData.Sprite : _cardData.VerseSprite;
    }

    public void DestroyCard()
    {
        Destroy(this.gameObject);
    }
}
