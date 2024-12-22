using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class StateSwitcherCompoundModule : AbstractStateSwitcherModule
    {
        private List<AbstractStateSwitcherModule> m_States = new();

        public override int MaxState => m_States.Select(x => x.MaxState).FirstOrDefault();

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_States = new List<AbstractStateSwitcherModule>();
            var switcherModules =
                m_AbstractEntity.GetBehaviorModulesCollectionByType<AbstractStateSwitcherModule>();
            switcherModules.Remove(this);
            m_States.AddRange(switcherModules);
        }

        protected override void OnStateChanged()
        {
            foreach (var switcherModule in m_States)
            {
                switcherModule.State = State;
            }
        }
    }
}