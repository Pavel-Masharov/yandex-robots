using Interfaces;
using UnityEngine;

namespace Services.Weapons
{
    public class Bullet : MonoBehaviour, IBullet
    {
        public virtual TypeAffectHit TypeAffect { get; }

        public virtual float Damage => 50;

        private bool _isHit = false;
        public void DeactiveBullet()
        {
            _isHit = false;
            gameObject.SetActive(false);
        }

        public virtual void Move()
        {

        }
       
        public virtual void EffectHit(Vector3 posContact)
        {

        }

        private void FixedUpdate()
        {
            Move();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (_isHit)
                return;

            _isHit = true;

            EffectHit(collision.contacts[0].point);

            var takeDamageObject = collision.gameObject.GetComponent<MonoBehaviour>() as ITakeDamage;
            if (takeDamageObject != null)
            {
                takeDamageObject.TakeDamage(Damage);
            }

            DeactiveBullet();
        }


    }
}