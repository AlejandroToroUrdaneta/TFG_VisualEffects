using StarterAssets;
using Enemies;
using UnityEngine;

public class RootsSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject strRoots;

    [SerializeField]
    private GameObject circlRoots;


    private ThirdPersonController _tpc;
    private bool _targetExist = false;
    private float _timer = 0f;

    private readonly float _timeSpan = 7f/5.5f;

    public bool level3 = false;

    private void Start()
    {
        _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        if (_tpc.enemyTarget != null) _tpc.enemyTarget.GetComponent<ActiveNPC>().isBlocked =  _targetExist = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (level3)
        {
            if (_timer <= 0)
            {
                if(_targetExist && Vector3.Distance(transform.position,_tpc.enemyTarget.position) <= 4f) InstanceCrclRoots();
                else InstanceStrRoots();
                _timer = _timeSpan ;
            }

            _timer -= Time.deltaTime;
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
    
}
