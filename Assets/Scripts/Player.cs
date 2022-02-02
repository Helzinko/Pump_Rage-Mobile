using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    public float _playerMovementSpeed = 10.0f;

    private Vector3 inputVector;
    private Vector2 _rotationValue;

    [SerializeField] private int _health = 10;

    private int _currentHealth;

    private WeaponController _weaponController;

    private Vector3 _initialPos;
    private Quaternion _initialRotation;
    
    void Start()
    {
        GameManager._instance.GameplayReset.AddListener(Reset);
        
        inputVector = Vector2.zero;
        _controller = GetComponent<CharacterController>();
        _weaponController = GetComponentInChildren<WeaponController>();

        _initialPos = transform.position;
        _initialRotation = transform.rotation;
        
        _currentHealth = _health;
        HUD._instance.SetHealth(_currentHealth);
    }

    void Update()
    {
        _controller.SimpleMove(inputVector * _playerMovementSpeed);
        
        if(_rotationValue.magnitude != 0)
            _weaponController.Shoot();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        var inputValue = value.ReadValue<Vector2>();
        inputVector = new Vector3(inputValue.x, 0, inputValue.y);
    }

    public void OnAim(InputAction.CallbackContext value)
    {
        _rotationValue = value.ReadValue<Vector2>();

        if (_rotationValue.x != 0.0f || _rotationValue.y != 0.0f) {
            var angle = Mathf.Atan2(_rotationValue.y, -_rotationValue.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HUD._instance.SetHealth(_currentHealth);

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        // disable so can move player
        _controller.enabled = false;
        
        _currentHealth = _health;
        HUD._instance.SetHealth(_currentHealth);
        transform.position = _initialPos;
        transform.rotation = _initialRotation;
        _weaponController.Reset();
        
        // enable
        _controller.enabled = true;
        
        gameObject.SetActive(true);
    }
}
