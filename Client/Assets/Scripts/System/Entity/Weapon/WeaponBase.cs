using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public abstract class WeaponBase : EntityBase
    {
        protected override EntityType _EntityType => SystemEnum.EntityType.Weapon;
        [SerializeField]
        private WeaponName WeaponName;

        private WeaponData _weaponData = null;

        public WeaponBase ()
        {
            _weaponData = DataManager.Instance.GetData<WeaponData>((int)WeaponName);
        }

        public abstract void Shot();
        public abstract float SetDamage(float Damage);

        public virtual WeaponData GetWeaponData()
        {
            return _weaponData;
        }


    }
}