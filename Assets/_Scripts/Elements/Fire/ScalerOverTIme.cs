using UnityEngine;

public class ScalerOverTIme : MonoBehaviour
{
    private float _MaxScale = 30f;
    private Vector3 _scaleChange;
    
    void Start()
    {
        float scaleStep = _MaxScale / 250f;
        transform.rotation = transform.parent.rotation;
        _scaleChange = new Vector3(scaleStep, scaleStep, scaleStep);
        
    }
    
    void Update()
    {
        if(transform.localScale.x < 5+_MaxScale) transform.localScale += _scaleChange*Time.deltaTime;
    }
}
