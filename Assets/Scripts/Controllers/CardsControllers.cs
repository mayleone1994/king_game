using System.Collections.Generic;
using System.Linq;

namespace KingGame
{
    // TODO: This class must be improve [!]
    public class CardsControllers : IController,
        IDependent<PrefabsControllerSO>, IDependent<PlayersViewerController>, IDependent<DeckController>,
        IDependent<SuitController>
    {
        private DeckController _deckController;
        private PlayersViewerController _playersViewerController;
        private PrefabsControllerSO _prefabsController;
        private SuitController _suitController;
        private CardHandler _cardPrefab;
        private List<CardHandler> _cardsOnScene = new List<CardHandler>();


        public void SetDependency(PrefabsControllerSO dependency)
        {
            _prefabsController = dependency;
        }

        public void SetDependency(PlayersViewerController dependency)
        {
            _playersViewerController = dependency;
        }

        public void SetDependency(DeckController dependency)
        {
            _deckController = dependency;
        }

        public void SetDependency(SuitController dependency)
        {
            _suitController = dependency;
        }

        public void Init()
        {
            _cardPrefab = GetCardPrefab();

            for (int i = 0; i < _playersViewerController.PlayersViewer.Count; i++)
            {
                PlayerViewer playerViewer = _playersViewerController.PlayersViewer[i];
                CreateCardsForPlayer(playerViewer);
            }
        }

        private void CreateCardsForPlayer(PlayerViewer playerViewer)
        {
            List<CardData> playerCards = new();

            _cardPrefab ??= GetCardPrefab();

            List<CardDataSO> playerHand = CreatePlayerHand(_deckController.Deck);

            for (int i = 0; i < playerHand.Count; i++)
            {
                CardDataSO cardData = playerHand[i];

                CardHandler cardHandler = UnityEngine.MonoBehaviour.Instantiate(
                    _cardPrefab, playerViewer.RectTransform.transform);

                CardData card = new(
                    data: cardData,
                    verseSprite: _deckController.GetCurrentDeckVerse(),
                    playerData: playerViewer.PlayerData,
                    cardHandler: cardHandler);

                cardHandler.SetDependency(_suitController);
                cardHandler.Init(card, playerViewer);

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

            for (int i = 0; i < GameConstants.CARDS_PER_PLAYER; i++)
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
}
