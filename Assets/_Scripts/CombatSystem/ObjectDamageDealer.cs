using Unity.Mathematics;
using UnityEngine;

public class ObjectDamageDealer : MonoBehaviour
{
    [SerializeField]
    private float _damage = 100.0f;
    
    [SerializeField]
    private bool _isDestroyable = false;
    
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float delayExplosion;
    

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
                GameObject instance = Instantiate(explosion, transform.position, quaternion.identity);
                Destroy(instance,delayExplosion);
            }
            transform.gameObject.SetActive(false);
            Destroy(this.gameObject,delayExplosion+1f);
        }
    }
    
}
