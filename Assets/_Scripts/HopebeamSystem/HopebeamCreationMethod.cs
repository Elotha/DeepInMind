using System.Collections.Generic;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamCreationMethod : MonoBehaviour
    {
        public bool isActive;
        public bool isRepetable;
        public List<ConditionHolder> startConditionHolders;
        [SerializeField] private List<ConditionHolder> endConditionHolders = new();
        [SerializeField] private List<ConditionHolder> creationConditionHolders = new();
        [SerializeField] private Frequency frequency;
        [SerializeField] private List<HopebeamPackageWithWeight> hopebeamPackages = new();
        [SerializeField] private List<PackageCreationOverride> packageCreationOverrides = new();
        [SerializeField] private PackageHistory packageHistory = new();

        private bool _isDone;
        private bool _isCreating;
        

        public void SetCreationActivity(bool active)
        {
            isActive = active;
        }

        private void Update()
        {
            if (!isActive || _isDone) return;
            
            if (!_isCreating)
            {
                // if (ConditionHolder.EvaluateConditionHolders(startConditionHolders))
                // {
                //     StartCreating();
                // }
            }
            else
            {
                if (ConditionHolder.EvaluateConditionHolders(endConditionHolders))
                {
                    EndCreating();
                }
            }
        }

        private void StartCreating()
        {
            _isCreating = true;
        }

        private void Create()
        {
            if (ConditionHolder.EvaluateConditionHolders(creationConditionHolders))
            {
                // TODO: Create actual stuff
            }
        }

        private void EndCreating()
        {
            _isCreating = false;
            _isDone = !isRepetable;
        }
    }
}