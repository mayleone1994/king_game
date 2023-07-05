namespace KingGame
{
    public enum CardValue
    {
        TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE, NONE
    };

    public enum CardSuit
    {
        HEARTS, CLUBS, SPADES, DIAMONDS, NONE
    };

    public enum Round
    {
        NO_WINS, NO_HEARTS, NO_WOMAN, NO_MEN, NO_KING, NO_WIN_TWO_LAST
    };

    public enum DeckType
    {
        DEFAULT, NONE
    };

    public enum PrefabKey
    {
        CARD_VIEWER
    };
}