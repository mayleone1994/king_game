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

    private TurnValidatorController _turnValidator;

    private Queue<PlayerData> _playersOrder;

    public void InitTurnController(PlayersController playersController, TurnValidatorController turnValidator)
    {
        _playersController = playersController;
        _turnValidator = turnValidator;

        UpdatePlayersOrder();

        _turnValidator.OnPlayerTurnChanged += UpdatePlayerTurn;
    }

    private void OnDestroy()
    {
        _turnValidator.OnPlayerTurnChanged -= UpdatePlayerTurn;
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

        OnPlayerTurnUpdated?.Invoke(currentPlayer);
    }
}
