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
    public float _lifeSpan = 2f;

    public void Reset()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = -transform.right * _bulletSpeed;

        StartCoroutine(DisableBullet());
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.ApplyDamage(_damage);
        }
        
        gameObject.SetActive(false);
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(_lifeSpan);
        gameObject.SetActive(false);
    }
}
