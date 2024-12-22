using System;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialClickFadeBackingButtonCondition : TutorialClickButtonCondition
    {
        [Inject] private GuiFadeBackingButtonModule m_FadeBackingButtonModule;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            m_Button = m_FadeBackingButtonModule.Button;
            base.Initialize(abstractEntity);
        }
    }
}