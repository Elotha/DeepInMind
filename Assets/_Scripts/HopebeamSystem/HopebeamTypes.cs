using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren._Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamTypes : Singleton<HopebeamTypes>
    {
        public List<HopebeamTypeHolder> hopebeamTypes = new();
        [HideInInspector] public List<HopebeamTypeHolder> soloList = new();
        [HideInInspector] public List<HopebeamTypeHolder> muteList = new();

        [Button]
        public void DestroyAllHopebeams()
        {
            foreach (var hopebeamType in hopebeamTypes)
            {
                hopebeamType.DestroyHopebeams();
            }
        }

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

            private void DealWithSolosAndMutes()
            {
                I.soloList.Clear();
                I.muteList.Clear();
                foreach (var holder in I.hopebeamTypes)
                {
                    switch (holder.soloAndMute)
                    {
                        case SoloAndMute.Solo:
                            I.soloList.Add(holder);
                            break;
                        
                        case SoloAndMute.Mute:
                            I.muteList.Add(holder);
                            break;
                    }
                }

                if (I.soloList.Count > 0)
                {
                    foreach (var holder in I.hopebeamTypes)
                    {
                        holder.hopebeamType.SetActivityOfHopebeamType(I.soloList.Contains(holder));
                    }
                }
                else
                {
                    foreach (var holder in I.hopebeamTypes)
                    {
                        holder.hopebeamType.SetActivityOfHopebeamType(!I.muteList.Contains(holder));
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