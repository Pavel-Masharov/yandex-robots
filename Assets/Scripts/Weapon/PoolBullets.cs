using Interfaces;
using Services.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Weapons
{
    public class PoolBullets : MonoBehaviour
    {
        public static PoolBullets instance;

        [SerializeField] private Bullet _bulletPrefabRocket;
        [SerializeField] private Bullet _bulletPrefabBall;
        [SerializeField] private Bullet _bulletPrefabLaser;

        [SerializeField] private EffectHit _affectPrefabRocket;

        private readonly int _amountToPool = 1;
        private Dictionary<TypeWeapons, List<Bullet>> _dicAllBullets = new Dictionary<TypeWeapons, List<Bullet>>();

        private Dictionary<TypeAffectHit, List<EffectHit>> _dicAllAffectHit = new Dictionary<TypeAffectHit, List<EffectHit>>();

        private void Start()
        {
            instance = this;

            CreateAllBullets();
            CreateAllAffects();
        }

        public void CreateAllBullets()
        {
            _dicAllBullets.Add(TypeWeapons.LauncherRocket, GetListBullets(TypeWeapons.LauncherRocket));
            _dicAllBullets.Add(TypeWeapons.LauncherBall, GetListBullets(TypeWeapons.LauncherBall));
            _dicAllBullets.Add(TypeWeapons.LaserGun, GetListBullets(TypeWeapons.LaserGun));
        }

        private List<Bullet> GetListBullets(TypeWeapons typeWeapons)
        {
            List<Bullet> pooledBullets = new List<Bullet>();
            Bullet bullet;
            for (int i = 0; i < _amountToPool; i++)
            {
                bullet = CreatePrefabBullet(typeWeapons);
                bullet.gameObject.SetActive(false);
                pooledBullets.Add(bullet);
            }

            return pooledBullets;
        }

        private Bullet CreatePrefabBullet(TypeWeapons typeWeapons)
        {
            if (typeWeapons == TypeWeapons.LauncherRocket)
                return Instantiate(_bulletPrefabRocket, transform);
            else if (typeWeapons == TypeWeapons.LauncherBall)
                return Instantiate(_bulletPrefabBall, transform);
            else if (typeWeapons == TypeWeapons.LaserGun)
                return Instantiate(_bulletPrefabLaser, transform);
            else
                return null;
        }

        public Bullet GetBullet(TypeWeapons typeWeapons)
        {
            bool hasKey = _dicAllBullets.TryGetValue(typeWeapons, out List<Bullet> pooledBullets);          
            if(hasKey)
            {
                for (int i = 0; i < pooledBullets.Count; i++)                
                    if (!pooledBullets[i].gameObject.activeSelf)
                        return pooledBullets[i];                  
                
                Bullet bullet = CreatePrefabBullet(typeWeapons);
                bullet.gameObject.SetActive(false);
                pooledBullets.Add(bullet);
                return bullet;
            }  
            return null;
        }

        
        //Affects
        public void CreateAllAffects()
        {
            _dicAllAffectHit.Add(TypeAffectHit.EffectHitRocket, GetListAffects(TypeAffectHit.EffectHitRocket));          
        }
        private List<EffectHit> GetListAffects(TypeAffectHit typeAffect)
        {
            List<EffectHit> pooledAffects = new List<EffectHit>();
            EffectHit affect;
            for (int i = 0; i < _amountToPool; i++)
            {
                affect = CreatePrefabAffect(typeAffect);
                affect.gameObject.SetActive(false);
                pooledAffects.Add(affect);
            }

            return pooledAffects;
        }

        private EffectHit CreatePrefabAffect(TypeAffectHit typeAffect)
        {
            if (typeAffect == TypeAffectHit.EffectHitRocket)
                return Instantiate(_affectPrefabRocket, transform);          
            else
                return null;
        }

        public EffectHit GetAffect(TypeAffectHit typeAffect)
        {
            bool hasKey = _dicAllAffectHit.TryGetValue(typeAffect, out List<EffectHit> pooledAffects);
            if (hasKey)
            {
                for (int i = 0; i < pooledAffects.Count; i++)
                    if (!pooledAffects[i].gameObject.activeSelf)
                        return pooledAffects[i];

                EffectHit affect = CreatePrefabAffect(typeAffect);
                affect.gameObject.SetActive(false);
                pooledAffects.Add(affect);
                return affect;
            }
            return null;
        }
    }
}
