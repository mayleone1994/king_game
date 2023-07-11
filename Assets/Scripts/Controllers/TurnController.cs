using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnController : MonoBehaviour
{
    public static event Action<PlayerData> OnPlayerTurnUpdated;

    private PlayersController _playersController;

    private EventsMediator _eventsMediator;

    private Queue<PlayerData> _playersOrder;

    private bool _init = false;

    public void InitTurnController(PlayersController playersController, EventsMediator eventsMediator)
    {
        _playersController = playersController;
        _eventsMediator = eventsMediator;

        UpdatePlayersOrder();

        if (!_init)
        {
            _eventsMediator.Subscribe(EventKey.TURN_CHANGED, UpdatePlayerTurn);
            _init = true;
        }
    }

    private void OnDestroy()
    {
        _eventsMediator.Unsubscribe(EventKey.TURN_CHANGED, UpdatePlayerTurn);
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

    private void UpdatePlayerTurn(object arg = null)
    {
        PlayerData currentPlayer = _playersOrder.Dequeue();

        OnPlayerTurnUpdated?.Invoke(currentPlayer);
    }
}
