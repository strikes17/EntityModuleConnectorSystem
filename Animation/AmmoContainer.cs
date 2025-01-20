using System;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class AmmoContainer : EntityContainerModule
    {
        public AmmoEntity SpawnAmmo(AmmoDataObject ammoDataObject)
        {
            var ammoEntity = Object.Instantiate(ammoDataObject.AmmoEntity);
            AddElement(ammoEntity);
            return ammoEntity;
        }
    }
}