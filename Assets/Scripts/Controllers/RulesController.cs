using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KingGame;

public class RulesController : SubscriberBase, IController, IDependent<RuleDataSO[]>
{
    private RuleDataSO[] _rulesData;

    public void SetDependency(RuleDataSO[] dependecy)
    {
        _rulesData = dependecy;
    }

    public void Init()
    {
        if (_init) return;

        SubscribeToEvents();

        _init = true;
    }

    protected override void SubscribeToEvents()
    {
       
    }

    protected override void UnsubscribeToEvents()
    {
    }
}
