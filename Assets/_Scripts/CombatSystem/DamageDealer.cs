using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField]
        private float weaponLength;
        [SerializeField]
        private float weaponDamage;
        [SerializeField]
        private bool _canDealDamage;
        
        private List<GameObject> _hasDealtDamage;

        private void Start()
        {
            if (_canDealDamage)
            {
                RaycastHit hit;

                int layerMask = 1 << 11; 

                if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
                {
                    if (hit.transform.TryGetComponent(out Enemies.ActiveNPC enemyA) /*|| hit.transform.TryGetComponent(out Enemies.PasiveNPC enemyP))*/
                        && !_hasDealtDamage.Contains(hit.transform.gameObject))
                    {
                        print("DAMAGING THE ENEMY RAAAAAAAAA");
                        /*if (enemyA != null)*/ enemyA.TakeDamage(weaponDamage);
                        /*else enemyP.TakeDamage(weaponDamage);*/
                         
                        _hasDealtDamage.Add(hit.transform.gameObject);
                    }
                }
            }
        }

        public void StartDealDamage()
        {
            _canDealDamage = true;
            _hasDealtDamage.Clear();
        }

        public void EndDealDamage()
        {
            _canDealDamage = false;
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position,transform.position-transform.up * weaponLength);
        }
  
    }
}

