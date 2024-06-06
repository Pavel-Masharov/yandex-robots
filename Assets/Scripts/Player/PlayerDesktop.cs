using Services.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Services.Player
{
    public class PlayerDesktop : Player
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
                curWeapon.transform.localPosition = Vector3.zero;//_positionWeapon.position;
                curWeapon.gameObject.SetActive(false);
                _weapons.Add(curWeapon);
            }
            _weapon = _weapons[_idWeapon];
            _weapon.ShowWeapon();
            _playerUI.SetSpriteWreapon(_weapon.SpriteWeapon);
        }



        [SerializeField] private Transform _positionWeapon; //possition Weapon
        private PlayerUI _playerUI;
        private UnityAction _actionDefeat;
        private UnityAction _actionPause;
        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
            _playerUI = GetComponent<PlayerUI>();

            CreateWeapons();

        }
        void Update()
        {
            if (SettingsGame.isGamePause)
                return;


            OnPause();
            Move();
            Rotate();
            Jump();
            AttackFire();
            ReplaceWeapon();
        }

        public override void InitializePlayer(UnityAction actionDefeat, UnityAction actionOnPause)
        {
            _actionDefeat = actionDefeat;
            _actionPause = actionOnPause;
        }

        public override void Move()
        {
            Vector3 dir = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            dir = Vector3.ClampMagnitude(dir, speed);
            transform.Translate(Time.deltaTime * dir, Space.Self);

            if (dir != Vector3.zero)
            {
                _animator.SetBool(_runStateHash, true);
            }
            else
            {
               // _animator.SetBool(_runStateHash, true);
                _animator.SetBool(_runStateHash, false);
            }
        }

        public override void Rotate()
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensVert;
            _rotationX = Mathf.Clamp(_rotationX, _minVert, _maxVert);
            float delta = Input.GetAxis("Mouse X") * _sensHor;
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

        public override void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canJump)
            {
                //_animator.SetTrigger(_jumpStateHash);
                _rigidbody.AddForce(Vector3.up * _forseJump, ForceMode.Impulse);
                RollbackJump();
            }
        }

        public override void AttackFire()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 posTarget = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
                Ray ray = _camera.ScreenPointToRay(posTarget);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                  //  Debug.Log(hit.point);
                    _playerUI.AimAnimationShot();
                    _weapon.Shot(hit.transform.gameObject, hit.point);
                }
            }
        }

        public override void ReplaceWeapon()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _weapon.HideWeapon();

                if (_idWeapon < _weapons.Count - 1)
                    _idWeapon++;
                else
                    _idWeapon = 0;

                _weapon = _weapons[_idWeapon];
                _weapon.ShowWeapon();
                _playerUI.SetSpriteWreapon(_weapon.SpriteWeapon);
                GameAudioController.instance.PlayOneShotSound(_clipReplaceWeapon);
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
            _playerUI.OutputInfo(textInfo);
        }


        public override void OnPause()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _actionPause?.Invoke();
                //открыть попап
            }
        }

        public override void RestoreHealth(float health)
        {
            canTakeDamage = false;
            _health = health;
        }
    }
}
