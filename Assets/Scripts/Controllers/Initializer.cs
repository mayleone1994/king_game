using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private UIReferences _references;

    private List<CardViewer> _cards = new List<CardViewer>();

    private void Awake()
    {
        _references = FindObjectOfType<UIReferences>();

        if (_references != null)
            InitGame();
        else
            ShowInitError($"{nameof(_references)} is null or not found");
    }

    private void InitGame()
    {
        _cards.Clear();

        if (!_references.DeckController.HasDeck())
        {
            ShowInitError("The current deck was not found");
            return;
        }

        ShuffleDeck();

        if (_references.PlayerContainers == null || _references.PlayerContainers.GetContainersCount == 0)
        {
            ShowInitError($"{nameof(_references.PlayerContainers)} is null or containers is equals to 0");
            return;
        }

        PopulateContainers();

    }

    public void RestartGame()
    {
        foreach (var card in _cards)
        {
            if (card != null)
                card.DestroyCard();
        }

        InitGame();
    }

    private void ShuffleDeck()
    {
        _references.DeckController.ShuffleDeck();
    }

    private void PopulateContainers()
    {
        PlayerContainerController playerContainers = _references.PlayerContainers;

        int containersCount = playerContainers.GetContainersCount;

        for (int i = 0; i < containersCount; i++)
        {
            CreateCards(i, playerContainers.GetContainerByIndex(i));
        }
    }

    private void CreateCards(int index, Transform container)
    {
        CardViewer prefab = _references.PrefabsController?.GetPrefab(
    PrefabKey.CARD_VIEWER)?.GetComponent<CardViewer>();

        for (int i = 0; i < GameConstants.CARDS_PER_PLAYER; i++)
        {
            var currCard = _references.DeckController.Deck.Pop();

            CardData card = new(currCard, index == 0);

            CardViewer cardViewer = Instantiate(prefab, container.transform);

            cardViewer.SetData(card);

            _cards.Add(cardViewer);
        }
    }

    private void ShowInitError(string cause)
    {
        Debug.LogError($"The Game can't be initialized due {cause}.");
    }
}
