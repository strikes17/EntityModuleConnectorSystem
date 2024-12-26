using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiSetDestinationOfflineTask : AiOfflineTask
    {
        public override event Action<AiTask> TaskCompleted;

        public float SimulatedProgress => m_SimulatedProgress;

        private WaitForSeconds m_WaitForSeconds;

        private Moroutine m_ProgressMoroutine;

        private float m_SimulatedProgress;

        public override void StartResolve()
        {
            if (m_ProgressMoroutine != null)
            {
                m_ProgressMoroutine.Stop();
            }

            m_ProgressMoroutine = Moroutine.Run(Simulate());
        }

        public override void StopResolve()
        {
            if (m_ProgressMoroutine != null)
            {
                m_ProgressMoroutine.Stop();
            }
        }

        private IEnumerator Simulate()
        {
            float interval = 1f;
            m_WaitForSeconds = new WaitForSeconds(interval);
            AiSetDestinationOnlineTask destinationOnlineTask = m_OnlineTask as AiSetDestinationOnlineTask;
            float totalTime = destinationOnlineTask.TotalTimeToDestination;
            m_SimulatedProgress = destinationOnlineTask.MoveToDestinationProgress;
            while (true)
            {
                m_SimulatedProgress += interval / totalTime;
                Debug.Log($"Simulated offline progress of moving: {m_SimulatedProgress}");
                yield return m_WaitForSeconds;
            }
        }
    }
}