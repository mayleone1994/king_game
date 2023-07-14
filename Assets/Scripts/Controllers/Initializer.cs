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

        _serviceLocator.SetController(typeof(AIController), _aiController);

        _serviceLocator.SetController(typeof(PlayersController), _playersController);

        _serviceLocator.SetController(typeof(DeckController), _deckController);

        _serviceLocator.SetController(typeof(CardsControllers), _cardsController);

        _serviceLocator.SetController(typeof(TurnController), _turnController);

        _serviceLocator.SetController(typeof(TurnValidatorController), _turnValidatorController);

        _serviceLocator.SetController(typeof(ScoreController), _scoreController);

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

       for (int i = 0; i < _playersController.PlayersViewer.Count; i++)
        {
            PlayerViewer playerViewer = _playersController.PlayersViewer[i];
            _cardsController.Init(_serviceLocator);
            _cardsController.CreateCardsForPlayer(playerViewer);
        }

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
