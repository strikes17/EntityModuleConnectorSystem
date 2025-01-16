﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class InventoryGridValidationModule : AbstractBehaviourModule
    {
        public event Action<Vector2Int, AbstractUsableItem> ItemPlacedInGrid = delegate { };
        public event Action<Vector2Int> GridCellUnlocked = delegate { };

        private Dictionary<Vector2Int, AbstractUsableItem> m_Grid;

        public List<(Vector2Int, AbstractUsableItem)> AllItems => m_Grid.Select(x => (x.Key, x.Value)).ToList();

        public int UnlockedCellsCount
        {
            get => m_UnlockedCellsCount;
            set
            {
                Debug.Log($"Value: {value}, current: {m_UnlockedCellsCount}");
                if (value > m_UnlockedCellsCount)
                {
                    // var lastUnlockedCellPosition =
                    //     new Vector2Int((m_UnlockedCellsCount - 1) % 9, (m_UnlockedCellsCount - 1) / 9);
                    // Vector2Int nextUnlockedCellPosition = Vector2Int.zero;
                    // var lastUnlockedXPosition = lastUnlockedCellPosition.x + 1;
                    // nextUnlockedCellPosition.x = lastUnlockedXPosition % 9;
                    // nextUnlockedCellPosition.y = lastUnlockedXPosition / 9;


                    for (int i = m_UnlockedCellsCount; i < value; i++)
                    {
                        int x = i % 9;
                        int y = i / 9;
                        var vector2Int = new Vector2Int(x, y);
                        m_Grid.Add(vector2Int, null);
                        GridCellUnlocked(vector2Int);
                    }


                    m_UnlockedCellsCount = value;
                }
            }
        }

        private int m_UnlockedCellsCount;

        public Vector2Int GetCellPositionByIndex(int i)
        {
            int x1 = m_UnlockedCellsCount % 9;
            int y1 = m_UnlockedCellsCount / 9;

            int x = i % 9;
            int y = i / 9;

            if (y < y1)
            {
                var lastCellPosition = new Vector2Int(x, y);
                return lastCellPosition;
            }

            if (y != y1) return new Vector2Int(-1, -1);
            {
                if (x > x1) return new Vector2Int(-1, -1);
                var lastCellPosition = new Vector2Int(x, y);
                return lastCellPosition;
            }
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_Grid = new();
            AllowPostInitialization(10);
        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
            UnlockedCellsCount = 120;
        }

        public Vector2Int CanAddItemToInventory(AbstractPickableItemDataObject dataObject)
        {
            var cantAddItemPosition = new Vector2Int(-1, -1);
            var inventoryItemEntity = dataObject.InventoryItemEntityPrefab;
            var size = inventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>().SizeInGrid;
            int yMax = m_UnlockedCellsCount / 9 + 1;

            bool canFit = size.x + size.y <= m_UnlockedCellsCount;
            Debug.Log($"maxCells: {m_UnlockedCellsCount}, size: {size}, canFit: {canFit}");

            if (!canFit)
            {
                return cantAddItemPosition;
            }


            for (int i = 0; i < yMax; i++)
            {
                int xMax = 0;
                if (i == yMax - 1)
                {
                    xMax = m_UnlockedCellsCount % 9 + 1;
                }
                else
                {
                    xMax = 9;
                }

                for (int j = 0; j < xMax; j++)
                {
                    m_Grid.TryGetValue(new Vector2Int(j, i), out AbstractUsableItem item);
                    bool isFree = item == null;
                    if (!isFree)
                    {
                        continue;
                    }

                    if (size.x == 1 && size.y == 1)
                    {
                        return new Vector2Int(j, i);
                    }

                    int xCellsLeft = xMax - j;
                    if (size.x > xCellsLeft)
                    {
                        break;
                    }

                    bool isOccupied = false;
                    for (int y = 0; y < size.y; y++)
                    {
                        for (int x = 0; x < size.x; x++)
                        {
                            m_Grid.TryGetValue(new Vector2Int(j + x, i + y), out item);
                            isFree = item == null;
                            Debug.Log($"At cell: {j + x},{i + y} is free: {isFree}");
                            if (!isFree)
                            {
                                isOccupied = true;
                                break;
                            }
                        }

                        if (isOccupied)
                        {
                            break;
                        }
                    }

                    if (!isOccupied)
                    {
                        return new Vector2Int(j, i);
                    }
                }
            }

            return cantAddItemPosition;
        }

        public void PlaceItemInGrid(AbstractUsableItem abstractUsableItem, Vector2Int gridPosition)
        {
            var size = abstractUsableItem.InventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>().SizeInGrid;
            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    var position = new Vector2Int(gridPosition.x + j, gridPosition.y + i);
                    m_Grid[position] = abstractUsableItem;
                }
            }

            ItemPlacedInGrid(gridPosition, abstractUsableItem);
        }
    }
}