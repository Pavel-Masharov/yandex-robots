using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Interfaces;

namespace Services.Weapons
{
    public class EffectHitRocket : EffectHit
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private Sequence _sequenceAimAnimationExplosion;

        protected override float Damage => 70;
        private bool _isHit = false;

        private List<GameObject> _listDamageObjects = new();

        public override void ActionEffect()
        {
            float scaleExplosion = 20f;
            float timeAnimationExplosion = 0.5f;
            float timeDelay = 1;

            _sequenceAimAnimationExplosion.Kill();
            _sequenceAimAnimationExplosion = DOTween.Sequence();
            _particleSystem.Play();
            _sequenceAimAnimationExplosion.Append(transform.DOScale(scaleExplosion, timeAnimationExplosion));
            _sequenceAimAnimationExplosion.AppendInterval(timeDelay).AppendCallback(() => Deactive());
        }

        private void Deactive()
        {
            _isHit = false;
            transform.localScale = Vector3.one;

            _listDamageObjects.Clear();

            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (!_isHit)
            {
                _isHit = true;
                ActionEffect();
            }


            var takeDamageObject = collision.gameObject.GetComponent<MonoBehaviour>() as ITakeDamage;
            if (takeDamageObject != null)
            {
                if(!_listDamageObjects.Contains(collision.gameObject))
                {
                    _listDamageObjects.Add(collision.gameObject);
                    takeDamageObject.TakeDamage(Damage);
                }             
            }
        }
    }
}
