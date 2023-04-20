using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamTypes : Singleton<HopebeamTypes>
    {
        public List<HopebeamType> hopebeamTypes = new();

        public HopebeamType GetHopebeamTypeByID(string hopebeamTypeID)
        {
            return hopebeamTypes.FirstOrDefault(hopebeamType => hopebeamType.hopebeamTypeID == hopebeamTypeID);
        }
    }
}