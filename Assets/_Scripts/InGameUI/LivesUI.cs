using System.Collections.Generic;
using EraSoren._Core.Helpers;
using EraSoren.InGameUI.Interfaces;
using EraSoren.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.InGameUI
{
    public class LivesUI : MonoBehaviour
    {
        [SerializeField] private GameObject lifePrefab;
        [SerializeField] private float originX;
        [SerializeField] private float originY;
        
        [OnValueChanged(nameof(AdjustGaps))]
        [SerializeField] private float gap;
        [SerializeField] private Transform lifeParent;
        
        public List<GameObject> lifeObjects = new ();

        private ILifeEffect _lifeEffect;
        private void Awake()
        {
            _lifeEffect = GetComponent<ILifeEffect>();
            PlayerLives.OnLivesChange += UpdateLivesUI;
        }

        private void OnDestroy()
        {
            PlayerLives.OnLivesChange -= UpdateLivesUI;
        }

        private void UpdateLivesUI(int notAddedDelta, int delta)
        {
            // TODO: Maybe this can be more readable
            var sign = Ext.Sign(delta);
            if (sign == 1)
            {
                for (var i = 0; i < delta; i++)
                {
                    if (notAddedDelta + 1 > lifeObjects.Count)
                    {
                        lifeObjects.Add(CreateNewLifeObject(notAddedDelta));
                    }

                    _lifeEffect.IncreaseLife(notAddedDelta++);
                }
            }
            else
            {
                for (var i = 0; i < Mathf.Abs(delta); i++)
                {
                    _lifeEffect.DecreaseLife(notAddedDelta--);
                }
            }
        }

        private GameObject CreateNewLifeObject(int lifeNumber)
        {
            var pos = new Vector2(originX + (lifeNumber * gap), originY);
            var life = Instantiate(lifePrefab, Vector3.zero, Quaternion.identity, lifeParent);
            life.transform.localPosition = pos;
            return life;
        }

        // This is called on value changed
        private void AdjustGaps()
        {
            for (var i = 0; i < lifeObjects.Count; i++)
            {
                var pos = new Vector2(originX + (i * gap), originY);
                lifeObjects[i].transform.localPosition = pos;
            }
        }
    }
}