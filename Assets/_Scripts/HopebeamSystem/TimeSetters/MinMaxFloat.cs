using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.HopebeamSystem.TimeSetters
{
    [Serializable]
    public class ProvideMinMaxFloat : IProvideTime
    {
        public Vector2 randomizeTimeInBetween;

        public float GetTime()
        {
            return Random.Range(randomizeTimeInBetween.x, randomizeTimeInBetween.y);
        }
    }
}