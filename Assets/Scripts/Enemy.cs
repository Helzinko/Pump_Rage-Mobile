using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private Player _player;
    private Transform _target;

    private NavMeshAgent _agent;

    private void Start()
    {
        _player = GameManager._instance.player;
        _target = _player.gameObject.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_enemyState == EnemyState.DEAD) return;

        if (_enemyState == EnemyState.IDLE)
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
            return;
        }
        
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
                print("I am attacking");
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
}
