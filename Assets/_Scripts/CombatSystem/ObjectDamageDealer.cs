using System.Collections;
using Enemies;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;

public class ObjectDamageDealer : MonoBehaviour
{
    [SerializeField]
    private float _damage = 100.0f;
    
    [SerializeField]
    private bool _isDestroyable = false;
    
    public GameObject explosion;

    [SerializeField]
    private float delayExplosion;
    [SerializeField]
    private float waitForExplosion;

    [SerializeField]
    private LayerMask groundLayer;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy ))
        {
            enemy.TakeDamage(_damage);

            if (other.TryGetComponent(out ActiveNPC activeNpc) && activeNpc.isBlocked)
            {
                activeNpc.isBlocked = false;
            }
        }

        if (_isDestroyable)
        {
            if (explosion != null)
            {
                Invoke("Explode", waitForExplosion);
            }
            transform.gameObject.SetActive(false);
            Destroy(this.gameObject,delayExplosion+1f);
        }
        
    }

    private void Explode()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        GameObject instance;
        
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().currentElement
            == ThirdPersonController.Element.Energy)
        {
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);
            instance = Instantiate(explosion, hit.point, Quaternion.identity);
        }
        else
        {
            instance = Instantiate(explosion, transform.position, Quaternion.identity);
        }

        Destroy(instance, delayExplosion);
    }


}
