using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._CameraSystem;
using EraSoren._Core.Helpers;
using EraSoren._InputSystem;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class CatchingManager : Singleton<CatchingManager>
    {
        [SerializeField] private LayerMask beamMask;
        [SerializeField] private float catchingRadius = 0.1f;
        public bool isSecondaryInputActiveForHopebeams;

        #region Events

        public Action<CatchingInfo> onPrimaryInputCatch;
        public Action<CatchingInfo> onSecondaryInputCatch;

        #endregion

        [Serializable]
        public struct CatchingInfo
        {
            public Vector2 catchingPos;
            public List<Hopebeam> hopebeams;

            public CatchingInfo(Vector2 catchingPos, List<Hopebeam> hopebeams)
            {
                this.catchingPos = catchingPos;
                this.hopebeams = hopebeams;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private CatchingInfo TryToCatch()
        {
            var catchingPos = CameraController.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            List<Hopebeam> beams = new ();
            var overlapCircles = Physics2D.OverlapCircleAll(catchingPos, catchingRadius, beamMask);
            if (overlapCircles.Length > 0)
            {
                beams.AddRange(overlapCircles.Select(circle => circle.GetComponent<Hopebeam>()));
            }
            // Debug.DrawLine(catchingPos, catchingPos + Vector3.up * catchingRadius / 2f, Color.cyan, 2f);
            var catchingInfo = new CatchingInfo(catchingPos, beams);
            return catchingInfo;
        }

        private void Update()
        {
            ProcessPrimaryInput();
            ProcessSecondaryInput();
        }
        private void ProcessPrimaryInput()
        {
            if (!InputManager.GetKeyDown(InputButton.Primary)) return;
            
            var catchingInfo = TryToCatch();
            onPrimaryInputCatch?.Invoke(catchingInfo);

            TryToInteractWithHopebeam(catchingInfo, true);
        }

        private void ProcessSecondaryInput()
        {
            if (!isSecondaryInputActiveForHopebeams) return;
            if (!InputManager.GetKeyDown(InputButton.Secondary)) return;
            
            var catchingInfo = TryToCatch();
            onSecondaryInputCatch?.Invoke(catchingInfo);

            TryToInteractWithHopebeam(catchingInfo, false);
        }

        private static void TryToInteractWithHopebeam(CatchingInfo catchingInfo, bool primaryInput)
        {
            var beams = catchingInfo.hopebeams;
            var maxPriority = beams.Select(beam => beam.catchPriority).Prepend(0).Max();

            List<Hopebeam> beamsToRemove = new();
            foreach (var beam in beams.Where(beam => beam.catchPriority == maxPriority))
            {
                // Debug.Log("hopebeamType = " + (beam.hopebeamType != null));
                // Debug.Log("cathingPos = " + (catchingInfo.catchingPos));
                var isCatchingValid = beam.hopebeamType.TryToInteract(beam, catchingInfo.catchingPos, primaryInput);
                if (!isCatchingValid)
                {
                    beamsToRemove.Add(beam);
                }
            }
            
            // var beamsToRemoveFromList = (from beam in beams.Where(beam => beam.catchPriority == maxPriority) 
            //     let isCatchingValid = beam.hopebeamType.TryToInteract(beam, catchingInfo.catchingPos, primaryInput) 
            //     where !isCatchingValid select beam).ToList();

            foreach (var hopebeam in beamsToRemove)
            {
                catchingInfo.hopebeams.Remove(hopebeam);
            }
        }
    }
}