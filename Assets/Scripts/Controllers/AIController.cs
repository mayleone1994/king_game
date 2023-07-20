using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController : SubscriberBase, IController
{
    [SerializeField] private MinMax _waitTimeForDecision;

    private PlayerData _playerData;

    private King_ServiceLocator _serviceLocator;

    public void Init(King_ServiceLocator serviceLocator)
    {
        if(_init) return;

        _serviceLocator = serviceLocator;

        SubscribeToEvents();

        _init = true;   
    }

    protected override void SubscribeToEvents()
    {
        TurnController.OnPlayerTurnUpdated += RunAIPlayer;
    }

    protected override void UnsubscribeToEvents()
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

        List<CardData> cardsAvailableToPlay = _playerData.CardsOnHand.Where
            (c => c.CardHandler.CardValidator.IsValidaSuitToPlay).ToList();

        int randIndex = UnityEngine.Random.Range(0, cardsAvailableToPlay.Count);

        CardData randomizedCard = cardsAvailableToPlay[randIndex];

        StartCoroutine(WaitToSelectCard(randomizedCard));
    }

    private IEnumerator WaitToSelectCard(CardData cardData)
    {
        float randTime = UnityEngine.Random.Range(_waitTimeForDecision.min, _waitTimeForDecision.max);

        yield return new WaitForSeconds(randTime);

        cardData.CardHandler.CardAction.CallAction(CardAction.BOARD);
    }
}
