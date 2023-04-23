using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem.PackageOverrideConditions
{
    [Serializable]
    public class ConsecutivePackageException : ICondition
    {
        [field: SerializeField]
        public bool IsActive { get; set; }
        
        [SerializeField] private PackageHistory packageHistory;
        [SerializeField] private HopebeamPackage checkForThisPackage;
        [SerializeField] private int consecutivePackageCount;

        public bool EvaluateCondition()
        {
            if (packageHistory.history.Count < consecutivePackageCount) return false;
            
            var didPackageComeConsecutively = true;
            for (var i = packageHistory.history.Count - consecutivePackageCount; i < packageHistory.history.Count; i++)
            {
                if (packageHistory.history[i] != checkForThisPackage)
                {
                    didPackageComeConsecutively = false;
                }
            }

            return didPackageComeConsecutively;
        }
    }
}