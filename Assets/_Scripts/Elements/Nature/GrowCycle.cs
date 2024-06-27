using System.Collections.Generic;
using UnityEngine;

public class GrowCycle : MonoBehaviour
{
    [SerializeField]
    private bool circle = false;
    
    //timers 
    private float _timer = 0;
    private float _visible = 0;
    private float _invisible = 0;
    
    //settings
    private float _cycleAppearanceVelocity;
    private float _cycleDissapearanceVelocity;
    private float _timeOffset;
    
    
    private Material sharedMaterial;
    private Material material;
    
    private List<Material> mats = new List<Material>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        sharedMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

        foreach (Transform child in transform)
        {
           mats.Add(child.GetComponent<MeshRenderer>().material = new Material(sharedMaterial));
        }

        SetTimesSettings();
    }
    
    void Update()
    {
        foreach (Material currentMat in mats)
        {
            
            _visible += _cycleAppearanceVelocity;
            currentMat.SetFloat("_Grow", _visible);
            if (_timer >= _timeOffset)
            {
                _invisible += _cycleDissapearanceVelocity;
                currentMat.SetFloat("_Dissapear", _invisible);
            }

            _timer += Time.deltaTime;

            if (currentMat.GetFloat("_Dissapear") >= 1f) Destroy(this.gameObject,0.25f);
        }
        

    }

    private void SetTimesSettings()
    {
        if (circle)
        {
            _cycleAppearanceVelocity = 0.02f;
            _cycleDissapearanceVelocity = 0.0025f;
            _timeOffset = 2.4f;
        }
        else
        {
            _cycleAppearanceVelocity = 0.01f;
            _cycleDissapearanceVelocity = 0.005f;
            _timeOffset = 1.2f;
        }
    }
    
}
