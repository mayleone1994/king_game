using KingGame;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class CardViewer : MonoBehaviour
{
    [SerializeField] private LayoutElement _layoutElement;

    private Image _imageComponent;

    private CardData _cardData;

    private Sprite _sprite;

    private bool _init = false;

    private UIReferences _references;

    public CardData CardData => _cardData;

    public Image ImageComponent => _imageComponent;
    public LayoutElement LayoutElement => _layoutElement;

    private void Awake()
    {
        _imageComponent = this.GetComponent<Image>();

        _references = FindObjectOfType<UIReferences>();
    }

    public void SetData(CardData cardData)
    {
        if (_init) return;

        _cardData = cardData;

        SetSprite();

        _init = true;
    }

    public void ShowSprite(bool showSprite)
    {
        _imageComponent.sprite = showSprite ? _sprite : _references.DeckController.CurrDeck.VerseSprite;
    }

    public void DestroyCard()
    {
        Destroy(this.gameObject);
    }

    private void SetSprite()
    {
        _sprite = _cardData.Sprite;

        ShowSprite(_cardData.IsMainPlayer);
    }
}
