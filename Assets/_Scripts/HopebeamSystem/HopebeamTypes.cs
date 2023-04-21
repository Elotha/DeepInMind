using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren._Core.Utilities;
using Sirenix.OdinInspector;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamTypes : Singleton<HopebeamTypes>
    {
        public List<HopebeamTypeHolder> hopebeamTypes = new();

        [Serializable]
        public class HopebeamTypeHolder
        {
            public HopebeamType hopebeamType;

            [OnValueChanged(nameof(DealWithSolosAndMutes))]
            public SoloAndMute soloAndMute;

            [Button]
            public void DestroyHopebeams()
            {
                hopebeamType.GetComponent<DestroyChildObjects>().DestroyChilds();
            }

            private List<HopebeamTypeHolder> _soloList = new();
            private List<HopebeamTypeHolder> _muteList = new();

            private void DealWithSolosAndMutes()
            {
                _soloList.Clear();
                _muteList.Clear();
                foreach (var holder in I.hopebeamTypes)
                {
                    switch (holder.soloAndMute)
                    {
                        case SoloAndMute.Solo:
                            _soloList.Add(holder);
                            break;
                        
                        case SoloAndMute.Mute:
                            _muteList.Add(holder);
                            break;
                    }
                }

                if (_soloList.Count > 0)
                {
                    foreach (var holder in I.hopebeamTypes)
                    {
                        holder.hopebeamType.SetActivityOfHopebeamType(_soloList.Contains(holder));
                    }
                }
                else
                {
                    foreach (var holder in I.hopebeamTypes)
                    {
                        holder.hopebeamType.SetActivityOfHopebeamType(!_muteList.Contains(holder));
                    }
                }
            }
        }

        public HopebeamType GetHopebeamTypeByID(string hopebeamTypeID)
        {
            return hopebeamTypes.FirstOrDefault(x => x.hopebeamType.hopebeamTypeID == hopebeamTypeID)?.hopebeamType;
        }
    }
}