using System.Linq;
using EraSoren.Enemies.Interfaces;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        private IDetectPlayer _detectPlayer;
        private ICanDetectPlayer[] _canDetectPlayer;
        
        public IEnemyAttack attack;

        private void Awake()
        {
            _detectPlayer = GetComponentInChildren<IDetectPlayer>();
            _canDetectPlayer = GetComponents<ICanDetectPlayer>();
            attack = GetComponent<IEnemyAttack>();
        }

        private void FixedUpdate()
        {
            RangeCheck();
        }

        private void RangeCheck()
        {
            if (_canDetectPlayer.Any(canDetectPlayer => !canDetectPlayer.CanDetectPlayer()))
            {
                return;
            }

            if (_detectPlayer.IsPlayerInRegion())
            {
                attack.Attack();
            }
        }
    }
}