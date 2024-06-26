using StarterAssets;
using UnityEngine;
using UnityEngine.VFX;

namespace Elements
{
    public class FireElement : MonoBehaviour
    {
           
        [SerializeField]
        private GameObject arrow;

        [SerializeField]
        private GameObject phoenix;

        [SerializeField]
        private Transform arrowInstantiateTransform;
        
        private ThirdPersonController _tpc;
        private VisualEffect _vfx;
        private Animator _animator;
        
        private static readonly int ShootArrow = Animator.StringToHash("ShootArrow");


        private void Start()
        {
            _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            _vfx = arrow.GetComponentInChildren<VisualEffect>();
        }

        public void CastAbility( )
        {
            _animator.SetTrigger(ShootArrow);
            SetAbilitySettingsByLevel();
        }

        public void Shoot()
        {
            GameObject instance = Instantiate(arrow, arrowInstantiateTransform.position, Quaternion.identity);
            if(_tpc.Level == 2) instance.transform.GetChild(2).gameObject.SetActive(true);
            if(_tpc.Level == 3) instance.transform.GetChild(0).gameObject.SetActive(true);
            
        }

        private void SetAbilitySettingsByLevel()
        {
            int currentLevel = _tpc.Level;

            switch (currentLevel)
            {
                case 3:
                    
                    _vfx.SetFloat("_vfxPhase2Rate", 100);
                    _vfx.SetFloat("_vfxFireSphereRadius", 2f);
                    _vfx.SetFloat("_vfxFireLifetimeFloat", 1f);
                    _vfx.SetFloat( "_vfxFireSizeFloatA", 0.001f);
                    _vfx.SetVector3("_vfxFireMinVelocity", new Vector3(0f,5f,-10f));
                    _vfx.SetVector3("_vfxFireMaxVelocity", new Vector3(0f,20f,-30f));
                    break;
                case 2:
                    _vfx.SetFloat("_vfxPhase2Rate", 35);
                    _vfx.SetFloat("_vfxFireSphereRadius", 1f);
                    _vfx.SetFloat("_vfxFireLifetimeFloat", 0.8f);
                    _vfx.SetFloat( "_vfxFireSizeFloatA", 0.3f);
                    _vfx.SetVector3("_vfxFireMinVelocity", new Vector3(-0.2f,0.4f,-0.2f));
                    _vfx.SetVector3("_vfxFireMaxVelocity", new Vector3(0.35f,2f,0.35f));
                    break;
                default:
                    _vfx.SetFloat("_vfxPhase2Rate", 0);
                    break;
            }
            
        }
    }
}

