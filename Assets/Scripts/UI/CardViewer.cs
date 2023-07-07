using KingGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class CardViewer : MonoBehaviour
{
    private Image _imageComponent;

    private RectTransform _imageTransform;

    private CardData _cardData;

    private bool _init = false;

    private PlayerViewer _playerViewer;

    public CardData CardData => _cardData;

    public Image ImageComponent => _imageComponent;

    public RectTransform ImageTransform => _imageTransform;


    public PlayerViewer PlayerViewer => _playerViewer;

    private void Awake()
    {
        _imageComponent = this.GetComponent<Image>();

        _imageTransform = _imageComponent.GetComponent<RectTransform>();
    }

    public void SetData(CardData cardData, PlayerViewer playerViwer)
    {
        if (_init) return;

        _cardData = cardData;

        _playerViewer = playerViwer;

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
