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
        [SerializeField] private Frequency frequency;
        [SerializeField] private List<HopebeamPackageWithWeight> hopebeamPackages = new();
        [SerializeField] private List<PackageCreationOverride> packageCreationOverrides = new();
        [SerializeField] private PackageHistory packageHistory = new();

        private bool _isDone;
        private bool _isCreating;
        private bool _isActivatedByMethodList;
        private float _nextCreationTime;
        
        public void SetCreationActivity(bool active)
        {
            _isActivatedByMethodList = active;
        }

        private void Update()
        {
            if (!isActive || _isDone || !_isActivatedByMethodList) return;
            
            if (!_isCreating)
            {
                if (ConditionHolder.EvaluateConditionHolders(startConditionHolders))
                {
                    if (!ConditionHolder.EvaluateConditionHolders(endConditionHolders))
                    {
                        StartCreating();
                        Create();
                        _nextCreationTime = frequency.GetTime();
                    }
                }
            }
            else
            {
                if (ConditionHolder.EvaluateConditionHolders(endConditionHolders))
                {
                    EndCreating();
                }
                else
                {
                    _nextCreationTime -= Time.deltaTime;
                    if (_nextCreationTime <= 0f)
                    {
                        _nextCreationTime = frequency.GetTime();
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
            if (ConditionHolder.EvaluateConditionHolders(creationConditionHolders))
            {
                var packageOverride = CheckPackageCreationOverrides();
                var packageToCreate = packageOverride != null ? packageOverride.hopebeamPackage : ChooseAPackageAccordingToTheirWeight();
                packageHistory.packages.Add(packageToCreate);
                foreach (var hopebeamCreation in packageToCreate.hopebeamCreations)
                {
                    if (hopebeamCreation.delayTime > 0f)
                    {
                        StartCoroutine(CreationSequence(hopebeamCreation));
                    }
                    else
                    {
                        ActivateHopebeamSpawnProtocol(hopebeamCreation.hopebeamTypeID);
                    }
                }

            }
        }

        private static IEnumerator CreationSequence(HopebeamCreation hopebeamCreation)
        {
            yield return new WaitForSeconds(hopebeamCreation.delayTime);
            ActivateHopebeamSpawnProtocol(hopebeamCreation.hopebeamTypeID);
        }

        private static void ActivateHopebeamSpawnProtocol(string hopebeamTypeID)
        {
            var hopebeamType = HopebeamTypes.I.GetHopebeamTypeByID(hopebeamTypeID);
            hopebeamType.SpawnHopebeam();
        }

        private PackageCreationOverride CheckPackageCreationOverrides()
        {
            return packageCreationOverrides.FirstOrDefault(creationOverride => ConditionHolder.EvaluateConditionHolders(creationOverride.overrideConditionHolders));
        }

        private HopebeamPackage ChooseAPackageAccordingToTheirWeight()
        {
            var weights = new int[hopebeamPackages.Count];
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

            return hopebeamPackages[selectedPackageNo].package;
        }

        private void EndCreating()
        {
            _isCreating = false;
            _isDone = !isRepetable;
        }
    }
}