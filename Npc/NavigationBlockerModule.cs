using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    public class NavigationBlockerModule : AbstractBehaviourModule
    {
        public event Action<List<(int, int)>> Placed = delegate { };

        [SerializeField] private IntegerObject m_BlockedId;
        [SerializeField] private Transform m_PivotTransform;
        [SerializeField] private Vector2Int m_Size;
        private bool m_IsDestroyed;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            AllowPostInitialization(2);
        }

        protected override void OnDestroyed(GameObject gameObject)
        {
            m_IsDestroyed = true;
        }

        protected override void PostInitialize()
        {
            if (!m_IsDestroyed)
            {
                OnPlaced();
            }
        }

        private void OnPlaced()
        {
            List<(int, int)> blockedIndexes = new();

            Vector2Int pivotWorldPosition = new Vector2Int(Mathf.RoundToInt(m_PivotTransform.position.x),
                Mathf.RoundToInt(m_PivotTransform.position.z));

            int sizeX = m_Size.x;
            int sizeY = m_Size.y;

            int indexModifierX = 1;
            int indexModifierY = 1;

            int i = 0;
            int j = 0;

            Predicate<int> untilX = x => x < sizeX;
            Predicate<int> untilY = y => y < sizeY;

            int angle = Mathf.RoundToInt(m_PivotTransform.localRotation.eulerAngles.y);

            if (angle == 90)
            {
                indexModifierY = -1;
                sizeX = m_Size.y;
                sizeY = -m_Size.x;
                untilY = y => y > sizeY;
            }
            else if (angle == 180)
            {
                indexModifierX = -1;
                indexModifierY = -1;
                sizeX = -m_Size.x;
                sizeY = -m_Size.y;
                untilX = x => x > sizeX;
                untilY = y => y > sizeY;
            }
            else if (angle == 270)
            {
                indexModifierX = -1;
                sizeY = m_Size.x;
                sizeX = -m_Size.y;
                untilX = x => x > sizeX;
            }


            for (i = 0; untilX(i); i += indexModifierX)
            {
                for (j = 0; untilY(j); j += indexModifierY)
                {
                    int x = pivotWorldPosition.x + i;
                    int z = pivotWorldPosition.y + j;
                    blockedIndexes.Add(new ValueTuple<int, int>(x, z));
                }
            }

            Placed(blockedIndexes);
        }
    }
}