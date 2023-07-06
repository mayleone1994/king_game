using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsControllers : MonoBehaviour
{
    [SerializeField] PrefabsController _prefabsController;

    private List<CardViewer> _cardsInstances = new List<CardViewer>();

    public List<CardViewer> CardsInstances => _cardsInstances;

    public void CreateCardsForPlayer(PlayerViewer playerViewer, DeckController deckController, bool isMainPlayer)
    {
        List<CardData> playerCards = new List<CardData>();

        CardViewer prefab = _prefabsController.GetPrefab(PrefabKey.CARD_VIEWER).GetComponent<CardViewer>();

        for (int i = 0; i < GameConstants.CARDS_PER_PLAYER; i++)
        {
            var currCard = deckController.Deck.Pop();

            CardData card = new(currCard, deckController.GetCurrentDeckVerse(), true);

            CardViewer cardViewer = Instantiate(prefab, playerViewer.RectTransform.transform);

            cardViewer.SetData(card);

            _cardsInstances.Add(cardViewer);

            playerCards.Add(card);
        }

        playerViewer.PlayerData.SetCards(playerCards);
    }

    public void DestroyCardsInstances()
    {
        foreach (var card in _cardsInstances)
        {
            if (card != null)
                card.DestroyCard();
        }

        _cardsInstances.Clear();
    }
}
