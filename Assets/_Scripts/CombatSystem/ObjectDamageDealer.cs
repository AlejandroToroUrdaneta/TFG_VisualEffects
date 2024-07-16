using System.Collections;
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
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemies.ActiveNPC enemy ))
        {
            enemy.TakeDamage(_damage);
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
        GameObject instance = Instantiate(explosion, transform.position, quaternion.identity);
        Destroy(instance, delayExplosion);
    }


}
