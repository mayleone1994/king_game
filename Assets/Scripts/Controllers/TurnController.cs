using System;
using System.Collections.Generic;
using UnityEngine;

namespace KingGame
{
    public class TurnController : SubscriberBase, IController, IDependent<PlayersDataController>
    {
        public static event Action<PlayerData> OnNextPlayer;

        private PlayersDataController _playersDataController;

        private Queue<PlayerData> _playersOrder;

        public void SetDependency(PlayersDataController dependency)
        {
            _playersDataController = dependency;
        }

        public void Init()
        {
            UpdatePlayersOrder();

            if (_init) return;

            SubscribeToEvents();

            _init = true;
        }

        protected override void SubscribeToEvents()
        {
            TurnValidatorController.OnNextPlayer += UpdatePlayerTurn;
            TurnValidatorController.OnTurnEnded += UpdatePlayersOrder;
        }

        protected override void UnsubscribeToEvents()
        {
            TurnValidatorController.OnNextPlayer -= UpdatePlayerTurn;
            TurnValidatorController.OnTurnEnded -= UpdatePlayersOrder;
        }

        public void UpdatePlayersOrder(PlayerData playerData = null)
        {
            _playersOrder = new();

            PlayerData[] players = _playersDataController.PlayersData;

            int max = players.Length;

            int playerIndex = playerData == null
                ? UnityEngine.Random.Range(0, max)
                : playerData.RoomIndex;

            for (int i = 0; i < max; i++)
            {
                int relativeIndex = Utils.GetRelativeIndex(i, playerIndex, max);

                _playersOrder.Enqueue(players[relativeIndex]);
            }

            List<PlayerData> playerOrderDebug = new(_playersOrder);

            playerOrderDebug.ForEach(p => { Debug.Log($"Player turn order: {p.Name}"); });

            UpdatePlayerTurn();
        }

        private void UpdatePlayerTurn()
        {
            PlayerData currentPlayer = _playersOrder.Dequeue();

            OnNextPlayer?.Invoke(currentPlayer);
        }
    }
}
