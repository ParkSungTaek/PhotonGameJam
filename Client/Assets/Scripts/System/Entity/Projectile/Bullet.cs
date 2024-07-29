using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{

    public class Bullet : ProjectileBase
    {
        public override SystemEnum.ProjectileName Projectile => SystemEnum.ProjectileName.Bullet;

        protected override void HitPlayer(CharPlayer player)
        {
            throw new System.NotImplementedException();
        }

    }
}