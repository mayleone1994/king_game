using UnityEngine;

namespace KingGame
{
    public class PlayerData
    {
        private const string SAVE_KEY = "SAVED KEY";

        public readonly long ID; // TODO: became this unique
        public readonly string Name;
        public readonly Sprite Picture;
        public int Wins => GetWinsValue();

        public PlayerData(string name, Sprite picture, long id)
        {
            Name = name;
            Picture = picture;
            ID = id;
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