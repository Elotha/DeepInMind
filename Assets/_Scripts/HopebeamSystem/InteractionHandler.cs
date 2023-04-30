using System;
using System.Collections.Generic;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class InteractionHandler
    {
        public bool isActive;
        public InteractionType interactionType;
        public List<InteractionConditionHolder> interactionConditionHolders = new();
        public List<HopebeamInteractionBehaviour> actions;

        public void Interact(Hopebeam hopebeam, Vector2 catchingPos)
        {
            foreach (var behaviour in actions)
            {
                behaviour.Interact(hopebeam, catchingPos);
            }
        }
    }

    public enum InteractionType
    {
        PrimaryInputDown,
        PrimaryInput,
        PrimaryInputUp,
        SecondaryInputDown,
        SecondaryInput,
        SecondaryInputUp,
        MouseEnter,
        MouseExit
    }
}