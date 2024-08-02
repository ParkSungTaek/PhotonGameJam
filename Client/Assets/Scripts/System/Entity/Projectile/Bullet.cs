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
            float time = LifeTime();
            life = TickTimer.CreateFromSeconds(Runner, time);
            base.Shot(direction);
        }

        public override void FixedUpdateNetwork()
        {
            if (life.Expired(Runner))
                Runner.Despawn(Object);
        }
    }
}