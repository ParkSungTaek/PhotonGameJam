using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class Pistol : WeaponBase
    {
        private float _Damage;

        public override float SetDamage(float Damage)
        {
            _Damage = Damage;
            return _Damage;
        }

        public override void Shot()
        {
            throw new System.NotImplementedException();
        }

    }
}
