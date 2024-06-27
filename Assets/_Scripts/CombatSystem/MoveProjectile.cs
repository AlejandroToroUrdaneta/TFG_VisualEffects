using StarterAssets;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private Rigidbody _rigidbody;
    private ThirdPersonController _tpc;
    private Vector3 _aimDirection;
    private float _heigthOffset = 0.95f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();

        if (_tpc.enemyTarget != null)
        {
            
            Vector3 enemyWithOffset = new Vector3(_tpc.enemyTarget.position.x,  _tpc.enemyTarget.position.y+_heigthOffset, _tpc.enemyTarget.position.z);
            Vector3 normalizeDirection = (enemyWithOffset - transform.position).normalized;
            _aimDirection = normalizeDirection;
        }
        else _aimDirection = _tpc.transform.forward;

        transform.forward = _aimDirection;

    }

    private void FixedUpdate()
    {
        _rigidbody.position += _aimDirection * (speed * Time.deltaTime);
    }
    
}
