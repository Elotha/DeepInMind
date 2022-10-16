using System;
using System.Collections.Generic;
using UnityEngine;

namespace EraSoren.Hopelight
{
    public class HopelightSpawner : MonoBehaviour
    {
        public List<HopelightSeed> seeds = new ();

        [Serializable]
        public class HopelightSeed
        {
            public Hopelight hopelight;
            public float possibility;
            // Maybe I can add spesificied spawner object to spawn it
        }
    }
}