using Services.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Services.Player
{
    public class PlayerPhone : Player
    {
        /// <summary>
        /// //TEMP
        /// </summary>
        [SerializeField] private List<Weapon> _listWeapons;
        private void CreateWeapons()
        {
            foreach (var item in _listWeapons)
            {
                var curWeapon = Instantiate(item, _positionWeapon);             
                curWeapon.transform.localPosition = Vector3.zero;
                curWeapon.gameObject.SetActive(false);
                _weapons.Add(curWeapon);
            }
            _weapon = _weapons[_idWeapon];
            _weapon.ShowWeapon();
        }



        [SerializeField] private Transform _positionWeapon; //possition Weapon
        private PlayerUI _playerUI;
        private UnityAction _actionDefeat;
        private UnityAction _actionPause;

        //только для телефонов
        [SerializeField] private Joystick _joystickMove;
        [SerializeField] private Joystick _joystickRotate;
        [SerializeField] private Button _buttonJump;
        [SerializeField] private Button _buttonAttack;
        [SerializeField] private Button _buttonChangeWeapon;
        [SerializeField] private Button _buttonPause;

        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
            _playerUI = GetComponent<PlayerUI>();

            CreateWeapons();

            _buttonJump.onClick.AddListener(() => Jump());
            _buttonAttack.onClick.AddListener(() => AttackFire());
            _buttonChangeWeapon.onClick.AddListener(() => ReplaceWeapon());
            _buttonPause.onClick.AddListener(() => OnPause());
        }

        void Update()
        {
            if (SettingsGame.isGamePause)
                return;

            Move();
            Rotate();
        }

        public override void InitializePlayer(UnityAction actionDefeat, UnityAction actionOnPause)
        {
            _actionDefeat = actionDefeat;
            _actionPause = actionOnPause;
        }
        public override void AttackFire()
        {
            if (SettingsGame.isGamePause)
                return;

            Vector3 posTarget = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(posTarget);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //  Debug.Log(hit.point);
                _playerUI.AimAnimationShot();
                _weapon.Shot(hit.transform.gameObject, hit.point);
            }
        }

        public override void ReplaceWeapon()
        {
            if (SettingsGame.isGamePause)
                return;

            _weapon.HideWeapon();

            if (_idWeapon < _weapons.Count - 1)
                _idWeapon++;
            else
                _idWeapon = 0;

            _weapon = _weapons[_idWeapon];
            _weapon.ShowWeapon();
        }

        public override void Jump()
        {
            if (SettingsGame.isGamePause)
                return;

            _rigidbody.AddForce(Vector3.up * _forseJump, ForceMode.Impulse);
            RollbackJump();
        }

        public override void Move()
        {
            if (SettingsGame.isGamePause)
                return;

            Vector3 joysticDirection = _joystickMove.Direction;
            Vector3 dir = new Vector3(joysticDirection.x * speed, 0, joysticDirection.y * speed);
            dir = Vector3.ClampMagnitude(dir, speed);

            transform.Translate(Time.deltaTime * dir, Space.Self);
            if (dir != Vector3.zero)
            {
                _animator.SetBool(_runStateHash, true);
            }
            else
            {
                _animator.SetBool(_runStateHash, false);
            }
        }

        public override void Rotate()
        {
            if (SettingsGame.isGamePause)
                return;

            Vector3 joysticDirection = _joystickRotate.Direction;
            _rotationX -= joysticDirection.y * _sensVert;
            _rotationX = Mathf.Clamp(_rotationX, _minVert, _maxVert);
            float delta = joysticDirection.x * _sensHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, 0);
            _objectRotate.transform.eulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);


            if (_objectRotate.transform.localRotation.x < 0)
            {
                float deltaZoom = _objectRotate.transform.localRotation.x;
                float zoom = deltaZoom * (-10);
                _objectRotate.transform.localPosition = new Vector3(_objectRotate.transform.localPosition.x, zoom, _objectRotate.transform.localPosition.z);
            }
        }

        public override void TakeDamage(float damage)
        {
            if (SettingsGame.isGamePause)
                return;

            if (!canTakeDamage)
                return;

            _health -= damage;
            float percentHealth = _health / MaxHealth;
            _playerUI.OutputHealth(percentHealth);

            GameAudioController.instance.PlayOneShotSound(_clipTakeDamage);

            if (_health <= 0)
                _actionDefeat?.Invoke();
        }

        public override void UpdateInfoUI(string textInfo)
        {
            if (SettingsGame.isGamePause)
                return;

            _playerUI.OutputInfo(textInfo);
        }

        public override void OnPause()
        {
            if (SettingsGame.isGamePause)
                return;

            _actionPause?.Invoke();
            
        }

        public override void RestoreHealth(float health)
        {
            canTakeDamage = false;
            _health = health;
        }
    }
}
