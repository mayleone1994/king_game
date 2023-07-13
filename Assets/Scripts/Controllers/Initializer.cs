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

    private void Start()
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

        _aiController.gameObject.SetActive(_roomConfig.RoomType != RoomType.ONLINE);

        InitPlayers();
    }

    private void InitPlayers()
    {
        if(_playersController == null)
        {
            ShowInitError("Players controller not found");
            return;
        }

        _playersController.InitPlayers(_scoreController);

        InitDeck();
    }

    private void InitDeck()
    {
        if (_deckController == null || !_deckController.HasDeck())
        {
            ShowInitError("The current deck was not found");
            return;
        }

        _deckController.ShuffleDeck();

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
            _cardsController.CreateCardsForPlayer(playerViewer, _deckController, _turnValidatorController);
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

        _turnValidatorController.Init();

        InitTurn();
    }

    private void InitTurn()
    {
        if(_turnController == null)
        {
            ShowInitError("Turn controller was not found");
            return;
        }

        _turnController.InitTurnController(_playersController, _turnValidatorController);

        InitScoreController();
    }

    private void InitScoreController()
    {
        if (_scoreController == null)
        {
            ShowInitError("Score controller was not found");
            return;
        }

        _scoreController.Init(_turnValidatorController);
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
