using KingGame;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardViewer : SubscriberBaseMonoBehaviourBase, ICardModule
{
    private CardData _cardData;

    private Image _imageComponent;

    public void InitModule(CardHandler cardHandler)
    {
        if (_init) return;

        _cardData = cardHandler.CardData;

        _imageComponent = cardHandler.ImageComponent;

        ShowSprite(_cardData.IsMainPlayer);

        SubscribeToEvents();

        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        CardActions.OnCardSelected += SelectedCard;
    }

    protected override void UnsubscribeToEvents()
    {
        CardActions.OnCardSelected -= SelectedCard;
    }

    public void ChangeUIOrderPriority()
    {
        _cardData.CardHandler.transform.SetAsLastSibling();
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
