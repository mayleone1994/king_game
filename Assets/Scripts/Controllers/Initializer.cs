using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{

    [Header("Configs")]
    [SerializeField] private RoomConfigSO _roomConfig;


    [Header("Controller Dependencies")]
    [SerializeField] private DeckController             _deckController;
    [SerializeField] private PlayersController          _playersController;
    [SerializeField] private CardsControllers           _cardsController;
    [SerializeField] private TurnController             _turnController;
    [SerializeField] private TurnValidatorController    _turnValidatorController;
    [SerializeField] private ScoreController            _scoreController;
    [SerializeField] private AIController               _aiController;
    [SerializeField] private SuitController             _suitController;

    private King_ServiceLocator _serviceLocator;

    private void Awake()
    {
        InitGame();
    }

    private void InitGame()
    {
        InitServiceLocator();
    }

    private void InitServiceLocator()
    {
        _serviceLocator = new King_ServiceLocator();

        _serviceLocator.SetController(_aiController.GetType(), _aiController);

        _serviceLocator.SetController(_playersController.GetType(), _playersController);

        _serviceLocator.SetController(_deckController.GetType(), _deckController);

        _serviceLocator.SetController(_cardsController.GetType(), _cardsController);

        _serviceLocator.SetController(_turnController.GetType(), _turnController);

        _serviceLocator.SetController(_turnValidatorController.GetType(), _turnValidatorController);

        _serviceLocator.SetController(_scoreController.GetType(), _scoreController);

        _serviceLocator.SetController(_suitController.GetType(), _suitController);

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
            _aiController.Init(_serviceLocator);
        }

        InitPlayers();
    }

    private void InitPlayers()
    {
        if(_playersController == null)
        {
            ShowInitError("Players controller not found");
            return;
        }

       _playersController.Init(_serviceLocator);

        InitDeck();
    }

    private void InitDeck()
    {
        if (_deckController == null || !_deckController.HasDeck())
        {
            ShowInitError("The current deck was not found");
            return;
        }

        _deckController.Init(_serviceLocator);

        InitCards();
    }

    private void InitCards()
    {
        if(_cardsController == null)
        {
            ShowInitError("Cards controller was not found");
            return;
        }

        _cardsController.Init(_serviceLocator);

        InitTurnValidator();
    }

    private void InitTurnValidator()
    {
        if(_turnValidatorController == null)
        {
            ShowInitError("Turn validator controller was not found");
            return;
        }

        _turnValidatorController.Init(_serviceLocator);

        InitScoreController();
    }

    private void InitScoreController()
    {
        if (_scoreController == null)
        {
            ShowInitError("Score controller was not found");
            return;
        }

        _scoreController.Init(_serviceLocator);

        InitSuitController();
    }

    private void InitSuitController()
    {
        if (_suitController == null)
        {
            ShowInitError("Suit controller was not found");
            return;
        }

        _suitController.Init(_serviceLocator);

        InitTurn();
    }

    private void InitTurn()
    {
        if (_turnController == null)
        {
            ShowInitError("Turn controller was not found");
            return;
        }

        _turnController.Init(_serviceLocator);

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
