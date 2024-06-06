using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Weapons
{
    public class PistolWeapon : Weapon
    {
        public override TypeWeapons TypeWeapon => TypeWeapons.Pistol;

        [SerializeField] private Sprite _spriteWeapon;
        public override Sprite SpriteWeapon => _spriteWeapon;
        public override float speedFire => 1;

        public override float Damage => 5;

        public override void Shot(GameObject target, Vector3 targetPos = default)
        {

            if (target != null)
            {
                ITakeDamage takeDamageObject = target.GetComponent<MonoBehaviour>() as ITakeDamage;

                Debug.Log(takeDamageObject + " " + "PistolWeapon");
                if (takeDamageObject != null)
                {
                    takeDamageObject.TakeDamage(Damage);
                }

            }
            else
            {
                Debug.Log("NOT TARGET");
            }
        }
    }
}
