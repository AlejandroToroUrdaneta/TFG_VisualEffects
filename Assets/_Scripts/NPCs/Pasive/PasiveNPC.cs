using UnityEngine;

namespace Enemies
{
    public class PassiveNPC : Enemy
    {
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
        }
        

        public override void TakeDamage(float damageAmount)
        {
            base.TakeDamage(0);
            
        }

        protected override void Die()
        {
            
        }
    }
}
