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
        public readonly PlayerData PlayerData;

        public CardData(CardDataSO data, Sprite verseSprite, PlayerData playerData)
        {
            Name = data.name;
            Sprite = data.Sprite;
            Suit = data.Suit;
            Value = data.Value;
            PlayerData = playerData;
            IsMainPlayer = playerData.IsMainPlayer;
            VerseSprite = verseSprite;
        }
    }
}