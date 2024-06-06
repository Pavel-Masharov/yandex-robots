using Interfaces;
using UnityEngine;


namespace Services.Weapons
{
    public class AssaultRifleWeapon : Weapon
    {
        [SerializeField] private Sprite _spriteWeapon;
        [SerializeField] private ParticleSystem _particleShot;
        [SerializeField] private AudioClip _clipShot;
        public override Sprite SpriteWeapon => _spriteWeapon;
        public override TypeWeapons TypeWeapon => TypeWeapons.AssaultRifle;

        public override float speedFire => 0.1f;

        public override float Damage => 5;

        public override void Shot(GameObject target, Vector3 targetPos = default)
        {
            if (!_canFire)
                return;

            StartCoroutineControlSpeedFire();

            GameAudioController.instance.PlayOneShotSound(_clipShot);

            _particleShot.Stop();
            _particleShot.Play();

            if (target != null)
            {
                ITakeDamage takeDamageObject = target.GetComponent<MonoBehaviour>() as ITakeDamage;
                if(takeDamageObject != null)
                {
                    takeDamageObject.TakeDamage(Damage);
                }
                
            }
            else
            {

            }
           
        }
    }
}
