using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsControllers : MonoBehaviour
{
    [SerializeField] PrefabsControllerSO _prefabsController;

    private CardViewer _cardPrefab;

    private List<CardViewer> _cardsOnScene = new List<CardViewer>();

    public List<CardViewer> CardsOnScene => _cardsOnScene;

    private void Awake()
    {
        _cardPrefab = GetCardPrefab();
    }

    public void CreateCardsForPlayer(PlayerViewer playerViewer, DeckController deckController)
    {
        List<CardData> playerCards = new List<CardData>();

        _cardPrefab ??= GetCardPrefab();

        for (int i = 0; i < GameConstants.CARDS_PER_PLAYER; i++)
        {
            CardDataSO cardData = deckController.Deck.Pop();

            CardData card = new (
                data: cardData, 
                verseSprite: deckController.GetCurrentDeckVerse(), 
                playerData: playerViewer.PlayerData);

            CardViewer cardViewer = Instantiate(_cardPrefab, playerViewer.RectTransform.transform);

            cardViewer.SetData(card, playerViewer);

            _cardsOnScene.Add(cardViewer);

            playerCards.Add(card);
        }

        playerViewer.PlayerData.SetCards(playerCards);
    }

    public void DestroyCardsInstances()
    {
        foreach (var card in _cardsOnScene)
        {
            if (card != null)
                card.DestroyCard();
        }

        _cardsOnScene.Clear();
    }

    private CardViewer GetCardPrefab()
    {
        return _prefabsController.GetPrefab(PrefabKey.CARD_VIEWER).GetComponent<CardViewer>();
    }
}
