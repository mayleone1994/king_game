using System.Collections.Generic;
using UnityEngine;

namespace KingGame
{
    public class PlayerData
    {
        private const string SAVE_KEY = "SAVED KEY";

        private List<CardData> _cards;

        private bool isPlayerTurn;

        public readonly long ID; // TODO: became this unique
        public readonly string Name;
        public readonly Sprite Picture;
        public readonly bool IsMainPlayer;
        public readonly int RoomIndex;
        public int Wins => GetWinsValue();
        public List<CardData> Cards => _cards;
        public bool IsPlayerTurn => isPlayerTurn;

        public PlayerData(string name, Sprite picture, long id, int roomIndex, bool isMainPlayer)
        {
            Name = name;
            Picture = picture;
            ID = id;
            IsMainPlayer = isMainPlayer;
            RoomIndex = roomIndex;

            EventsSubscribe();
        }

        public void SetCards(List<CardData> cards)
        {
            _cards = cards;
        }

        private void EventsSubscribe()
        {
            TurnController.OnPlayerTurnUpdated += UpdatePlayerTimeToPlayInformation;
        }

        // To avoid memory leak
        private void EventsUnsubscribe()
        {
            TurnController.OnPlayerTurnUpdated -= UpdatePlayerTimeToPlayInformation;
        }
        private void UpdatePlayerTimeToPlayInformation(PlayerData player)
        {
            isPlayerTurn = player.ID == this.ID;
        }

        ~PlayerData()
        {
            EventsUnsubscribe();
        }

        public void AddWinValue()
        {
            string key = GetSavedKey();

            int oldValue = Wins;

            int newValue = oldValue + 1;

            PlayerPrefs.SetInt(key, newValue);

            PlayerPrefs.Save();
        }

        private int GetWinsValue()
        {
            return PlayerPrefs.GetInt(GetSavedKey());
        }

        private string GetSavedKey()
        {
            return $"{SAVE_KEY}_{ID.ToString()}";
        }
    }
}