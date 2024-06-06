using Interfaces;
using Services.Player;
using Services.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyCharacter : MonoBehaviour, ITakeDamage, IAttackFire
{
    [SerializeField] private Image _imageHealthBar;
    public float MaxHealth => 100;

    private float _health = 100;

    //Animation
    [SerializeField] protected Animator _animator;
    protected int _runStateHash = Animator.StringToHash("isRun");
    protected int _throwStateHash = Animator.StringToHash("isThrow");
    protected int _jumpStateHash = Animator.StringToHash("jump");

    private Player _player;

    private Coroutine _coroutineControlMove;
    private Coroutine _coroutineMove;
    private float _speedMove = 10; 
    private float _valueBorder = 10; //Значение для определения границ


    [SerializeField] private Transform _positionWeapon;
    [SerializeField] private Weapon _weaponPrefab;
    private IWeapon _weapon;
    private float _distanceAttack = 70;

    private UnityAction _actionDeath;


    private NavMeshAgent _agent;
    private void Start()
    {
        var curWeapon = Instantiate(_weaponPrefab, _positionWeapon);
        curWeapon.transform.localPosition = Vector3.zero;
        _weapon = curWeapon;

        _agent = GetComponent<NavMeshAgent>();

        _coroutineControlMove = StartCoroutine(CoroutineControlMove());
        _animator.SetBool(_runStateHash, true);


    }

    private void Update()
    {
        if (SettingsGame.isGamePause)
            return;

        transform.LookAt(_player.transform);

        AttackFire();
    }

    public void InitializeEnemy(Player player, UnityAction actionDeath, int id = 0)
    {
        _player = player;
        _actionDeath = actionDeath;

        transform.name = id.ToString();
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OutputHealth();

        if (_health <= 0)       
            Death();            
    }

    public void OutputHealth()
    {
        float percentHealth = _health / MaxHealth;
        _imageHealthBar.fillAmount = percentHealth;
    }

    private void Death()
    {
        _actionDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void AttackFire()
    {
        if (CheckDistanceToPlayer())
            _weapon.Shot(_player.transform.gameObject, _player.transform.position);    
    }

    private bool CheckDistanceToPlayer()
    {      
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= _distanceAttack)
            return true;

        return false;
    }

    //Движение
    private IEnumerator CoroutineControlMove()
    {
        while (true)
        {
            float timeMove = GenerateRandomFloat(0, 7);
            yield return new WaitForSeconds(timeMove);

           // Debug.Log(transform.name);
            _agent.SetDestination(_player.transform.position);
        }


        //while (true)
        //{
        //    float timeMove = GenerateRandomFloat(3, 10);
        //    yield return new WaitForSeconds(timeMove);

        //    if (_coroutineMove != null)
        //        StopCoroutine(_coroutineMove);

        //    _coroutineMove = StartCoroutine(MoveEnemy(GenerateRandomPoint(_valueBorder)));
        //}
    }

    //Корутина движения
    private IEnumerator MoveEnemy(Vector3 point)
    {
        while(true)
        {
            if (SettingsGame.isGamePause)
                break;

            var step = _speedMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, point, step);
            yield return null;
        }
        
    }

    private float GenerateRandomFloat(float min, float max)
    {
        float time = Random.Range(min, max);
        return time;
    }
    private Vector3 GenerateRandomPoint(float border)
    {
        float PosX = Random.Range(-border, border);
        float PosZ = Random.Range(-border, border);
        return new Vector3(PosX, transform.position.y, PosZ);
    }
}
