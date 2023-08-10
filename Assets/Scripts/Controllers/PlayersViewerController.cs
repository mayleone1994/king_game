using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersViewerController : MonoBehaviour, IController, IDependent<PlayersDataController> 
{
    [SerializeField] private PrefabsControllerSO _prefabsController;
    [SerializeField] private Crystal.SafeArea _safeAreaToCreateContainer;

    private PlayersDataController _playersDataController;
    private List<PlayerViewer> _playersViewer;
    private RectTransform _playersContainer;

    public List<PlayerViewer> PlayersViewer => _playersViewer;

    public void SetDependency(PlayersDataController dependency)
    {
        _playersDataController = dependency;
    }

    public void Init(King_ServiceLocator serviceLocator)
    {
        GameObject prefab = _prefabsController.GetPrefab(PrefabKey.PLAYERS_CONTAINER_PREFAB);

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

        InitPlayerViewers();
    }

    private void InitPlayerViewers()
    {
        for(int i = 0; i < GameConstants.MAX_PLAYERS; i++)
        {
            int seatRelativeIndex = Utils.GetRelativeIndex(
                i, PlayersDataController.MAIN_PLAYER_SEAT_INDEX, GameConstants.MAX_PLAYERS);

            var playerData = _playersDataController.PlayersData[seatRelativeIndex];

            PlayerViewer playerViewer = _playersViewer[i];

            playerViewer.InitPlayerViewer(playerData,
                _safeAreaToCreateContainer.GetComponentInParent<Canvas>());
        }
    }
}
