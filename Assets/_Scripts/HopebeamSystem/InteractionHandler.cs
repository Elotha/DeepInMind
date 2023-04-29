using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class InteractionHandler
    {
        public bool isActive;
        public InteractionType interactionType;
        public List<InteractionConditionHolder> interactionConditionHolders = new();
        public UnityEvent<Hopebeam, Vector2> onInteraction;

        public void Interact(Hopebeam hopebeam, Vector2 catchingPos)
        {
            onInteraction?.Invoke(hopebeam, catchingPos);
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