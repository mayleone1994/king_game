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

        public CardData(CardDataSO data, bool isMainPlayer)
        {
            Name = data.name;
            Sprite = data.Sprite;
            Suit = data.Suit;
            Value = data.Value;
            IsMainPlayer = isMainPlayer;
        }
    }
}