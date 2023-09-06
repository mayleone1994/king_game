using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingGame
{
    public class RestartFlowController : SubscriberBase, IController, IDependent<DeckController>, IDependent<CardsControllers>
    {
        public static event System.Action OnReadyForUpdateRule;

        private DeckController _deckController;
        private CardsControllers _cardsController;

        public void Init()
        {
            if(_init) return;

            SubscribeToEvents();

            _init = true;
        }

        public void SetDependency(DeckController dependency)
        {
            _deckController = dependency;
        }

        public void SetDependency(CardsControllers dependency)
        {
            _cardsController = dependency;
        }

        protected override void SubscribeToEvents()
        {
            TurnValidatorController.OnRuleEnded += RestartFlow;
        }

        protected override void UnsubscribeToEvents()
        {
            TurnValidatorController.OnRuleEnded -= RestartFlow;
        }

        public void RestartFlow()
        {
            _cardsController.DestroyCardsInstances();
            _deckController.Init();
            _cardsController.Init();
            OnReadyForUpdateRule?.Invoke();
        }
    }
}
