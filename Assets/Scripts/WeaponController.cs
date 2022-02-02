using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float _timeBetweenShots = 0.5f;
    public int _bulletCount = 7;
    private int _currentBullets = 0;
    private float _reloadingTime = 0.5f;

    private float _timeSinceLastShot = 0f;

    [SerializeField] private Transform _muzzlePlace;
    [SerializeField] private GameObject _bulletPrefab;

    private bool isReloading = false;
    private void Start()
    {
        _currentBullets = _bulletCount;
        HUD._instance.SetBullets(_currentBullets);
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
                if (!isReloading)
                {
                    StartCoroutine(Reloading());
                    isReloading = true;
                }
            }
            else
            {
                SoundManager.instance.PlayEffect(SoundTypes.shoot);
                var bullet = PoolManager.instance.SpawnBullet(GameTypes.Bullets.normal, _muzzlePlace.position, _muzzlePlace.rotation);
                bullet.GetComponent<Bullet>().Reset();
                _currentBullets--;
                _timeSinceLastShot = 0;
                HUD._instance.SetBullets(_currentBullets);
            }
        }
    }

    public void Reset()
    {
        _currentBullets = _bulletCount;
        _timeSinceLastShot = 0f;
        HUD._instance.SetBullets(_currentBullets);
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(_reloadingTime);
        
        _currentBullets = _bulletCount;
        HUD._instance.SetBullets(_currentBullets);
        isReloading = false;
    }
}
