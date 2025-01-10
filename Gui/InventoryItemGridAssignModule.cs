using System;

namespace _Project.Scripts
{
    [Serializable]
    public class InventoryItemGridAssignModule : AbstractBehaviourModule
    {
        private int m_GridTypesMask = 0;

        public void AssignNewGrid(GridFillInventoryType gridFillInventoryType)
        {
            var id = (int)gridFillInventoryType;
            m_GridTypesMask = 1 << id;
        }

        public bool IsGridAssigned(GridFillInventoryType gridFillInventoryType)
        {
            var id = (int)gridFillInventoryType;
            return (m_GridTypesMask & id) != 0;
        }
    }
}