using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsControllers : MonoBehaviour, IController
{
    [SerializeField] PrefabsControllerSO _prefabsController;

    private CardHandler _cardPrefab;

    private List<CardHandler> _cardsOnScene = new List<CardHandler>();

    private King_ServiceLocator _serviceLocator;

    public void Init(King_ServiceLocator serviceLocator)
    {
        _cardPrefab = GetCardPrefab();

        _serviceLocator = serviceLocator;
    }

    public void CreateCardsForPlayer(PlayerViewer playerViewer)
    {
        List<CardData> playerCards = new();

        _cardPrefab ??= GetCardPrefab();

        DeckController deckController = _serviceLocator.GetController<DeckController>();

        TurnValidatorController turnValidatorController = 
            _serviceLocator.GetController<TurnValidatorController>();

        for (int i = 0; i < GameConstants.CARDS_PER_PLAYER; i++)
        {
            CardDataSO cardData = deckController.Deck.Pop();

            CardHandler cardHandler = Instantiate(_cardPrefab, playerViewer.RectTransform.transform);

            CardData card = new (
                data: cardData, 
                verseSprite: deckController.GetCurrentDeckVerse(), 
                playerData: playerViewer.PlayerData,
                cardHandler: cardHandler);

            cardHandler.Init(card, playerViewer, turnValidatorController);

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

    private CardHandler GetCardPrefab()
    {
        return _prefabsController.GetPrefab(PrefabKey.CARD_PREFAB).GetComponent<CardHandler>();
    }
}
