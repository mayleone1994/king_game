using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{

    // Dependencies:

    [SerializeField] private DeckController _deckController;
    [SerializeField] private PlayersController _playersController;
    [SerializeField] private CardsControllers _cardsController;

    private void Awake()
    {
        InitGame();
    }

    private void InitGame()
    {
        InitPlayers();
    }

    private void InitPlayers()
    {
        if(_playersController == null)
        {
            ShowInitError("Players controller not found");
            return;
        }

        _playersController.InitPlayers();

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

       for (int i = 0; i < _playersController.PlayersViewer.Length; i++)
        {
            PlayerViewer playerViewer = _playersController.PlayersViewer[i];
            _cardsController.CreateCardsForPlayer(playerViewer, _deckController, i == 0);
        }
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
