using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KingGame;

[CreateAssetMenu(fileName = "New Rule", menuName = "Create New Rule")]
public class RuleDataSO : ScriptableObject
{
    // EDITOR DATA
    [SerializeField]                private Rule _ruleCategory;
    [SerializeField]                private string _ruleName;
    [SerializeField]                private Sprite _ruleArt;
    [SerializeField]                private CardSuit _suitRestriction;
    [SerializeField]                private int _ruleScoreValue;
    [SerializeField][TextArea]      private string _ruleDescription;

    public Rule RuleCategory => _ruleCategory;
    public string RuleName => _ruleName;
    public Sprite RuleArt => _ruleArt;
    public CardSuit SuitRestriction => _suitRestriction;
    public int RuleScoreValue => _ruleScoreValue;
    public string RuleDescription => _ruleDescription;
}
