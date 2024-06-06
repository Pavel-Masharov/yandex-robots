using Cysharp.Threading.Tasks;
using Interfaces;
using Services.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Services.Player
{
    public abstract class Player : MonoBehaviour, IMove, IRotate, IJump, IAttackFire, IArsenal, ITakeDamage
    {
        protected Camera _camera;

        //for move
        protected float speed = 10f;

        //for rotate //*
        [SerializeField] protected GameObject _objectRotate;
        protected float _rotationX = 0f;
        protected float _sensHor = 9f;
        protected float _sensVert = 9f;
        protected float _minVert = -35;
        protected float _maxVert = 35;

        //for jump
        protected Rigidbody _rigidbody;
        protected float _forseJump = 10;
        protected bool _canJump = true;

        //Animation
        [SerializeField] protected Animator _animator;
        protected int _runStateHash = Animator.StringToHash("isRun");
        protected int _throwStateHash = Animator.StringToHash("isThrow");
        protected int _jumpStateHash = Animator.StringToHash("jump");

        //Arsenal
        public List<IWeapon> Weapons => _weapons;
        public int IdWeapon => _idWeapon;
        public IWeapon UsedWeapon => _weapon;

        protected List<IWeapon> _weapons = new List<IWeapon>();
        protected IWeapon _weapon;
        protected int _idWeapon = 0;


        //audio
        [SerializeField] protected AudioClip _clipReplaceWeapon;
        [SerializeField] protected AudioClip _clipTakeDamage;

        //ui
        public float MaxHealth => 1000;
        protected float _health = 1000;
        public bool canTakeDamage = true;
        //

        public abstract void InitializePlayer(UnityAction actionDefeat, UnityAction actionOnPause);
        public abstract void AttackFire();
        public abstract void ReplaceWeapon();
        public abstract void Jump();
        public abstract void Move();
        public abstract void Rotate();
        public abstract void TakeDamage(float damage);
        public abstract void UpdateInfoUI(string textInfo);
        public abstract void OnPause();
        public abstract void RestoreHealth(float health);

        protected async UniTask RollbackJump()
        {
            _canJump = false;
            await UniTask.Delay(2000);
            _canJump = true;
        }

    }
}
