using Services.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    //FOR CHARACTER
    interface IMove
    {
        void Move();
    }

    interface IRotate
    {
        void Rotate();
    }

    interface IJump
    {
        void Jump();
    }

    interface IAttackFire
    {
        void AttackFire();
    }

    interface ITakeDamage
    {
       // float Health { get; }
        float MaxHealth { get; }
        void TakeDamage(float damage);
    }

    interface IArsenal
    {
        int IdWeapon { get; }
        IWeapon UsedWeapon { get; }
        List<IWeapon> Weapons { get; }
        void ReplaceWeapon();
    }
    //FOR WEAPONS
    public interface IWeapon
    {
        TypeWeapons TypeWeapon { get; }
        Sprite SpriteWeapon { get; }
        float Damage { get; }
        void Shot(GameObject target = null, Vector3 targetPos = default);
        void ShowWeapon();
        void HideWeapon();

        
    }

    public interface IAffectHit
    {
        TypeAffectHit TypeAffect { get; }
        
    }

    interface IGiveDamage
    { 
    
    }

    public interface IBullet
    {
        TypeAffectHit TypeAffect { get; }
        float Damage { get; }
        void Move();
        void EffectHit(Vector3 posContact);
    }

    public interface IGameMode
    {
        TypeGameModes TypeGameModes { get; }
        int AmmountEnemy { get; set; }
        int AmmountScore { get; set; }
        void StartGame();
        void Initialize(int ammountEnemy);
        bool CheckVictory();
        void UpdateValues(int value = 0);

        (string, string) GetTextVictory(int level = 0, int ammountEnemy = 0, int quantityKill = 0);
        (string, string) GetTextDefeat(int level = 0, int ammountEnemy = 0, int quantityKill = 0);
        (string, string) GetTextPause(int level = 0, int ammountEnemy = 0, int quantityKill = 0);
        string GetTextUI(int level = 0, int ammountEnemy = 0, int quantityKill = 0);
    }
}

