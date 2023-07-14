using Crystal;
using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour, IController
{
    // TO DEBUG THE POSITION OF EACH PLAYER BY BOARD
    [SerializeField] private int _mainPlayerIndex;

    [SerializeField] private PrefabsControllerSO _prefabsController;

    [SerializeField] private SafeArea _safeAreaToCreateContainer;

    private List<PlayerViewer> _playersViewer;

    private RectTransform _playersContainer;

    private PlayerData[] _playersData;

    private ScoreController _scoreController;

    private King_ServiceLocator _serviceLocator;

    public PlayerData[] PlayersData => _playersData;

    public List<PlayerViewer> PlayersViewer => _playersViewer;
    public RectTransform PlayersContainer => _playersContainer;

    public void Init(King_ServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;

        _scoreController = _serviceLocator.GetController<ScoreController>();

        CreatePlayersContainer();
    }

    private void CreatePlayersContainer()
    {
        GameObject prefab = _prefabsController.GetPrefab(PrefabKey.PLAYERS_CONTAINER);

        _playersContainer = Instantiate(
            prefab, _safeAreaToCreateContainer.transform).GetComponent<RectTransform>();

        _playersViewer = new();

        foreach (Transform child in _playersContainer.transform)
        {
            PlayerViewer playerViewer = child.GetComponent<PlayerViewer>();

            if (playerViewer == null)
                continue;

            _playersViewer.Add(playerViewer);
        }

        CreatePlayersData();
    }

    private void CreatePlayersData()
    {
        // Create local players

        _playersData = new PlayerData[GameConstants.MAX_PLAYERS];

        for (int i = 0; i < GameConstants.MAX_PLAYERS; i++)
        {
            int relativeIndex = Utils.GetRelativeIndex(i, _mainPlayerIndex, GameConstants.MAX_PLAYERS);

            PlayerData playerData = new
                (   name: $"Player {relativeIndex + 1}",
                    picture: null, 
                    id: i,
                    isMainPlayer: i == 0,
                    roomIndex: relativeIndex);

            _playersData[relativeIndex] = playerData;

            PlayerViewer playerViewer = _playersViewer[i];

            playerViewer.InitPlayerViewer(playerData, 
                _safeAreaToCreateContainer.GetComponentInParent<Canvas>(),
                _scoreController);
        }
    }
}
