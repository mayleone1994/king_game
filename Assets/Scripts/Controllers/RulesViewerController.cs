using Crystal;
using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RulesViewerController : SubscriberBaseMonoBehaviourBase, IController
{
    [SerializeField] private Transform _uiArea;
    [SerializeField] private PrefabsControllerSO _prefabController;

    public static Action OnRulesClosed;

    public void Init()
    {
        if (_init) return;
        SubscribeToEvents();
        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        RulesDataController.OnRuleDataUpdated += CreateNewRulesViewer;
    }

    protected override void UnsubscribeToEvents()
    {
        RulesDataController.OnRuleDataUpdated -= CreateNewRulesViewer;
    }

    private void CreateNewRulesViewer(RuleDataSO currentRuleDataSO)
    {
        RulesUIViewer rulesViewer = Instantiate(_prefabController.GetPrefab(PrefabKey.RULE_UI_PREFAB),
            _uiArea).GetComponent<RulesUIViewer>();

        rulesViewer.SetRulesView(currentRuleDataSO, () => OnRulesClosed?.Invoke());
    }
}
