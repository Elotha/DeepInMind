using System.Collections.Generic;
using EraSoren.Enemies.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.Enemies
{
    public class RandomWalkInFourDirections : MonoBehaviour, IChoosePath
    {
        [SerializeField] private float minimumMovementDistance = 1f;
        
        private EnemyMovement _enemyMovement;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
        }

        public Vector2 ChooseNewDestination()
        {
            var emptyPaths = new List<Vector2>();
            var paths = new bool[4];
            var direction = Vector2.right;
            for (var i = 0; i < 4; i++)
            {
                paths[i] = _enemyMovement.Walkable.CanWalkOntoPosition(direction * minimumMovementDistance);
                
                if (paths[i]) 
                    emptyPaths.Add(direction);
                
                direction = Vector2.Perpendicular(direction);
            }
            
            if (emptyPaths.Count > 0) 
            {
                var rnd = Random.Range(0,emptyPaths.Count);
                var repetition = Random.Range(1,4);
                
                return emptyPaths [rnd] * (minimumMovementDistance * repetition);
            }
            return Vector2.zero;
        }
    }
}