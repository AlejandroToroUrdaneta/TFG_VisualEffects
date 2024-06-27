using StarterAssets;
using UnityEngine;

namespace CombatSystem
{
    public class EnemyDamageDealer : MonoBehaviour
    {
        [SerializeField]
        private float weaponLength;
        [SerializeField]
        private float weaponDamage;
        
        private bool _canDealDamage;
        private bool _hasDealtDamage;

        private void Start()
        {
            _canDealDamage = _hasDealtDamage = false;
        }

        private void Update()
        {
            if (_canDealDamage && !_hasDealtDamage)
            {
                RaycastHit hit;
                int layerMask = 1 << 10; 

                if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
                {
                    if (hit.transform.TryGetComponent(out ThirdPersonController tpc))
                    {
                        tpc.TakeDamage(weaponDamage);
                        _hasDealtDamage = true;
                    }
                }
            }
        }

        public void StartDealDamage()
        {
            _canDealDamage = true;
            _hasDealtDamage = false;
        }

        public void EndDealDamage()
        {
            _canDealDamage = false;
            _hasDealtDamage = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position,transform.position-transform.up * weaponLength);
        }
    }
}

