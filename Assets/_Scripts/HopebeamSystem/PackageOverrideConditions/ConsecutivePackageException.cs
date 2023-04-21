using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem.PackageOverrideConditions
{
    [Serializable]
    public class ConsecutivePackageException : ICondition
    {
        [SerializeField] private PackageHistory packageHistory;
        [SerializeField] private HopebeamPackage checkForThisPackage;
        [SerializeField] private int consecutivePackageCount;
        
        public bool EvaluateCondition()
        {
            if (packageHistory.packages.Count < consecutivePackageCount) return false;
            
            var didPackageComeConsecutively = true;
            for (var i = packageHistory.packages.Count - consecutivePackageCount; i < packageHistory.packages.Count; i++)
            {
                if (packageHistory.packages[i] != checkForThisPackage)
                {
                    didPackageComeConsecutively = false;
                }
            }

            return didPackageComeConsecutively;
        }
    }
}