using KingGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public event Action<PlayerData, string> OnScoreUpdated;

    public const string DEFAULT_SCORE_TEXT = "Score:";

    private TurnValidatorController _turnValidatorController;
    private bool _init = false;

    public void Init(TurnValidatorController turnValidatorController)
    {
        if (_init) return;

        _turnValidatorController = turnValidatorController;

        SubscribeToEvents();

        _init = true;
    }

    private void SubscribeToEvents()
    {
        _turnValidatorController.OnTurnEnded += UpdatePlayerWinnerScore;
    }

    private void OnDestroy()
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
