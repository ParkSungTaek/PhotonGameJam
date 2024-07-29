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

        protected Rigidbody2D _rigidbody2D = null;

        protected virtual bool IsDestroyWhenHit => true;

        public float Damage { get; set; } = 0;

        private void Awake()
        {
            _projectileData = DataManager.Instance.GetData<ProjectileData>((int)Projectile);
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public virtual void Shot(Vector3 direction)
        {
            Vector3 v3 = direction * (_projectileData._projectileSpd / (SystemConst.Per));
            _rigidbody2D.AddForce(direction * (_projectileData._projectileSpd / SystemConst.Per), ForceMode2D.Impulse);
        }

        public virtual void SetDamage(float damage)
        {
            Damage = damage;
        }

        protected abstract void HitPlayer(CharPlayer player);
        protected virtual void HitFloor() { }
        protected virtual void HitDefault() { }
        protected virtual void HitProjectile() { }


        private void OnTriggerEnter2D(Collider2D collision)
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
                        CharPlayer player = collision.gameObject.GetComponent<CharPlayer>();
                        if(player == null)
                        {
                            Debug.LogError($"{collision.gameObject.name} 이 Layer Player 이나 CharPlayer 타입이 없음 확인바람 (ProjectileBase)");
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
                Destroy(this.gameObject);
        }

    }
}