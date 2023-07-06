using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    // TO DEBUG THE POSITION OF EACH PLAYER BY BOARD
    [SerializeField] private int _mainPlayerIndex;

    [SerializeField] private PlayerViewer[] _playersViewer;

    private PlayerData[] _playersData;

    public PlayerData[] PlayersData => _playersData;

    public PlayerViewer[] PlayersViewer => _playersViewer;

    public void InitPlayers()
    {
        // Create local players

        _playersData = new PlayerData[GameConstants.MAX_PLAYERS];

        for (int i = 0; i < GameConstants.MAX_PLAYERS; i++)
        {
            int relativeIndex = (i + _mainPlayerIndex) % GameConstants.MAX_PLAYERS;
            PlayerData playerData = new PlayerData($"Player {relativeIndex + 1}", null, i);
            _playersData[relativeIndex] = playerData;
            PlayerViewer playerViewer = _playersViewer[i];
            playerViewer.InitPlayerViewer(playerData);
        }
    }
}
