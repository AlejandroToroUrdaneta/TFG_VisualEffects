using UnityEngine;

public class ObjectDamageDealer : MonoBehaviour
{
    [SerializeField]
    private float _damage = 100.0f;

    [SerializeField]
    private bool _isDestroyable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemies.ActiveNPC enemy ))
        {
            print( $"EnemyDamage -{_damage} health = {enemy.GetRestingHealth()} ");
            enemy.TakeDamage(_damage);
        }
        if(_isDestroyable) Destroy(this.gameObject);
    }
    
}
