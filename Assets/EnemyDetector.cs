using System;
using StarterAssets;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField]
    private ThirdPersonController _tpc;
    

    private void Start()
    {
        _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_tpc.enemyTarget == null)
        {
            _tpc.enemyTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_tpc.enemyTarget != null && other.gameObject == _tpc.enemyTarget.gameObject)
        {
            _tpc.enemyTarget = null;
        }
    }
}
