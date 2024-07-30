using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public abstract class WeaponBase : EntityBase
    {
        [SerializeField]
        private WeaponName WeaponName;
        [SerializeField]
        protected ProjectileName ProjectileName;
        protected override EntityType _EntityType => SystemEnum.EntityType.Weapon;
        protected Player _charPlayer = null;
        private WeaponData _weaponData = null;

        protected string ProjectilePath => $"Projectile/{ProjectileName.ToString()}";
        public Vector3 direction { get; set; } = Vector3.zero;

        ProjectileBase InstantiateProjectile() 
        {
            GameObject Instantiate = ObjectManager.Instance.Instantiate(ProjectilePath, transform);
            if (Instantiate == null)
            {
                Debug.Log($"{ProjectilePath} 에서 Projectile을 찾지못함 (WeaponBase : {this.name})");
                return null;
            }

            ProjectileBase projectile = Instantiate.GetComponent<ProjectileBase>();
            if (projectile == null)
            {

                Debug.Log($"{ProjectileName} 에서 ProjectileBase 를 Get 하지못함 ");
                return null;
            }
            projectile.SetDamage(_charPlayer.PlayerInfo.GetStat(EntityStat.NAtt));
            Debug.Log($"해당 투사체 데미지는 {_charPlayer.PlayerInfo.GetStat(EntityStat.NAtt)}");
            return projectile;
        }

        public WeaponBase ()
        {
            _weaponData = DataManager.Instance.GetData<WeaponData>((int)WeaponName);
        }

        public virtual void Shot()
        {
            InstantiateProjectile().Shot(direction);
        }

        public virtual void SetDirection()
        {
            if (_charPlayer != null && _charPlayer == EntityManager.Instance.MyPlayer)
            {
                GameManager.Instance.AddOnUpdate(PointToMouse);
            }
        }

        void PointToMouse()
        {
            if (_charPlayer != null)
            {

                Vector3 mouseScreenPosition = Input.mousePosition;

                // 마우스의 z 위치를 플레이어 오브젝트의 z 위치와 맞춥니다.
                // 이는 카메라의 뷰포트에서 동일한 깊이(플레이어의 깊이)를 사용하도록 하기 위함입니다.
                mouseScreenPosition.z = Camera.main.WorldToScreenPoint(_charPlayer.transform.position).z;

                // 스크린 좌표계를 월드 좌표계로 변환합니다.
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                // 플레이어 오브젝트와 마우스 위치 간의 차이를 계산합니다.
                Vector3 rawDirection = mouseWorldPosition - _charPlayer.transform.position;

                direction = rawDirection.normalized;
            }
                
        }

        public abstract float SetDamage(float Damage);

        public virtual WeaponData GetWeaponData()
        {
            return _weaponData;
        }

        public void SetCharPlayer(Player charPlayer)
        {
            _charPlayer = charPlayer;
            SetDirection();
        }

    }
}