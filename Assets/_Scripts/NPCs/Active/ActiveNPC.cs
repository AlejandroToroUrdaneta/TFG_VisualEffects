using CombatSystem;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class ActiveNPC : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField]
        private float health = 150f;
        [SerializeField]
        [Tooltip("time that takes the animation Dying to play")]
        private float _dyingDelay = 6.0f;

        [Header("Combat")]
        [SerializeField]
        [Tooltip("Time it waits to attack again")]
        private float attackCD = 3.0f;

        [SerializeField]
        [Tooltip("From how far it can deal damage")]
        private float attackRange = 1.0f;

        [SerializeField]
        [Tooltip("From how far starts to attack the player")]
        private float aggroRange = 4.0f;
        
        
        // NPC Components
        private ThirdPersonController _player;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private CharacterController _controller;
        private NPCSpawner _spawner;

        private float _timePassed;
        private float _newDestinationCD = 0.5f;
        private float _patrolSpeed = 3.5f;
        private float _seekSpeed = 4.5f;
        private bool _Swing;
        private int _currentWaypoint = 0;
        
        public Transform[] waypoints;
        public bool isBlocked = false;
        
        // ID animations
        private int _animSpeedId;
        private int _animDamageId;
        private int _animAttackId;
        private int _animSwingId;
        private int _animDeathId;
        
        // Animations Events
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] 
        public float FootstepAudioVolume = 0.5f;
        

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
            _spawner = GameObject.FindWithTag("Spawner").GetComponent<NPCSpawner>();
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _controller = GetComponent<CharacterController>();
            
            AssignAnimationIDs();
        }
        
        private void  AssignAnimationIDs()
        {
            _animSpeedId = Animator.StringToHash("NpcSpeed");
            _animDamageId = Animator.StringToHash("NpcDamage");
            _animAttackId = Animator.StringToHash("NpcAttack");
            _animSwingId = Animator.StringToHash("NpcSwing");
            _animDeathId = Animator.StringToHash("NpcDeath");
        }

        private void Update()
        {
            if (health > 0 && !isBlocked)
            {
                Vector3 playerPos = _player.transform.position;

                if (_timePassed >= attackCD)
                {
                    if (Vector3.Distance(playerPos, transform.position) <= attackRange)
                    {
                        _Swing = !_Swing;
                        _animator.SetBool(_animSwingId, _Swing);
                        _animator.SetTrigger(_animAttackId);
                        _timePassed = 0;
                    }
                }

                _timePassed += Time.deltaTime;
                
                if (_newDestinationCD <= 0)
                {
                    if (Vector3.Distance(playerPos, transform.position) <= aggroRange)//seek the target
                    {
                        _animator.SetFloat(_animSpeedId,_navMeshAgent.velocity.magnitude / _seekSpeed);
                        _navMeshAgent.speed = _seekSpeed;
                        _navMeshAgent.SetDestination(playerPos);
                        transform.LookAt(_player.transform);
                    }
                    else //patrol
                    {
                        _navMeshAgent.speed = _patrolSpeed;
                        _animator.SetFloat(_animSpeedId,_navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
                        if (Vector3.Distance(waypoints[_currentWaypoint].position, transform.position) < 1f)
                        {
                            _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
                        }
                        _navMeshAgent.SetDestination(waypoints[_currentWaypoint].position);
                        transform.LookAt(waypoints[_currentWaypoint].position);
                    }
                    _newDestinationCD = 0.5f;
                    
                }

                _newDestinationCD -= Time.deltaTime;
                
            }
            else
            {
                _animator.SetFloat(_animSpeedId,0);
            }
            
        }
        

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            _animator.SetTrigger(_animDamageId);

            if (health <= 0)
            {
                Die();
            }
        }
        
        private void Die()
        {
            _spawner.Invoke(nameof(NPCSpawner.SpawnNPC),_dyingDelay - 0.1f);
            _animator.SetTrigger(_animDeathId); 
            _animator.SetFloat(_animSpeedId, 1f);
            Destroy(this.gameObject, _dyingDelay);
        }
        
        public void StartDealDamage()
        {
            EnemyDamageDealer[] children = GetComponentsInChildren<EnemyDamageDealer>();

            foreach (EnemyDamageDealer eDD in children)
            {
                eDD.StartDealDamage();
            }
        }

        public void EndDealDamage()
        {
            EnemyDamageDealer[] children = GetComponentsInChildren<EnemyDamageDealer>();

            foreach (EnemyDamageDealer eDD in children)
            {
                eDD.EndDealDamage();
            }
        }
        
        
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = UnityEngine.Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;;
            Gizmos.DrawWireSphere(transform.position,attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,aggroRange);
        }

        public float GetRestingHealth()
        {
            return health;
        }
    }
}
