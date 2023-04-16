using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class ProvideMinMaxFloat : IProvideFloatField
    {
        public Vector2 randomizedTimeInBetween;

        public float GetTime()
        {
            return Random.Range(randomizedTimeInBetween.x, randomizedTimeInBetween.y);
        }
    }
}