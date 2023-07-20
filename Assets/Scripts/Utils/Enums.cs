namespace KingGame
{
    public enum CardValue
    {
        TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
    };

    public enum CardSuit
    {
        HEARTS, CLUBS, SPADES, DIAMONDS, NONE
    };

    public enum Rule
    {
        NO_WINS, NO_HEARTS, NO_WOMAN, NO_MEN, NO_KING, NO_WIN_TWO_LAST, TO_UP
    };

    public enum DeckType
    {
        DEFAULT, NONE
    };

    public enum CardAction
    {
        DRAW, BOARD, HAND, EXIT, REMOVE
    };

    public enum CardState
    {
        ON_HAND, ON_BOARD, ON_DECK
    };

    public enum DragDirection
    {
        UP, DOWN
    };

    public enum PrefabKey
    {
        CARD_PREFAB, PLAYERS_CONTAINER_PREFAB
    };

    public enum RoomType
    {
        OFFLINE, ONLINE, TUTORIAL
    };
}