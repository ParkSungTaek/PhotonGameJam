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
                Debug.Log($"{ProjectilePath} ���� Projectile�� ã������ (WeaponBase : {this.name})");
                return null;
            }

            ProjectileBase projectile = Instantiate.GetComponent<ProjectileBase>();
            if (projectile == null)
            {

                Debug.Log($"{ProjectileName} ���� ProjectileBase �� Get �������� ");
                return null;
            }
            projectile.SetDamage(_charPlayer.PlayerInfo.GetStat(EntityStat.NAtt));
            Debug.Log($"�ش� ����ü �������� {_charPlayer.PlayerInfo.GetStat(EntityStat.NAtt)}");
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

                // ���콺�� z ��ġ�� �÷��̾� ������Ʈ�� z ��ġ�� ����ϴ�.
                // �̴� ī�޶��� ����Ʈ���� ������ ����(�÷��̾��� ����)�� ����ϵ��� �ϱ� �����Դϴ�.
                mouseScreenPosition.z = Camera.main.WorldToScreenPoint(_charPlayer.transform.position).z;

                // ��ũ�� ��ǥ�踦 ���� ��ǥ��� ��ȯ�մϴ�.
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                // �÷��̾� ������Ʈ�� ���콺 ��ġ ���� ���̸� ����մϴ�.
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