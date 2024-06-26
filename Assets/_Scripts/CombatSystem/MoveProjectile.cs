using System;
using StarterAssets;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private Rigidbody _rigidbody;
    private ThirdPersonController _tpc;
    private Vector3 _aimDirection;
    private float _heigthOffset = 1.8f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();

        this.transform.rotation = _tpc.transform.rotation;

        if (_tpc.enemyTarget != null)
        {
            Vector3 enemyWithOffset = new Vector3(_tpc.enemyTarget.position.x, _tpc.enemyTarget.position.y + _heigthOffset, _tpc.enemyTarget.position.z);
            _aimDirection = (_tpc.enemyTarget.position - transform.position).normalized;
        }
        else _aimDirection = _tpc.transform.forward;

    }

    private void FixedUpdate()
    {
        _rigidbody.position += _aimDirection * (speed * Time.deltaTime);
    }
    
}
