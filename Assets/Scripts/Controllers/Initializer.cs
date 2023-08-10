using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{

    [Header("Configs")]
    [SerializeField] private RoomConfigSO _roomConfig;
    [SerializeField] private PrefabsControllerSO _prefabsSO;


    [Header("Controller Dependencies")]
    [SerializeField] private DeckController              _deckController;
    [SerializeField] private PlayersDataController       _playerDataController;
    [SerializeField] private PlayersViewerController     _playerViewerController;
    [SerializeField] private CardsControllers            _cardsController;
    [SerializeField] private TurnController              _turnController;
    [SerializeField] private TurnValidatorController     _turnValidatorController;
    [SerializeField] private ScoreController             _scoreController;
    [SerializeField] private AIController                _aiController;
    [SerializeField] private SuitController              _suitController;
    [SerializeField] private RulesController             _rulesController;

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

        InitAIController();
    }

    private void InitAIController()
    {
        if (_aiController == null)
        {
            ShowInitError("AI Controller not found");
            return;
        }

        if (_roomConfig.RoomType != RoomType.ONLINE)
        {
            _aiController.SetDependency(_roomConfig);
            _aiController.Init();
        }

        InitDataPlayers();
    }

    private void InitDataPlayers()
    {
        if(_playerDataController == null)
        {
            ShowInitError("Players data controller not found");
            return;
        }

       _playerDataController.Init();

        InitViewerPlayers();
    }

    private void InitViewerPlayers()
    {
        if (_playerViewerController == null)
        {
            ShowInitError("Players viewer controller not found");
            return;
        }

        _playerViewerController.SetDependency(_playerDataController);

        _playerViewerController.Init();

        InitDeck();
    }

    private void InitDeck()
    {
        if (_deckController == null)
        {
            ShowInitError("The current deck was not found");
            return;
        }

        _deckController.SetDependency(_roomConfig.RoomDeckData);

        _deckController.Init();

        InitCards();
    }

    private void InitCards()
    {
        if(_cardsController == null)
        {
            ShowInitError("Cards controller was not found");
            return;
        }

        _cardsController.SetDependency(_prefabsSO);
        _cardsController.SetDependency(_deckController);
        _cardsController.SetDependency(_playerViewerController);

        _cardsController.Init();

        InitRules();
    }

    private void InitRules()
    {
        if(_rulesController == null)
        {
            ShowInitError("Rules controller was not found");
            return;
        }

        _rulesController.Init();

        InitTurnValidator();
    }

    private void InitTurnValidator()
    {
        if(_turnValidatorController == null)
        {
            ShowInitError("Turn validator controller was not found");
            return;
        }

        _turnValidatorController.Init();

        InitScoreController();
    }

    private void InitScoreController()
    {
        if (_scoreController == null)
        {
            ShowInitError("Score controller was not found");
            return;
        }

        _scoreController.Init();

        InitSuitController();
    }

    private void InitSuitController()
    {
        if (_suitController == null)
        {
            ShowInitError("Suit controller was not found");
            return;
        }

        _suitController.Init();

        InitTurn();
    }

    private void InitTurn()
    {
        if (_turnController == null)
        {
            ShowInitError("Turn controller was not found");
            return;
        }

        _turnController.SetDependency(_playerDataController);
        _turnController.Init();

        // END INITALIZATION
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
