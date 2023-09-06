using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class RulesUIViewer : MonoBehaviour
{
    [SerializeField] private float _openAnimationTime = 0.5f;
    [SerializeField] private float _closeAnimationTime = 0.25f;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _ruleName;
    [SerializeField] private TMP_Text _ruleDescription;
    [SerializeField] private Image _ruleArt;
    public void SetRulesView(RuleDataSO ruleData, UnityAction closeButtonAction = null)
    {
        _canvasGroup.DOFade(0, 0);
        _closeButton.gameObject.SetActive(false);
        _ruleName.text = ruleData.RuleName;
        _ruleDescription.text = ruleData.RuleDescription;
        _ruleArt.sprite = ruleData.RuleArt;
        _closeButton.onClick.AddListener(closeButtonAction);
        InitRuleViewAnimation();
    }
    public void DestroyThis()
    {
        _closeButton.interactable = false;
        _canvasGroup.DOFade(0, _closeAnimationTime).OnComplete(() => Destroy(this.gameObject));
    }

    private void InitRuleViewAnimation()
    {
        _canvasGroup.DOFade(1, _openAnimationTime).OnComplete(() => _closeButton.gameObject.SetActive(true));
    }
}
