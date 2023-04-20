using System;
using EraSoren._CameraSystem;
using EraSoren._Core.Helpers;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class CatchingManager : Singleton<CatchingManager>
    {
        [SerializeField] private LayerMask beamMask;
        [SerializeField] private float catchingRadius = 0.1f;

        [Serializable]
        public struct CatchingInfo
        {
            public Vector2 catchingPos;
            public Hopebeam hopebeam;
            public bool didPlayerCatchBeam;

            public CatchingInfo(Vector2 catchingPos, Hopebeam hopebeam, bool didPlayerCatchBeam)
            {
                this.catchingPos = catchingPos;
                this.hopebeam = hopebeam;
                this.didPlayerCatchBeam = didPlayerCatchBeam;
            }
        }

        public CatchingInfo TryToCatch()
        {
            var catchingPos = CameraController.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Hopebeam beam = null;
            var overlapCircle = Physics2D.OverlapCircle(catchingPos, catchingRadius, beamMask);
            if (overlapCircle != null)
            {
                beam = overlapCircle.GetComponent<Hopebeam>();
            }
            Debug.DrawLine(catchingPos, catchingPos + Vector3.up * catchingRadius / 2f, Color.cyan, 2f);
            var catchingInfo = new CatchingInfo(catchingPos, beam, beam != null);
            return catchingInfo;
        }
    }
}