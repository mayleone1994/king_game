using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TurnValidatorController : MonoBehaviour
{
    private List<CardData> _cardOnBoard;

    private EventsMediator _eventsMediator;

    public event Action OnPlayerTurnChanged;

    public event Action OnTurnEnded;

    private bool _init = false;

    public void Init(EventsMediator eventsMediator)
    {
        if (_init) return;

        _cardOnBoard = new List<CardData>();

        _eventsMediator = eventsMediator;

        _eventsMediator.Subscribe(EventKey.PLAY_CARD, OnCardSelected);

        _init = true;
    }

    private void OnCardSelected(object data)
    {
        CardData cardData = data as CardData;

        _cardOnBoard.Add(cardData);

        // All players has selected one card at this turn
        if(_cardOnBoard.Count == GameConstants.MAX_PLAYERS)
        {
            ValidateCardsValue();
        } else
        {
            // Otherwise, it's next player's turn

            _eventsMediator.InvokeEvent(EventKey.TURN_CHANGED, null);

            OnPlayerTurnChanged?.Invoke();
        }
    }

    private void ValidateCardsValue()
    {
        _eventsMediator.InvokeEvent(EventKey.TURN_ENDED, null); // 1) exit cards animation; 2) update the score
        _cardOnBoard.Clear();

        SetNewStep();
    }

    private void SetNewStep()
    {
        // TODO:
        // if the current rule is not ended, set the last winner player to restart the turn
        // if the current rule has ended, start a new rule and randomize the new player who will
        // starts the new turn
    }

    private void OnDestroy()
    {
        _eventsMediator.Unsubscribe(EventKey.PLAY_CARD, OnCardSelected);
    }
}
