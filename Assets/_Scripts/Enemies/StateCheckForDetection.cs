using EraSoren.Enemies.Interfaces;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class StateCheckForDetection : MonoBehaviour, ICanDetectPlayer
    {
        private EnemyState _enemyState;

        private void Awake()
        {
            _enemyState = GetComponent<EnemyState>();
        }

        public bool CanDetectPlayer()
        {
            return _enemyState.enemyState == EnemyStates.Searching;
        }
    }
}