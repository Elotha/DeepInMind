using EraSoren.InGameUI.Interfaces;
using UnityEngine;

namespace EraSoren.InGameUI
{
    public class StandardLifeEffect : MonoBehaviour, ILifeEffect
    {
        private LivesUI _livesUI;

        private void Awake()
        {
            _livesUI = GetComponent<LivesUI>();
        }

        public void IncreaseLife(int lifeNumber)
        {
            _livesUI.lifeObjects[lifeNumber].SetActive(true);
        }

        public void DecreaseLife(int lifeNumber)
        {
            _livesUI.lifeObjects[lifeNumber - 1].SetActive(false);
        }
    }
}