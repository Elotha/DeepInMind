using EraSoren._CameraSystem.CameraEffects;
using EraSoren._InputSystem;
using EraSoren.Player;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Hopelight
{
    public class HopelightTeleport : MonoBehaviour
    {
        private Camera _cam;
        
        [SerializeField] private LayerMask hopelightMask;
        [SerializeField] private float inputProximityRadius;

        #region Events

        public UnityEvent<Transform> onHopelightClicked;

        #endregion

        private void Start()
        {
            _cam = Camera.main;

        }

        private void Update()
        {
            if (InputManager.GetKeyDown(InputButton.Teleport)) 
            {
                HopelightCheck();
            }
        }

        private void HopelightCheck()
        {
            var mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapCircle(mousePosition, inputProximityRadius, hopelightMask); //cursorObject.position

            if (hit) 
            {
                onHopelightClicked?.Invoke(hit.transform);
            }
        }
    }
}