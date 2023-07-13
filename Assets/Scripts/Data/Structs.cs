using System;

namespace KingGame
{
    [Serializable]
    public struct MinMax
    {
        public float min;
        public float max;
    }

    public struct PlayerWinnerData
    {
        public PlayerViewer playerViewer;
        public CardData cardData;
    }
}