using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private MinMax _waitTimeForDecision;

    private PlayerData _playerData;

    private void Awake()
    {
        TurnController.OnPlayerTurnUpdated += RunAIPlayer;
    }

    private void OnDestroy()
    {
        TurnController.OnPlayerTurnUpdated -= RunAIPlayer;
    }

    private void RunAIPlayer(PlayerData playerData)
    {
        if (playerData.IsMainPlayer) return;

        _playerData = playerData;

        SelectCard();
    }

    private void SelectCard()
    {
        // TODO
        // Stategy to select a card

        List<CardData> cardsOnHand = _playerData.CardsOnHand;

        int randIndex = UnityEngine.Random.Range(0, cardsOnHand.Count);

        CardData sortedCard = cardsOnHand[randIndex];

        StartCoroutine(WaitToSelectCard(sortedCard));
    }

    private IEnumerator WaitToSelectCard(CardData cardData)
    {
        float randTime = UnityEngine.Random.Range(_waitTimeForDecision.min, _waitTimeForDecision.max);

        yield return new WaitForSeconds(randTime);

        cardData.CardHandler.CardAction.CallAction(CardAction.BOARD);
    }
}
