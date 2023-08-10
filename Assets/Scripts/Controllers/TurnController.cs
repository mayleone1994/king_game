using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;

public class TurnController : SubscriberBase, IController
{
    public static event Action<PlayerData> OnNextPlayer;

    private PlayersDataController _playersController;

    private TurnValidatorController _turnValidator;

    private Queue<PlayerData> _playersOrder;

    private King_ServiceLocator _serviceLocator;

    public void Init(King_ServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;

        _playersController = _serviceLocator.GetController<PlayersDataController>();

        _turnValidator = _serviceLocator.GetController<TurnValidatorController>();

        UpdatePlayersOrder();

        if (_init) return;

        SubscribeToEvents();

        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        _turnValidator.OnNextPlayer += UpdatePlayerTurn;
        _turnValidator.OnTurnEnded += UpdatePlayersOrder;
    }

    protected override void UnsubscribeToEvents()
    {
        _turnValidator.OnNextPlayer -= UpdatePlayerTurn;
        _turnValidator.OnTurnEnded -= UpdatePlayersOrder;
    }

    public void UpdatePlayersOrder(PlayerData playerData = null)
    {
        _playersOrder = new();

        PlayerData[] players = _playersController.PlayersData;

        int max = players.Length;

        int playerIndex = playerData == null
            ? UnityEngine.Random.Range(0, max)
            : playerData.RoomIndex;

        for (int i = 0; i < max; i++)
        {
            int relativeIndex = Utils.GetRelativeIndex(i, playerIndex, max);

            _playersOrder.Enqueue(players[relativeIndex]);
        }

        List<PlayerData> playerOrderDebug = new(_playersOrder);

        playerOrderDebug.ForEach(p => { Debug.Log($"Player turn order: {p.Name}"); });

        UpdatePlayerTurn();
    }

    private void UpdatePlayerTurn()
    {
        PlayerData currentPlayer = _playersOrder.Dequeue();

        OnNextPlayer?.Invoke(currentPlayer);
    }
}
