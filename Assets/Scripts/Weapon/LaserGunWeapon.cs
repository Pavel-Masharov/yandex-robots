using UnityEngine;

namespace Services.Weapons
{
    public class LaserGunWeapon : Weapon
    {

        [SerializeField] private Transform _startPositionBullet;
        [SerializeField] private Sprite _spriteWeapon;
        [SerializeField] private AudioClip _clipShot;
        public override Sprite SpriteWeapon => _spriteWeapon;
        public override TypeWeapons TypeWeapon => TypeWeapons.LaserGun;

        public override float speedFire => 0.1f;

        public override float Damage => 0;

        public override void Shot(GameObject target, Vector3 targetPos = default)
        {
            if (!_canFire)
                return;

            StartCoroutineControlSpeedFire();

            GameAudioController.instance.PlayOneShotSound(_clipShot);

            var bullet = PoolBullets.instance.GetBullet(TypeWeapon);
            bullet.gameObject.SetActive(true);
            bullet.transform.position = _startPositionBullet.position;
            bullet.transform.LookAt(targetPos);
        }
    }
}
