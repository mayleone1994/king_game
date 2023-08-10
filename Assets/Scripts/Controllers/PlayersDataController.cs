using System.Collections;
using System.Collections.Generic;

namespace KingGame
{
    public class PlayersDataController : IController
    {
        // DEBUGGING THE POSITION OF EACH PLAYER ACCORDING TO THE BOARD
        // In the online version, this will be replaced by the PUN API player's order system
        public const int MAIN_PLAYER_SEAT_INDEX = 0;

        private PlayerData[] _playersData;

        public PlayerData[] PlayersData => _playersData;

        public void Init()
        {
            CreatePlayersData();
        }

        private void CreatePlayersData()
        {
            // Create local players
            // Needs update to online version

            _playersData = new PlayerData[GameConstants.MAX_PLAYERS];

            for (int i = 0; i < GameConstants.MAX_PLAYERS; i++)
            {
                int seatRelativeIndex = Utils.GetRelativeIndex(i, MAIN_PLAYER_SEAT_INDEX, GameConstants.MAX_PLAYERS);

                PlayerData playerData = new
                    (name: $"Player {seatRelativeIndex + 1}",
                        picture: null,
                        id: i,
                        isMainPlayer: i == 0,
                        roomIndex: seatRelativeIndex);

                _playersData[seatRelativeIndex] = playerData;
            }

        }
    }
}
