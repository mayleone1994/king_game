using System.Collections;
using System.Collections.Generic;

namespace KingGame
{
    public class DeckController : IDependent<DeckDataSO>, IController
    {
        private DeckDataSO _deckData;

        private Stack<CardDataSO> _deck;

        public Stack<CardDataSO> Deck => _deck;

        public void SetDependency(DeckDataSO dependency)
        {
            _deckData = dependency;
        }

        public void Init()
        {
            ShuffleDeck();
        }

        private void ShuffleDeck()
        {
            _deckData.Cards.Shuffle();

            _deck = new Stack<CardDataSO>(_deckData.Cards);
        }

        public UnityEngine.Sprite GetCurrentDeckVerse()
        {
            return _deckData.VerseSprite;
        }
    }
}
