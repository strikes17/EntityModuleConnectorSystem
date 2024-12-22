using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerBrushTowerContextMenuConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerBrushModule m_PlayerBrushModule;
        [Inject] private TowerContextMenuModule m_TowerContextMenuModule;

        protected override void Initialize()
        {
            m_TowerContextMenuModule.Opened += TowerContextMenuModuleOnOpened;
            m_TowerContextMenuModule.Closed += TowerContextMenuModuleOnClosed;
        }

        private void TowerContextMenuModuleOnClosed()
        {
            Moroutine.Run(Delay(() => m_PlayerBrushModule.TryUnblockPainting(GetHashCode())));
        }

        private IEnumerator Delay(Action action)
        {
            yield return null;
            action();
        }

        private void TowerContextMenuModuleOnOpened()
        {
            m_PlayerBrushModule.TryBlockPainting(GetHashCode());
        }
    }
}