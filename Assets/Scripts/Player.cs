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
    void Start()
    {
        inputVector = Vector2.zero;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        _controller.SimpleMove(inputVector * _playerMovementSpeed);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        var inputValue = value.ReadValue<Vector2>();
        inputVector = new Vector3(inputValue.x, 0, inputValue.y);
    }

    public void OnAim(InputAction.CallbackContext value)
    {
        var inputValue = value.ReadValue<Vector2>();

        if (inputValue.x != 0.0f || inputValue.y != 0.0f) {
            var angle = Mathf.Atan2(inputValue.y, -inputValue.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }
}
