using System.Collections.Generic;
using UnityEngine;

namespace KingGame
{
    public class PlayerData
    {
        private const string SAVE_KEY = "SAVED KEY";

        private List<CardData> _cardsOnHand;

        private bool _isPlayerTurn;

        private int _currentScore = 0;

        public readonly long ID; // TODO: became this unique
        public readonly string Name;
        public readonly Sprite Picture;
        public readonly bool IsMainPlayer;
        public readonly int RoomIndex;
        public int Wins => GetWinsValue();
        public List<CardData> CardsOnHand => _cardsOnHand;
        public bool IsPlayerTurn => _isPlayerTurn;
        public int CurrentScore => _currentScore;

        public PlayerData(string name, Sprite picture, long id, int roomIndex, bool isMainPlayer)
        {
            Name = name;
            Picture = picture;
            ID = id;
            IsMainPlayer = isMainPlayer;
            RoomIndex = roomIndex;

            EventsSubscribe();
        }

        public void SetCardsOnHand(List<CardData> cards)
        {
            _cardsOnHand = cards;
        }

        public void UpdateScore(int newValue)
        {
            _currentScore += newValue;
        }

        private void EventsSubscribe()
        {
            TurnController.OnNextPlayer += UpdatePlayerTurn;
        }

        // To avoid memory leak
        private void EventsUnsubscribe()
        {
            TurnController.OnNextPlayer -= UpdatePlayerTurn;
        }
        private void UpdatePlayerTurn(PlayerData player)
        {
            _isPlayerTurn = player.ID == this.ID;
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