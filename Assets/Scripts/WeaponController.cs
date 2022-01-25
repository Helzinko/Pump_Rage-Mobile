using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float _timeBetweenShots = 0.5f;
    public int _bulletCount = 7;
    private int _currentBullets = 0;

    private float _timeSinceLastShot = 0f;

    [SerializeField] private Transform _muzzlePlace;
    [SerializeField] private GameObject _bulletPrefab;
    private void Start()
    {
        _currentBullets = _bulletCount;
    }

    private void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
    }

    public void Shoot()
    {
        if (_timeSinceLastShot >= _timeBetweenShots)
        {
            if (_currentBullets <= 0)
            {
                print("I am reloading...");
                _currentBullets = _bulletCount;
            }
            else
            {
                var bullet = Instantiate(_bulletPrefab, _muzzlePlace.position, _muzzlePlace.rotation);
                bullet.transform.SetParent(null);
                Destroy(bullet, 2f);
                
                _currentBullets--;
                _timeSinceLastShot = 0;
            }
        }
    }
}
