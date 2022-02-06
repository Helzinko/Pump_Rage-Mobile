using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum EnemyType
{
    NORMAL = 0,
    SHOOTING = 1,
}

public enum EnemyState
{
    IDLE = 0,
    CHASE = 1,
    ATTACK = 2,
    DEAD = 3,
}

public class Enemy : MonoBehaviour
{
    public EnemyType _enemyType = EnemyType.NORMAL;
    public EnemyState _enemyState = EnemyState.IDLE;
    
    public float _health = 5;
    public float _seeDistance = 5f;
    public float _attackDistance = 2f;
    public float _timeBetweenAttacks = 1f;
    public int _attackDamage = 1;

    public int score = 5;

    private float _currentHealth = 0;

    private Player _player;
    private Transform _target;

    private NavMeshAgent _agent;

    public void Reset()
    {
        _player = GameManager._instance.player;
        _target = _player.gameObject.transform;
        _agent = GetComponent<NavMeshAgent>();
        _currentHealth = _health;
        _enemyState = EnemyState.IDLE;
        
        gameObject.SetActive(true);
    }

    private float _timeSinceLastAttack = 0;
    
    private void Update()
    {
        if (_enemyState == EnemyState.DEAD) return;

        if (_enemyState == EnemyState.IDLE)
        {
            if (_target)
            {
                RaycastHit hit;
                Vector3 direction = _target.position - transform.position;
                if (Physics.Raycast(transform.position, direction, out hit, _seeDistance))
                {
                    if (hit.transform == _target)
                    {
                        _enemyState = EnemyState.CHASE;
                        _agent.SetDestination(_target.position);
                    }
                }
       
            }
            return;
        }

        _timeSinceLastAttack += Time.deltaTime;
        
        if (_enemyState == EnemyState.CHASE)
        {
            if(IsInAttackRange())
            {
                _agent.isStopped = true;
                _enemyState = EnemyState.ATTACK;
            }
            else
            {
                _agent.SetDestination(_target.position);
            }
            return;
        }
        
        if (_enemyState == EnemyState.ATTACK)
        {
            if(IsInAttackRange())
            {
                if (_timeSinceLastAttack >= _timeBetweenAttacks)
                {
                    StartCoroutine(DebugAttack());
                    _timeSinceLastAttack = 0;
                }
            }
            else
            {
                _enemyState = EnemyState.CHASE;
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
            }
        }
    }

    private bool IsInAttackRange()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        
        if (distance <= _attackDistance)
            return true;
        
        return false;
    }

    private float _attackTime = 0.1f;
    IEnumerator DebugAttack()
    {
        transform.DOScale(1.25f, _attackTime);
        _player.ApplyDamage(_attackDamage);
        yield return new WaitForSeconds(_attackTime);
        transform.DOScale(1f, _attackTime);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            Kill(false);
        }
    }

    public void Kill(bool systemKill)
    {
        if (!systemKill)
        {
            StatsManager._instance.AddScore(score);
            EnemysHandler.instance.RandomSpawn();
        }
        
        EnemysHandler.instance.RemoveEnemy(this);
        gameObject.SetActive(false);
    }
}
