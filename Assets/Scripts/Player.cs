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

    private float _currentHealth = 10;

    private WeaponController _weaponController;
    
    void Start()
    {
        inputVector = Vector2.zero;
        _controller = GetComponent<CharacterController>();
        _weaponController = GetComponentInChildren<WeaponController>();
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

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
