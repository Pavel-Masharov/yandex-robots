using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Services.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public abstract TypeWeapons TypeWeapon { get ; }
        public abstract Sprite SpriteWeapon { get; }

        // protected TypeWeapons typeWeapons;
        public abstract void Shot(GameObject target, Vector3 targetPos = default);


        public abstract float speedFire { get; }

        public abstract float Damage { get; }

        private Coroutine _coroutineControlSpeedFire;
        protected bool _canFire = true;

        public void ShowWeapon()
        {
            gameObject.SetActive(true);
        }
        public void HideWeapon()
        {
            if(_coroutineControlSpeedFire != null)
                StopCoroutine(_coroutineControlSpeedFire);

            gameObject.SetActive(false);
        }

        private IEnumerator ControlSpeedFire()
        {
            _canFire = false;
            yield return new WaitForSeconds(speedFire);
            _canFire = true;
        }

        protected void StartCoroutineControlSpeedFire()
        {
            _coroutineControlSpeedFire = StartCoroutine(ControlSpeedFire());
        }
    }
}
