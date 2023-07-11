using UnityEngine;

namespace KingGame
{
    public class CardData
    {
        public event System.Action OnCardStateUpdated;

        public readonly string Name;
        public readonly Sprite Sprite;
        public readonly CardSuit Suit;
        public readonly CardValue Value;
        public readonly bool IsMainPlayer;
        public readonly Sprite VerseSprite;
        public readonly PlayerData PlayerData;
        public readonly CardHandler CardHandler;

        private CardState _cardState;

        public CardState CardState => _cardState;

        public CardData(CardDataSO data, Sprite verseSprite, PlayerData playerData, 
            CardHandler cardHandler)
        {
            Name = data.name;
            Sprite = data.Sprite;
            Suit = data.Suit;
            Value = data.Value;
            PlayerData = playerData;
            IsMainPlayer = playerData.IsMainPlayer;
            VerseSprite = verseSprite;
            CardHandler = cardHandler;
            _cardState = CardState.ON_HAND;
            OnCardStateUpdated?.Invoke();
        }

        public void SetCardToBoardState()
        {
            if (_cardState == CardState.ON_BOARD)
                return;

            PlayerData.CardsOnHand.Remove(this);
            _cardState = CardState.ON_BOARD;
            OnCardStateUpdated?.Invoke();
        }
    }
}