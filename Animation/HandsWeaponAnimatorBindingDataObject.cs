using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "Data/New Hands Weapon Animator Binding", fileName = "NewHandsWeaponAnimatorBinding")]
    public class HandsWeaponAnimatorBindingDataObject : ScriptableObject
    {
        [SerializeField] private HandsWeaponAnimatorBindingDataDictionary m_WeaponAnimatorBindingData;

        public HandsWeaponAnimatorBindingData GetAnimatorForWeapon(WeaponDataObject weaponDataObject)
        {
            m_WeaponAnimatorBindingData.TryGetValue(weaponDataObject,
                out HandsWeaponAnimatorBindingData weaponAnimatorBindingData);
            return weaponAnimatorBindingData;
        }
    }
}