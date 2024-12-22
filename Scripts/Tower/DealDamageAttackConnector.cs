using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class DealDamageAttackConnector : BehaviourModuleConnector
    {
        [SerializeField] private AbstractEntity m_AbstractEntity;
        [SerializeField] private AbstractEntity m_EntityContainer;

        [SelfInject] private DealDamageModule m_DealDamageModule;
        [SelfInject] private AttackSingleTargetSelectorModule m_AttackSingleTargetSelectorModule;
        [SelfInject] private DamageModule m_DamageModule;
        [SelfInject] private AttackReloadModule m_AttackReloadModule;
        [SelfInject] private AttackRangeModule m_AttackRangeModule;
        [Inject] private EntityContainerModule m_EntityContainerModule;
        [SelfInject] private OwnerModule m_OwnerModule;
        [SelfInject] private AbstractAttackVfxModule m_AttackVfxModule;

        private AbstractBone m_VfxTargetBone;
        private BonesModule m_VfxTargetBoneModule;

        protected override void Initialize()
        {
            m_AttackSingleTargetSelectorModule.TargetSelected += AttackSingleTargetSelectorModuleOnTargetSelected;
            m_AttackSingleTargetSelectorModule.TargetLost += AttackSingleTargetSelectorModuleOnTargetLost;

            m_DealDamageModule.DamageApplied += DealDamageModuleOnDamageApplied;

            m_EntityContainerModule.ElementAdded += EntityContainerModuleOnElementAdded;
            m_EntityContainerModule.ElementRemoved += EntityContainerModuleOnElementRemoved;

            var possibleTargets = m_EntityContainerModule.ContainerCollection.ToList();
            possibleTargets = possibleTargets.Where(x =>
            {
                var ownerModule = x.GetValueModuleByType<OwnerModule>();
                if (ownerModule != null)
                {
                    return ownerModule.Value != m_OwnerModule.Value;
                }

                return false;
            }).ToList();
            possibleTargets.Remove(m_AbstractEntity);

            m_AttackSingleTargetSelectorModule.SetPossibleTargets(possibleTargets);
            m_AttackSingleTargetSelectorModule.SetAttackRange(m_AttackRangeModule.Value);
        }

        private void EntityContainerModuleOnElementRemoved(AbstractEntity abstractEntity)
        {
            m_AttackSingleTargetSelectorModule.RemovePossibleTarget(abstractEntity);
        }

        private void EntityContainerModuleOnElementAdded(AbstractEntity abstractEntity)
        {
            if (m_AbstractEntity != abstractEntity)
            {
                var ownerModule = abstractEntity.GetValueModuleByType<OwnerModule>();
                if (ownerModule != null)
                {
                    if (ownerModule.Value != m_OwnerModule.Value)
                    {
                        m_AttackSingleTargetSelectorModule.AddPossibleTarget(abstractEntity);
                    }
                }
            }
        }

        private void DealDamageModuleOnDamageApplied(AbstractEntity abstractEntity)
        {
            m_VfxTargetBoneModule = abstractEntity.GetBehaviorModuleByType<BonesModule>();
            if (m_AttackVfxModule.GetType() == typeof(ProjectileAttackVfxModule))
            {
                m_VfxTargetBone = m_VfxTargetBoneModule.GetBone<BodyBone>();
            }
            m_AttackVfxModule.Start(abstractEntity, m_VfxTargetBone);
            m_AttackVfxModule.ReachedTarget -= AttackVfxModuleOnReachedTarget;
            m_AttackVfxModule.ReachedTarget += AttackVfxModuleOnReachedTarget;
        }

        private void AttackVfxModuleOnReachedTarget(AbstractEntity abstractEntity)
        {
            var healthModule = abstractEntity.GetValueModuleByType<HealthModule>();
            if (healthModule != null)
            {
                healthModule.Value -= m_DamageModule.Value;
            }
        }

        private void AttackSingleTargetSelectorModuleOnTargetLost(AbstractEntity obj)
        {
            m_DealDamageModule.RemoveDamageTarget();
        }

        private void AttackSingleTargetSelectorModuleOnTargetSelected(AbstractEntity abstractEntity)
        {
            m_DealDamageModule.SetDamageTarget(abstractEntity, m_AttackReloadModule.Value);
        }
    }
}