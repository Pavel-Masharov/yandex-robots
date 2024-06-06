using UnityEngine;

namespace Services.Weapons
{
    public class LauncherRocketWeapon : Weapon
    {
        [SerializeField] private Transform _startPositionBullet;

        [SerializeField] private Sprite _spriteWeapon;
        [SerializeField] private AudioClip _clipShot;
        public override Sprite SpriteWeapon => _spriteWeapon;
        public override TypeWeapons TypeWeapon => TypeWeapons.LauncherRocket;
        public override float speedFire => 1;

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
