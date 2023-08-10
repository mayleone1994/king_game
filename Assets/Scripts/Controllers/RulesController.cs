using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KingGame;

public class RulesController : SubscriberBase, IController
{
    [SerializeField] private RuleDataSO[] _rules;
    public void Init(King_ServiceLocator serviceLocator)
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
