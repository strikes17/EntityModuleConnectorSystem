using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class AnimatorIKBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private Transform m_SpineBoneTransform;
        [SerializeField] private Vector3 m_SpineBoneFixedRotation;

        public Transform RotateTarget;
        
        private void LookAtTargetTransform(Transform bone, Vector3 position, Vector3 fixedRotation)
        {
            Vector3 direction = position - bone.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

            Quaternion targetRotation = lookRotation * Quaternion.Euler(fixedRotation);
            
            Vector3 eulerAngles = targetRotation.eulerAngles;
            // eulerAngles.y = bone.eulerAngles.y;
            // eulerAngles.y = Mathf.Clamp(eulerAngles.y, m_MainBodyTransform.eulerAngles.y - 45f, m_MainBodyTransform.eulerAngles.y + 45f);

            Quaternion limitedRotation = Quaternion.Euler(eulerAngles);
            
            
            bone.rotation = limitedRotation;
            // bone.rotation = Quaternion.RotateTowards(bone.rotation, lookRotation, m_RotateSpeed * Time.deltaTime);
        }
    }
}