using System;
using System.Collections.Generic;
using System.Linq;

namespace KingGame
{
    public class TurnValidatorController : SubscriberBase, IController
    {
        private List<CardData> _cardsOnBoard;

        private PlayerData _playerWinner;

        private CardSuit _currentCardSuit;

        private int _turnCount;

        public static event Action OnNextPlayer;

        public static event Action<PlayerWinnerData> OnChangePlayerWinner;

        public static event Action<PlayerData> OnTurnEnded;

        public static event Action OnRuleEnded;

        public static Action<List<CardData>> OnCardsOnBoardUpdated;

        public void Init()
        {
            _cardsOnBoard = new List<CardData>();

            InitTurnCount();

            if (_init) return;

            SubscribeToEvents();

            _init = true;
        }

        protected override void SubscribeToEvents()
        {
            CardActions.OnCardSelected += OnCardSelected;
            SuitController.OnCurrentSuitChanged += SetCurrentCardSuit;
        }

        protected override void UnsubscribeToEvents()
        {
            CardActions.OnCardSelected -= OnCardSelected;
            SuitController.OnCurrentSuitChanged -= SetCurrentCardSuit;
        }

        private void InitTurnCount()
        {
            _turnCount = GameConstants.CARDS_PER_PLAYER;
        }

        private void SetCurrentCardSuit(CardSuit suit)
        {
            _currentCardSuit = suit;
        }

        private void OnCardSelected(CardData cardData)
        {
            _cardsOnBoard.Add(cardData);
            OnCardsOnBoardUpdated?.Invoke(_cardsOnBoard);

            // All players has selected one card at this turn
            if (_cardsOnBoard.Count == GameConstants.MAX_PLAYERS)
            {
                ValidateCardWinner();
            }
            else
            {
                // Otherwise, it's next player's turn
                OnNextPlayer?.Invoke();
            }
        }

        private void ValidateCardWinner()
        {
            List<CardData> cardsWithTargetSuit = _cardsOnBoard.Where
                (c => c.Suit == _currentCardSuit).ToList();

            CardData cardWinner = cardsWithTargetSuit.OrderByDescending(c => c.Value).ToList()[0];

            _playerWinner = cardWinner.PlayerData;

            PlayerWinnerData playerWinnerData = new PlayerWinnerData
            {
                playerViewer = cardWinner.CardHandler.PlayerViewer,
                cardData = cardWinner,
            };

            OnChangePlayerWinner?.Invoke(playerWinnerData);

            ProcessEndValidation();
        }

        private void ProcessEndValidation()
        {
            Action endCallback = EndValidation;

            for (int i = 0; i < _cardsOnBoard.Count; i++)
            {
                bool isLastCardOnList = i == _cardsOnBoard.Count - 1;

                CardActions cardAction = _cardsOnBoard[i].CardHandler.CardAction;

                cardAction.CallAction(CardAction.EXIT, isLastCardOnList ? endCallback : null);
            }
        }

        private void EndValidation()
        {
            _cardsOnBoard.Clear();

            _turnCount--;

            SetNewStep();
        }

        private void SetNewStep()
        {
            if (_turnCount > 0)
            {
                OnTurnEnded?.Invoke(_playerWinner);
            } else
            {
                InitTurnCount();
                OnRuleEnded?.Invoke();
            }
        }
    }
}
