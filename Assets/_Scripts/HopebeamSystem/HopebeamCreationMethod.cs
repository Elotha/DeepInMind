using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private TimeSetter timeSetter;
        [SerializeField] private List<HopebeamPackageWithWeight> hopebeamPackages = new();
        [SerializeField] private List<PackageCreationOverride> packageCreationOverrides = new();
        [SerializeField] private PackageHistory packageHistory;

        private bool _isDone;
        private bool _isCreating;
        private bool _isActivatedByMethodList;
        private float _nextCreationTime;
        
        public void SetCreationActivity(bool active)
        {
            _isActivatedByMethodList = active;
            if (!active && _isCreating)
            {
                EndCreating();
            }
        }

        private void Update()
        {
            if (!isActive || _isDone || !_isActivatedByMethodList) return;
            
            if (!_isCreating)
            {
                // Start Conditions
                var startConditions = ConditionHolder.EvaluateConditionHolders(startConditionHolders);
                if (startConditions is not (ConditionHolder.ConditionResult.ConditionsAreMet
                    or ConditionHolder.ConditionResult.NoActiveConditionHolders)) return;
               
                // End Conditions
                var endConditions = ConditionHolder.EvaluateConditionHolders(endConditionHolders);
                if (endConditions is not (ConditionHolder.ConditionResult.ConditionsAreNotMet
                    or ConditionHolder.ConditionResult.NoActiveConditionHolders)) return;
                
                StartCreating();
                Create();
                _nextCreationTime = timeSetter.GetTime();
            }
            else
            {
                var endConditions = ConditionHolder.EvaluateConditionHolders(endConditionHolders);
                if (endConditions == ConditionHolder.ConditionResult.ConditionsAreMet)
                {
                    EndCreating();
                }
                else
                {
                    _nextCreationTime -= Time.deltaTime;
                    if (_nextCreationTime <= 0f)
                    {
                        _nextCreationTime = timeSetter.GetTime();
                        Create();
                    }
                }
            }
        }

        private void StartCreating()
        {
            _isCreating = true;
        }

        private void Create()
        {
            var creationConditions = ConditionHolder.EvaluateConditionHolders(creationConditionHolders);
            if (creationConditions == ConditionHolder.ConditionResult.ConditionsAreNotMet) return;
            
            var packageOverride = CheckPackageCreationOverrides();
            var packageToCreate = packageOverride != null ? packageOverride.hopebeamPackage : ChooseAPackageAccordingToTheirWeight();

            var packageConditions = ConditionHolder.EvaluateConditionHolders(packageToCreate.packageConditionHolders);
            if (packageConditions == ConditionHolder.ConditionResult.ConditionsAreNotMet) return;
            
            packageHistory.history.Add(packageToCreate);
            foreach (var hopebeamCreation in packageToCreate.hopebeamCreations)
            {
                var time = hopebeamCreation.delayTime.GetTime();
                if (time > 0f)
                {
                    StartCoroutine(CreationSequence(hopebeamCreation));
                }
                else
                {
                    ActivateHopebeamSpawnProtocol(hopebeamCreation.hopebeamTypeID);
                }
            }
        }

        private static IEnumerator CreationSequence(HopebeamCreation hopebeamCreation)
        {
            var time = hopebeamCreation.delayTime.GetTime();
            yield return new WaitForSeconds(time);
            if (HopebeamCreator.I.CanSpawn())
            {
                ActivateHopebeamSpawnProtocol(hopebeamCreation.hopebeamTypeID);
            }
        }

        private static void ActivateHopebeamSpawnProtocol(string hopebeamTypeID)
        {
            var hopebeamType = HopebeamTypes.I.GetHopebeamTypeByID(hopebeamTypeID);
            hopebeamType.SpawnHopebeam();
        }

        private PackageCreationOverride CheckPackageCreationOverrides()
        {
            var availablePackageCreationOverrides = packageCreationOverrides.Where(creationOverride => creationOverride.isActive).ToList();
            
            return (from creationOverride in availablePackageCreationOverrides 
                let creationOverrideConditions = ConditionHolder.EvaluateConditionHolders(creationOverride.overrideConditionHolders) 
                where creationOverrideConditions == ConditionHolder.ConditionResult.ConditionsAreMet 
                select creationOverride).FirstOrDefault();
        }

        private HopebeamPackage ChooseAPackageAccordingToTheirWeight()
        {
            var availablePackages = (from packageWithWeight in hopebeamPackages 
                let packageConditions = ConditionHolder.EvaluateConditionHolders(packageWithWeight.package.packageConditionHolders) 
                where packageConditions is ConditionHolder.ConditionResult.ConditionsAreMet or ConditionHolder.ConditionResult.NoActiveConditionHolders 
                select packageWithWeight).ToList();
            
            var weights = new int[availablePackages.Count];
            for (var i = 0; i < weights.Length; i++)
            {
                weights[i] = hopebeamPackages[i].weight;
            }
            var totalWeight = weights.Sum();
            var selectedPackageNo = 0;
            var rnd = Random.Range(0f, totalWeight);
            if (rnd <= weights[0])
            {
                selectedPackageNo = 0;
            }
            else
            {
                var weightSum = weights[0];
                for (var i = 1; i < weights.Length; i++)
                {
                    if (rnd > weightSum && rnd <= weightSum + weights[i])
                    {
                        selectedPackageNo = i;
                    }

                    weightSum += weights[i];
                }
            }

            return availablePackages[selectedPackageNo].package;
        }

        private void EndCreating()
        {
            _isCreating = false;
            _isDone = !isRepetable;
        }

        public void RestartNextCreationTime()
        {
            _nextCreationTime = 0f;
        }
    }
}