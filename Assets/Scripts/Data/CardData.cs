using UnityEngine;

namespace KingGame
{
    public class CardData
    {
        public readonly string Name;
        public readonly Sprite Sprite;
        public readonly CardSuit Suit;
        public readonly CardValue Value;
        public readonly bool IsMainPlayer;
        public readonly Sprite VerseSprite;
        public readonly CardsControllers CardController;

        public CardData(CardDataSO data, Sprite verseSprite, bool isMainPlayer, CardsControllers cardController)
        {
            Name = data.name;
            Sprite = data.Sprite;
            Suit = data.Suit;
            Value = data.Value;
            IsMainPlayer = isMainPlayer;
            VerseSprite = verseSprite;
            CardController = cardController;
        }
    }
}