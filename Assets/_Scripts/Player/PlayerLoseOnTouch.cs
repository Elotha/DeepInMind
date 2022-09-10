using System.Collections;
using System.Collections.Generic;
using RegularDuck._GameStateControl;
using UnityEngine;

namespace RegularDuck._Player
{
    public class PlayerLoseOnTouch : MonoBehaviour
    {
        public LayerMask TargetLayers;
        private void Start()
        {
            PlayerCollisions.OnPlayerCollisionEnter += LoseOnTouchCollider;
            PlayerCollisions.OnPlayerTriggerEnter += LoseOnTouchTrigger;
        }
        public void LoseOnTouchCollider(Collision other)
        {
            if (TargetLayers == (TargetLayers | 1 << other.gameObject.layer))
            {
                GameStateControl.I.LoseGame();
            }
        }

        public void LoseOnTouchTrigger(Collider other)
        {
            if (TargetLayers == (TargetLayers | 1 << other.gameObject.layer))
            {
                GameStateControl.I.LoseGame();
            }
        }
    }
}
