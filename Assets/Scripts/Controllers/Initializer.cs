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

    [Header("Viewer Controllers")]
    [SerializeField] private PlayersViewerController _playerViewerController;
    [SerializeField] private RulesViewerController _rulesViewerController;


    private DeckController              _deckController;
    private PlayersDataController       _playerDataController;
    private CardsControllers            _cardsController;
    private TurnController              _turnController;
    private TurnValidatorController     _turnValidatorController;
    private ScoreController             _scoreController;
    private AIController                _aiController;
    private SuitController              _suitController;
    private RulesDataController         _rulesDataController;
    private RestartFlowController       _restartFlowController;

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

        InitRestartFlow();
    }

    private void InitRestartFlow()
    {
        _restartFlowController = new();
        _restartFlowController.SetDependency(_cardsController);
        _restartFlowController.SetDependency(_deckController);
        _restartFlowController.Init();

        InitRulesViewer();
    }

    private void InitRulesViewer()
    {
        if (_rulesViewerController == null)
        {
            ShowInitError("Rules viewer controller is null");
            return;
        }
        _rulesViewerController.Init();

        InitDataRules();
    }

    private void InitDataRules()
    {
        _rulesDataController = new();
        _rulesDataController.SetDependency(_rulesSO);
        _rulesDataController.Init();
    }

    // END INIT

    public void RestartGame()
    {
        _restartFlowController?.RestartFlow();
    }

    private void ShowInitError(string cause)
    {
        Debug.LogError($"The Game can't be initialized due {cause}.");
    }
}
