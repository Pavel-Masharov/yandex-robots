using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Weapons
{
    public class LauncherBallWeapon : Weapon
    {
        [SerializeField] private Transform _startPositionBullet;

        [SerializeField] private Sprite _spriteWeapon;
        public override Sprite SpriteWeapon => _spriteWeapon;
        public override float speedFire => 1;
        public override TypeWeapons TypeWeapon => TypeWeapons.LauncherBall;

        public override float Damage => 0;

        public override void Shot(GameObject target, Vector3 targetPos = default)
        {
            Debug.Log("LauncherBallWeapon - SHot");
            var bullet = PoolBullets.instance.GetBullet(TypeWeapon);
           
            bullet.gameObject.SetActive(true);
           // bullet.gameObject.transform.SetParent(null);
            bullet.transform.position = _startPositionBullet.position;
            bullet.transform.DOMove(targetPos, 1f);
        }
    }
}
