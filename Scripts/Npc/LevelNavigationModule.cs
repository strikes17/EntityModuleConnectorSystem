using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AStar;
using AStar.Heuristics;
using AStar.Options;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class LevelNavigationModule : AbstractBehaviourModule
    {
        [SerializeField] private IntegerObject m_BlockedId;
        [SerializeField] private IntegerObject m_UnblockedId;
        [SerializeField] private PoolableGameObject m_DebugDrawGameObject;

        private Pool<PoolableGameObject> m_Pool;

        private Dictionary<int, PathFinder> m_PathFinders;

        private int m_SizeX, m_SizeY;

        private bool m_IsDebugDrawn;

        private WorldGrid m_WorldGrid;

        [Button]
        private void DebugDraw()
        {
            if (m_Pool == null)
            {
                m_Pool = new Pool<PoolableGameObject>(m_DebugDrawGameObject);
            }

            m_IsDebugDrawn = !m_IsDebugDrawn;

            if (m_IsDebugDrawn)
            {
                for (int i = 0; i < m_SizeX; i++)
                {
                    for (int j = 0; j < m_SizeY; j++)
                    {
                        if (m_WorldGrid[j, i] == m_BlockedId.Value)
                        {
                            var instance = m_Pool.Get();
                            instance.transform.position = new Vector3(i, 0, j);
                            instance.Enable();
                        }
                    }
                }
            }
            else
            {
                m_Pool.DisableAll();
            }
        }

        public void CreateNavigationMap(int sizeX, int sizeY)
        {
            m_SizeX = sizeX;
            m_SizeY = sizeY;
            var navigationMap = new short[sizeX, sizeY];
            m_PathFinders = new Dictionary<int, PathFinder>();
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    navigationMap[i, j] = (short)m_UnblockedId.Value;
                }
            }

            m_WorldGrid = new WorldGrid(navigationMap);
        }

        public List<Vector2Int> FindPath(Vector2Int from, Vector2Int to, int hashCode)
        {
            if (!m_PathFinders.TryGetValue(hashCode, out PathFinder pathFinder))
            {
                PathFinderOptions pathfinderOptions = new PathFinderOptions()
                {
                    PunishChangeDirection = false,
                    UseDiagonals = false
                };

                pathFinder = new PathFinder(m_WorldGrid, pathfinderOptions);
                m_PathFinders.Add(hashCode, pathFinder);
            }

            var path = pathFinder.FindPath(new Point(from.x, from.y), new Point(to.x, to.y));

            var result = path.Select(x => new Vector2Int(x.X, x.Y)).ToList();

            if (result.Count == 0)
            {
                Debug.LogError($"Path was not found!");
            }

            return result;
        }

        public bool IsCellBlocked(int x, int y)
        {
            if (x < m_SizeX && y < m_SizeY && x >= 0 && y >= 0)
            {
                return m_WorldGrid[y, x] == 0;
            }
            else
            {
                Debug.LogError($"Trying to reach out of bounds! {x},{y}");
                return true;
            }
        }

        public void BlockCell(int x, int y)
        {
            if (x < m_SizeX && y < m_SizeY && x >= 0 && y >= 0)
            {
                var blockedIdValue = (short)m_BlockedId.Value;
                m_WorldGrid[y, x] = blockedIdValue;
            }
            else
            {
                // Debug.LogError($"Trying to reach out of bounds! {x},{y}");
            }
        }

        public void UnblockCell(int x, int y)
        {
            if (x < m_SizeX && y < m_SizeY && x >= 0 && y >= 0)
            {
                var unblockedIdValue = (short)m_UnblockedId.Value;
                m_WorldGrid[y, x] = unblockedIdValue;
            }
            else
            {
                // Debug.LogError($"Trying to reach out of bounds! {x},{y}");
            }
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            m_AbstractEntity = abstractEntity;
            CreateNavigationMap(8, 8);
        }
    }
}