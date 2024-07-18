using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField]
        protected float health = 150f;
        
        [SerializeField]
        [Tooltip("time that takes the animation Dying to play")]
        protected float _dyingDelay = 6.0f;
        
        protected Animator _animator;
        protected CharacterController _controller;
        
        protected int animDamageId;

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
            AssignAnimationIDs();
        }

        protected virtual void AssignAnimationIDs()
        {
            animDamageId = Animator.StringToHash("NpcDamage");
        }

        public virtual void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            _animator.SetTrigger(animDamageId);

            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject, _dyingDelay);
        }
    }
}