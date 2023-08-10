using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : SubscriberBase, IController
{
    public static event Action<PlayerData, string> OnScoreUpdated;

    public const string DEFAULT_SCORE_TEXT = "Score:";

    private King_ServiceLocator _serviceLocator;

    private TurnValidatorController _turnValidatorController;

    public void Init(King_ServiceLocator serviceLocator)
    {
        if (_init) return;

        _serviceLocator = serviceLocator;

        _turnValidatorController = _serviceLocator.GetController<TurnValidatorController>();

        SubscribeToEvents();

        _init = true;
    }

    protected override void SubscribeToEvents()
    {
        _turnValidatorController.OnTurnEnded += UpdatePlayerWinnerScore;
    }

    protected override void UnsubscribeToEvents()
    {
        _turnValidatorController.OnTurnEnded -= UpdatePlayerWinnerScore;
    }

    private void UpdatePlayerWinnerScore(PlayerData playerData)
    {
        // TODO
        // Set score value according current rule

        playerData.UpdateScore(-25);

        int playerScore = playerData.CurrentScore;

        string scoreText = $"{DEFAULT_SCORE_TEXT} {playerScore}";

        OnScoreUpdated?.Invoke(playerData, scoreText);
    }
}
