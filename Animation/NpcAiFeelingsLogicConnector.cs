using System;
using _Project.Scripts.Camera;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponFireModule : AbstractBehaviourModule
    {
        public void Fire()
        {

        }
    }
    [Serializable]
    public class NpcAiFeelingsLogicConnector : BehaviourModuleConnector
    {
        [SelfInject] private NpcAiVisionModule m_VisionModule;
        [SelfInject] private NpcAiLogicModule m_LogicModule;
        [SelfInject] private AiTaskResolverModule m_TaskResolverModule;
        [SelfInject] private AiNavMeshModule m_AiNavMeshModule;
        [SelfInject] private NpcALifeModule m_NpcALifeModule;
        [SelfInject] private NpcFractionModule m_FractionModule;

        protected override void Initialize()
        {
            m_VisionModule.NoticedEntity += VisionModuleOnNoticedEntity;
            m_VisionModule.EntityLostFromSight += VisionModuleOnEntityLostFromSight;
        }

        private void VisionModuleOnEntityLostFromSight(AbstractEntity obj)
        {
        }

        private void VisionModuleOnNoticedEntity(AbstractEntity entity)
        {
            var pointOfInterest = entity.GetBehaviorModuleByType<NpcPointOfInterestModule>();
            if (pointOfInterest != null)
            {
                int rnd = Random.Range(0 + 5 * (int)pointOfInterest.PointOfInterestValue, 100 );
                if (rnd >= 50)
                {
                    if (pointOfInterest.PointOfInterestValue >= m_LogicModule.MinimumPointOfInterestValue)
                    {
                        SetDestinationTask(entity, AiTaskPriority.Important);
                    }
                }
            }
            else
            {
                if (entity.GetType() == typeof(NpcEntity))
                {
                    var npcFractionModule = entity.GetBehaviorModuleByType<NpcFractionModule>();
                    if (m_FractionModule.IsEnemyFraction(npcFractionModule.NpcFraction))
                    {
                        SetDestinationTask(entity, AiTaskPriority.MostImportant);
                    }
                }
            }
            // m_LogicModule.CheckEntity(entity);
        }

        private void SetDestinationTask(AbstractEntity entity, AiTaskPriority aiTaskPriority)
        {
            AiSetDestinationOnlineTask checkEntityOnlineTask =
                new AiSetDestinationOnlineTask(m_AiNavMeshModule, entity.transform.position);
            AiSetDestinationOfflineTask checkEntityOfflineTask =
                new AiSetDestinationOfflineTask(m_AiNavMeshModule, entity.transform.position);
            AiTaskResolver aiTaskResolver =
                new AiTaskResolver(checkEntityOfflineTask, checkEntityOnlineTask, aiTaskPriority);
            m_TaskResolverModule.AddTask(aiTaskResolver, m_NpcALifeModule.NpcALifeState);
        }
    }
}