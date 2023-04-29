using System;
using System.Linq;
using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamCreator : Singleton<HopebeamCreator>
    {
        public HopebeamCreationMethodList activeCreationMethodList;

        private ISpawnCondition[] _spawnConditions;

        protected override void Awake()
        {
            base.Awake();
            _spawnConditions = GetComponents<ISpawnCondition>();
        }

        public bool CanSpawn()
        {
            return _spawnConditions.All(x => x.CanSpawn());
        }

        [Button]
        public void ActivateCreation()
        {
            activeCreationMethodList.SetCreationActivity(true);
        }

        public void SetCreationActivity(bool active)
        {
            activeCreationMethodList.SetCreationActivity(active);
        }

        public void SetActiveCreationMethodList(HopebeamCreationMethodList methodList)
        {
            activeCreationMethodList.SetCreationActivity(false);
            activeCreationMethodList = methodList;
            activeCreationMethodList.SetCreationActivity(true);
        }

        public void RestartAllNextCreationTimes()
        {
            activeCreationMethodList.RestartAllNextCreationTimes();
        }
    }
}