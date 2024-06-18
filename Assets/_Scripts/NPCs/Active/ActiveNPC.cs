using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

namespace _Scripts.Enemies
{
    public class ActiveNPC : MonoBehaviour
    {   
        //NPC components
        private NavMeshAgent _navMeshAgent;
        private CharacterController _controller;
        private Animator _animator;

        public GameObject target;
        
        // animation IDs
        private int _animIDNpcSpeed;
        private int _animIDNpcMotionSpeed;
        private int _animIDNpcAttack;
        private int _animIDNpcChangeFist;
        
        // animation Clips
        public AudioClip landingAudioClip;
        public AudioClip[] footstepAudioClips;
        [Range(0, 1)] public float footstepAudioVolume = 0.5f;
        
        //NPC atributtes
        private const float SpeedPatroling = 2.0f;
        private const float SpeedChasing = 4.5f;
        private const float SpeedChangeRate = 10.0f;
        private const float RotationSmoothTime = 0.12f;

        private Vector3 Center => new Vector3(0.0f, 0.93f, 0.0f);

        private float _animationBlend;
        private float _speed;
        private float _rotationVelocity;
        private float _targetRotation = 0.0f;
        private int _currentWaypoint = 0;
        
        public float life = 100f;
        public float damage = 5.0f;
        public bool isChasing = false;
        public bool analogMovement = true;
        
        public float speedChangeRate = 10.0f;
        public Transform[] waypoints;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _navMeshAgent.speed = SpeedPatroling;
            //activar en el animator caminar
            AssignAnimationIDs();
        }

        private void Update()
        {
            if (!isChasing) Patrol();
            else ChaseTarget();

        }


        private void Patrol()
        {
            if (waypoints.Length == 0) return;

            float distanceToCurrentWayPoint = Vector3.Distance(waypoints[_currentWaypoint].position, transform.position);

            if (distanceToCurrentWayPoint <= 2.25f)
            {
                _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
            }
            
            _animationBlend = Mathf.Lerp(_animationBlend, SpeedPatroling, Time.deltaTime * speedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;
            
            _animator.SetFloat(_animIDNpcSpeed, _animationBlend);
            _animator.SetFloat(_animIDNpcMotionSpeed, 1f);
            
            Move(waypoints[_currentWaypoint].position - transform.position);
        }

        private void ChaseTarget()
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);

            Vector3 lastPoint = path.corners[^1];
            lastPoint.y = target.transform.position.y;
            if (lastPoint != target.transform.position || distance > 10f)
            {
                target = null;
                isChasing = false;
            }
            else if (distance <= 1.5f)
            {
                    Attack();
            }else{
                _animator.SetBool(_animIDNpcAttack,false);
                _animationBlend = Mathf.Lerp(_animationBlend, SpeedChasing, Time.deltaTime * speedChangeRate);
                if (_animationBlend < 0.01f) _animationBlend = 0f;
            
                _animator.SetFloat(_animIDNpcSpeed, _animationBlend);
                _animator.SetFloat(_animIDNpcMotionSpeed, 1f);

                
                Move(target.transform.position - transform.position);
            }

            
        }

        private void Attack()
        {
            Vector3 direction = new Vector3(target.transform.position.x, 0.0f, target.transform.position.z);
            transform.LookAt(direction);
            _animator.SetBool(_animIDNpcAttack,true);
        }
        
        private void Move(Vector3 direction)
        {

            float targetSpeed = isChasing ? SpeedChasing : SpeedPatroling;
            

            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.001f;
            float inputMagnitude = analogMovement ? (direction * targetSpeed).magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;
            
            Vector3 inputDirection = direction.normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (direction != Vector3.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime));


            _animator.SetFloat(_animIDNpcSpeed, _animationBlend);
            _animator.SetFloat(_animIDNpcMotionSpeed, inputMagnitude);

        }
        
        
        
        
        
        private void AssignAnimationIDs()
        {
            _animIDNpcSpeed = Animator.StringToHash("NpcSpeed");
            _animIDNpcMotionSpeed = Animator.StringToHash("NpcMotionSpeed");
            _animIDNpcAttack = Animator.StringToHash("NpcAttacking");
            _animIDNpcChangeFist = Animator.StringToHash("NpcChangeFist");
        }
        
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (footstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, footstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(footstepAudioClips[index], transform.TransformPoint(Center), footstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(landingAudioClip, transform.TransformPoint(Center), footstepAudioVolume);
            }
        }
    }
}
