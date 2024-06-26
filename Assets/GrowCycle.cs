using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCycle : MonoBehaviour
{
    [SerializeField]
    private Shader growShader;

    private float _timer = 0;
    private float _visible = 0;
    private float _invisible = 0;
    
    private List<Material> mats = new List<Material>();
    
    private Material sharedMaterial;
    private Material material;
    
    // Start is called before the first frame update
    void Start()
    {
        sharedMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

        foreach (Transform child in transform)
        {
           mats.Add(child.GetComponent<MeshRenderer>().material = new Material(sharedMaterial));
        }
    }
    
    void Update()
    {
        foreach (Material currentMat in mats)
        {
            float cycleVelocity = 0.002f;
            _visible += cycleVelocity;
            currentMat.SetFloat("_Grow", _visible);
            if (_timer >= 1.2f)
            {
                _invisible += cycleVelocity;
                currentMat.SetFloat("_Dissapear", _invisible);
            }

            _timer += Time.deltaTime;

            if (currentMat.GetFloat("_Dissapear") >= 1f) Destroy(this.gameObject,0.25f);
        }
        

    }

    private void SetMatSettings()
    {
        sharedMaterial.SetFloat("_Clip",1f);
        sharedMaterial.SetColor("_Color", new Color(0.0904f,0.1490f,0.0392f));
        sharedMaterial.SetFloat("_Smoothness",0f);
        sharedMaterial.SetFloat("_Metallic",0.5f);
        sharedMaterial.SetFloat("Grow",0.2f);
        sharedMaterial.SetFloat("_Scale",-0.01f);
        sharedMaterial.SetFloat("_Dissapear",0f);
    }
}
