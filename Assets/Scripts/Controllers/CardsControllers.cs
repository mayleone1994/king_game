using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsControllers : MonoBehaviour
{
    [SerializeField] PrefabsControllerSO _prefabsController;

    private CardHandler _cardPrefab;

    private List<CardHandler> _cardsOnScene = new List<CardHandler>();

    public List<CardHandler> CardsOnScene => _cardsOnScene;

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

            CardHandler cardHandler = Instantiate(_cardPrefab, playerViewer.RectTransform.transform);

            CardData card = new (
                data: cardData, 
                verseSprite: deckController.GetCurrentDeckVerse(), 
                playerData: playerViewer.PlayerData,
                cardHandler: cardHandler
                );

            cardHandler.Init(card, playerViewer);

            _cardsOnScene.Add(cardHandler);

            playerCards.Add(card);
        }

        playerViewer.PlayerData.SetCards(playerCards);
    }

    public void DestroyCardsInstances()
    {
        foreach (var card in _cardsOnScene)
        {
            if (card != null)
                card.CardAction.CallAction(CardAction.REMOVE);
        }

        _cardsOnScene.Clear();
    }

    private CardHandler GetCardPrefab()
    {
        return _prefabsController.GetPrefab(PrefabKey.CARD_PREFAB).GetComponent<CardHandler>();
    }
}
