using UnityEngine;

namespace Services.Weapons
{
    public class LauncherRocketBullet : Bullet
    {
        [SerializeField] private EffectHit _effectHit;
        public override TypeAffectHit TypeAffect => TypeAffectHit.EffectHitRocket;
        private float _moveSpeed = 60f;

        public override void EffectHit(Vector3 posContact)
        {
            EffectHit affect = PoolBullets.instance.GetAffect(TypeAffect);
            affect.gameObject.SetActive(true);
            affect.transform.position = posContact;
        }
        public override void Move()
        {        
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);        
        }
    }
}