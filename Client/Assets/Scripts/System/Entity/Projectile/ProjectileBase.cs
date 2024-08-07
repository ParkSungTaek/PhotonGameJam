using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public abstract class ProjectileBase : EntityBase
    {
        protected override SystemEnum.EntityType _EntityType => SystemEnum.EntityType.Projectile;

        public virtual SystemEnum.ProjectileName Projectile => SystemEnum.ProjectileName.Bullet;

        protected ProjectileData _projectileData = null;

        protected Rigidbody _rigidbody = null;

        protected virtual bool IsDestroyWhenHit => true;

        public float Damage { get; set; } = 0;

        private void Awake()
        {
            _projectileData = DataManager.Instance.GetData<ProjectileData>((int)Projectile);
            _rigidbody = GetComponent<Rigidbody>();
        }
        public virtual void Shot(Vector3 direction)
        {
            Vector3 v3 = direction * (_projectileData._projectileSpd / (SystemConst.Per));
            _rigidbody.AddForce(direction * (_projectileData._projectileSpd / SystemConst.Per), ForceMode.Impulse);
        }

        public virtual void SetDamage(float damage)
        {
            Damage = damage;
        }

        protected virtual void HitPlayer(Player player)
        {
            player.OnDamage(Damage);
        }
        protected virtual void HitFloor() { }
        protected virtual void HitDefault() { }
        protected virtual void HitProjectile() { }


        protected virtual void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.layer)
            {
                case SystemConst.DefaultLayer:
                    {
                        HitDefault();
                        break;
                    }
                case SystemConst.FloorLayer:
                    {
                        HitFloor();
                        break;
                    }

                case SystemConst.PlayerLayer:
                    {
                        Player player = collision.gameObject.GetComponent<Player>();
                        if (player == null)
                        {
                            Debug.LogError($"{collision.gameObject.name} 이 Layer Player 이나 Player 타입이 없음 확인바람 (ProjectileBase)");
                        }
                        else
                        {
                            HitPlayer(player);
                        }
                        break;
                    }
                case SystemConst.ProjectileLayer:
                    {
                        HitProjectile();
                        break;
                    }
            }

            if (IsDestroyWhenHit)
                Runner.Despawn(Object);
        }
        protected float LifeTime()
        {
            return _projectileData._lifeTime / SystemConst.Per;
        }
    }
}