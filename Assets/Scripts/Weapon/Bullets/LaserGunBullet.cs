using UnityEngine;

namespace Services.Weapons
{
    public class LaserGunBullet : Bullet
    {
        private float _moveSpeed = 60f;
        public override float Damage => 10;

        public override void Move()
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }

    }
}