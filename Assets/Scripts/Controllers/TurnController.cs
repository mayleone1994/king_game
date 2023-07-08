using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnController : MonoBehaviour
{
    public static event Action<PlayerData> OnPlayerTimeToPlayUpdated;

    private PlayersController _playersController;

    private Queue<PlayerData> _playersOrder;

    public void InitTurnController(PlayersController playersController)
    {
        _playersController = playersController;
    }

    public void RandomizePlayersOrder()
    {
        _playersOrder = new();

        PlayerData[] players = _playersController.PlayersData;

        int max = players.Length;

        int sortedIndex = UnityEngine.Random.Range(0, max);

        for (int i = 0; i < max; i++)
        {
            int relativeIndex = Utils.GetRelativeIndex(i, sortedIndex, max);
            _playersOrder.Enqueue(players[relativeIndex]);
        }

        List<PlayerData> playerOrderDebug = new(_playersOrder);

        playerOrderDebug.ForEach(p => { Debug.Log($"Player turn order: {p.Name}"); });

        UpdatePlayerTime();
    }

    private void UpdatePlayerTime()
    {
        PlayerData currentPlayer = _playersOrder.Dequeue();

        OnPlayerTimeToPlayUpdated?.Invoke(currentPlayer);
    }
}
