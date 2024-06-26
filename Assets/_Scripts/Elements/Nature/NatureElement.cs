using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;


namespace Elements
{
    public class NatureElement : MonoBehaviour{
        //attributes

        [SerializeField]
        private GameObject orb;

        [SerializeField]
        private GameObject capybara;
        
        [SerializeField]
        private Transform orbInstantiateTransform;
        
        [SerializeField]
        private Transform capybaraInstantiateTransform;
        
        
        private ThirdPersonController _tpc;
        private VisualEffect _vfx;
        private Animator _animator;
        
        private static readonly int CastOrb = Animator.StringToHash("CastOrb");

        private void Start()
        {
            //Initialize some attributes
            _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            _vfx = orb.GetComponentInChildren<VisualEffect>();
        }

        public void CastAbility( )
        {
            _animator.SetTrigger(CastOrb);
            SetAbilitySettingsByLevel();
        }

        public void Shoot()
        {
            //instantiate the projectile thats goin to be cast
           
            if (_tpc.Level > 1)
            {
                GameObject instance = Instantiate(orb, capybaraInstantiateTransform.position, Quaternion.identity);
                instance.transform.GetChild(1).gameObject.SetActive(true);

                if (_tpc.Level == 3)
                {
                    instance.transform.GetChild(1).gameObject.GetComponent<RootsSpawnManager>().level3 = true;
                }
                
            }
            else
            {
                GameObject instance = Instantiate(orb, orbInstantiateTransform.position, Quaternion.identity);

            }
        }

        private void SetAbilitySettingsByLevel()
        {
            int currentLevel = _tpc.Level;

            switch (currentLevel)
            {
                case 3:
                    _vfx.SetFloat("_vfxPhase1Rate", 0f);
                    _vfx.SetFloat("_vfxLeafSparkleFloat",15f);
                    _vfx.SetFloat("_vfxPhase2Rate",10f);
                    _vfx.SetFloat("_vfxLeafSparkleSphereRadius",0.4f);
                    _vfx.SetFloat("_vfxLeafSparkleSizeFloat",0.075f);
                    break;
                case 2:
                    _vfx.SetFloat("_vfxPhase1Rate", 0f);
                    _vfx.SetFloat("_vfxLeafSparkleFloat",5f);
                    _vfx.SetFloat("_vfxPhase2Rate",10f);
                    _vfx.SetFloat("_vfxLeafSparkleSphereRadius",0.2f);
                    _vfx.SetFloat("_vfxLeafSparkleSizeFloat",0.2f);
                    break;
                default:
                    _vfx.SetFloat("_vfxPhase1Rate",5f);
                    _vfx.SetFloat("_vfxLeafSparkleFloat",5f);
                    _vfx.SetFloat("_vfxPhase2Rate",0f);
                    _vfx.SetFloat("_vfxLeafSparkleSphereRadius",0.1f);
                    _vfx.SetFloat("_vfxLeafSparkleSizeFloat",0.1f);
                    break;
            }
            
        }
    }
}

