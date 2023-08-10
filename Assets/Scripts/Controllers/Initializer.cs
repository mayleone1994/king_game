using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{

    [Header("Configs")]
    [SerializeField] private RoomConfigSO _roomConfig;
    [SerializeField] private PrefabsControllerSO _prefabsSO;
    [SerializeField] private RuleDataSO[] _rulesSO;

    [SerializeField] private PlayersViewerController _playerViewerController;


    private DeckController              _deckController;
    private PlayersDataController       _playerDataController;
    private CardsControllers            _cardsController;
    private TurnController              _turnController;
    private TurnValidatorController     _turnValidatorController;
    private ScoreController             _scoreController;
    private AIController                _aiController;
    private SuitController              _suitController;
    private RulesController             _rulesController;

    private void Awake()
    {
        InitGame();
    }

    private void InitGame()
    {
        InitRoomConfigs();
    }

    private void InitRoomConfigs()
    {
        if (_roomConfig == null)
        {
            ShowInitError("Room configs not found");
            return;
        }

        InitDataPlayers();
    }

    private void InitDataPlayers()
    {
        _playerDataController = new();
        _playerDataController.Init();

        InitViewerPlayers();
    }

    private void InitViewerPlayers()
    {
        if (_playerViewerController == null)
        {
            ShowInitError("Players viewer controller is null");
            return;
        }

        _playerViewerController.SetDependency(_playerDataController);
        _playerViewerController.Init();

        InitDeck();
    }

    private void InitDeck()
    {
        _deckController = new();
        _deckController.SetDependency(_roomConfig.RoomDeckData);
        _deckController.Init();

        InitSuitController();
    }

    private void InitSuitController()
    {
        _suitController = new();
        _suitController.Init();

        InitRules();
    }

    private void InitRules()
    {
        _rulesController = new();
        _rulesController.SetDependency(_rulesSO);
        _rulesController.Init();

        InitScoreController();
    }

    private void InitScoreController()
    {
        _scoreController = new();
        _scoreController.Init();

        InitTurnValidator();
    }

    private void InitTurnValidator()
    {
        _turnValidatorController = new();
        _turnValidatorController.Init();

        InitCards();
    }

    private void InitCards()
    {
        _cardsController = new();
        _cardsController.SetDependency(_prefabsSO);
        _cardsController.SetDependency(_deckController);
        _cardsController.SetDependency(_playerViewerController);
        _cardsController.SetDependency(_suitController);
        _cardsController.Init();

        InitAIController();
    }

    private void InitAIController()
    {
        if (_roomConfig.RoomType != RoomType.ONLINE)
        {
            _aiController = new();
            _aiController.SetDependency(_roomConfig);
            _aiController.Init();
        }

        InitTurn();
    }

    private void InitTurn()
    {
        _turnController = new();
        _turnController.SetDependency(_playerDataController);
        _turnController.Init();
        // END INIT
    }

    public void RestartGame()
    {
        _cardsController?.DestroyCardsInstances();
        InitDeck();
    }

    private void ShowInitError(string cause)
    {
        Debug.LogError($"The Game can't be initialized due {cause}.");
    }
}
