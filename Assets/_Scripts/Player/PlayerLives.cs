using EraSoren._Core;
using EraSoren._Core.Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Player
{
    public class PlayerLives : Singleton<PlayerLives>
    {
        private static int Lives {
            get => _lives;
            set {
                OnLivesChange?.Invoke(_lives, value - _lives);
                _lives = value;
            }
        }
        private static int _lives;
        [SerializeField] private int maxLives = 5;
        
        #region Events
        
        public delegate void LiveChangeHandler(int notAddedDelta, int delta);
        public static event LiveChangeHandler OnLivesChange;
        
        #endregion

        private void Start()
        {
            Lives = maxLives;
        }

        public static void AlterLives(int delta)
        {
            Lives += delta;
        }
    }
}