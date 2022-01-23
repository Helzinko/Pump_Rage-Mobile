using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;
    
    private Vector3 _target_Offset;
    
    [SerializeField] private float _followSpeed = 50f;
    private void Start()
    {
        _target_Offset = transform.position - _target.position;
    }
    void Update()
    {
        if (_target)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position+_target_Offset, 50f * Time.deltaTime);
        }
    }
}
