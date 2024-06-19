using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDamageDealer : MonoBehaviour
{
    private float _damage = 100.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemies.ActiveNPC enemy ))
        {
            print( $"EnemyDamage -{_damage} health = {enemy.GetRestingHealth()} ");
            enemy.TakeDamage(_damage);
        }
    }
}
