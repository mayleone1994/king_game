using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsControllers : MonoBehaviour, IController
{
    [SerializeField] PrefabsControllerSO _prefabsController;

    private CardHandler _cardPrefab;

    private List<CardHandler> _cardsOnScene = new List<CardHandler>();

    private King_ServiceLocator _serviceLocator;

    private PlayersViewerController _playersController;

    public void Init(King_ServiceLocator serviceLocator)
    {
        _cardPrefab = GetCardPrefab();

        _serviceLocator = serviceLocator;

        _playersController = _serviceLocator.GetController<PlayersViewerController>();

        for (int i = 0; i < _playersController.PlayersViewer.Count; i++)
        {
            PlayerViewer playerViewer = _playersController.PlayersViewer[i];
            CreateCardsForPlayer(playerViewer);
        }
    }

    private void CreateCardsForPlayer(PlayerViewer playerViewer)
    {
        List<CardData> playerCards = new();

        _cardPrefab ??= GetCardPrefab();

        DeckController deckController = _serviceLocator.GetController<DeckController>();

        List<CardDataSO> playerHand = CreatePlayerHand(deckController.Deck);

        for (int i = 0; i < playerHand.Count; i++)
        {
            CardDataSO cardData = playerHand[i];

            CardHandler cardHandler = Instantiate(_cardPrefab, playerViewer.RectTransform.transform);

            CardData card = new (
                data: cardData, 
                verseSprite: deckController.GetCurrentDeckVerse(), 
                playerData: playerViewer.PlayerData,
                cardHandler: cardHandler);

            cardHandler.Init(card, playerViewer, _serviceLocator);

            _cardsOnScene.Add(cardHandler);

            playerCards.Add(card);
        }

        playerViewer.PlayerData.SetCardsOnHand(playerCards);
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

    private List<CardDataSO> CreatePlayerHand(Stack<CardDataSO> deck)
    {
        List<CardDataSO> playerHand = new();

        for(int i = 0; i < GameConstants.CARDS_PER_PLAYER; i++)
        {
            playerHand.Add(deck.Pop());
        }

        playerHand = playerHand.OrderByDescending(c => c.Suit).ThenBy(c => c.Value).ToList();

        return playerHand;
    }

    private CardHandler GetCardPrefab()
    {
        return _prefabsController.GetPrefab(PrefabKey.CARD_PREFAB).GetComponent<CardHandler>();
    }
}
