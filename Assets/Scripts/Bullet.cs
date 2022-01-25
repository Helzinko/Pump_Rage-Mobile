using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    public float _bulletSpeed = 100f;
    public int _damage = 1;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = -transform.right * _bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.ApplyDamage(_damage);
        }
        
        Destroy(gameObject);
    }
}
