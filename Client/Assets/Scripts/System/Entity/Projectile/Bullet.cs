using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{

    public class Bullet : ProjectileBase
    {
        public override SystemEnum.ProjectileName Projectile => SystemEnum.ProjectileName.Bullet;

        [Networked] private TickTimer life { get; set; }

        public override void Shot(Vector3 direction)
        {
            life = TickTimer.CreateFromSeconds(Runner, 5.0f);

            Vector3 v3 = direction * (_projectileData._projectileSpd / (SystemConst.Per));
            GetComponent<Rigidbody>().AddForce(direction * (_projectileData._projectileSpd / SystemConst.Per), ForceMode.Impulse);

            //Vector3 v3 = direction * (_projectileData._projectileSpd / (SystemConst.Per));
            //_rigidbody.AddForce(direction * (_projectileData._projectileSpd / SystemConst.Per), ForceMode.Impulse);
        }

        public override void FixedUpdateNetwork()
        {
            if (life.Expired(Runner))
                Runner.Despawn(Object);
        }

        protected override void HitPlayer(Player player)
        {
            throw new System.NotImplementedException();
        }

    }
}