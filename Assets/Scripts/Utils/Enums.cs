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
        NO_TRICKS = 1, NO_HEARTS = 2, NO_MEN = 3, NO_QUEENS = 4, NO_KING = 5, 
        NO_LAST_TWO_TRICKS = 6, TO_UP = 7
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