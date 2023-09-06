using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KingGame;
using System;
using System.Linq;

namespace KingGame
{
    public class RulesDataController : SubscriberBase, IController, IDependent<RuleDataSO[]>
    {
        public static Action<RuleDataSO> OnRuleDataUpdated;

        private RuleDataSO[] _rulesData;

        private Queue<RuleDataSO> _rulesQueue;

        private RuleDataSO _currentRule;

        public void SetDependency(RuleDataSO[] dependecy)
        {
            _rulesData = dependecy;
        }

        public void Init()
        {
            if (_init) return;

            SubscribeToEvents();

            _rulesQueue = new(_rulesData.OrderBy(r => r.RuleCategory));

            InitFirstRule();

            _init = true;
        }

        public void InitFirstRule()
        {
            UpdateRule();
        }

        private void UpdateRule()
        {
            if(_rulesQueue.Count == 0)
            {
                // TODO: end game
                return;
            }
            _currentRule = _rulesQueue.Dequeue();
            OnRuleDataUpdated?.Invoke(_currentRule);
        }

        protected override void SubscribeToEvents()
        {
            RestartFlowController.OnReadyForUpdateRule += UpdateRule;
        }

        protected override void UnsubscribeToEvents()
        {
            RestartFlowController.OnReadyForUpdateRule -= UpdateRule;
        }
    }
}
