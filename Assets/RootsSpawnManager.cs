using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RootsSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject strRoots;

    [SerializeField]
    private GameObject circlRoots;

    private bool _circle = false;
    private float _timer = 0f;

    private readonly float _timeSpan = 7f/5.5f;

    public bool level3 = false;

    // Update is called once per frame
    void Update()
    {
        if (level3)
        {
            if (_timer <= 0)
            {
                if(!_circle) InstanceStrRoots();
                else InstanceCrclRoots();
                _timer = _timeSpan;
            }

            _timer -= 1f * Time.deltaTime;
        }
    }

    private void InstanceStrRoots()
    {
        Instantiate(strRoots, transform.position, transform.rotation);
    }
    
    private void InstanceCrclRoots()
    {
        Instantiate(circlRoots, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        _circle = true;
    }
}
