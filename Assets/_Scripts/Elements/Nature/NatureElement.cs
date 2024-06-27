using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;


namespace Elements
{
    public class NatureElement : Element
    {
        [SerializeField]
        private Transform capybaraInstantiateTransform;

        private void Start()
        {
            _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            _vfx = abilityPrefab.GetComponentInChildren<VisualEffect>();
            CastAbilityTrigger = Animator.StringToHash("CastOrb");
            if (explosionPrefab != null) abilityPrefab.GetComponent<ObjectDamageDealer>().explosion = explosionPrefab;
        }

        public override void CastAbility()
        {
            base.CastAbility();
            Vector3 pos = new Vector3(_tpc.transform.position.x, 0f, _tpc.transform.position.z);
            GameObject vfxTmp = Instantiate(anticipationPrefab, pos, quaternion.identity);
            Destroy(vfxTmp, 1.5f);
        }

        public override void Shoot()
        {
            GameObject instance = Instantiate(abilityPrefab, _tpc.Level > 1 ? capybaraInstantiateTransform.position : instantiateTransform.position, Quaternion.identity);
            if (_tpc.Level > 1)
            {
                instance.transform.GetChild(1).gameObject.SetActive(true);
                if (_tpc.Level == 3)
                {
                    instance.transform.GetChild(1).gameObject.GetComponent<RootsSpawnManager>().level3 = true;
                }
            }
        }

        protected override void SetAbilitySettingsByLevel()
        {
            int currentLevel = _tpc.Level;

            switch (currentLevel)
            {
                case 3:
                    _vfx.SetFloat("_vfxPhase1Rate", 0f);
                    _vfx.SetFloat("_vfxLeafSparkleFloat", 15f);
                    _vfx.SetFloat("_vfxPhase2Rate", 10f);
                    _vfx.SetFloat("_vfxLeafSparkleSphereRadius", 0.4f);
                    _vfx.SetFloat("_vfxLeafSparkleSizeFloat", 0.075f);
                    break;
                case 2:
                    _vfx.SetFloat("_vfxPhase1Rate", 0f);
                    _vfx.SetFloat("_vfxLeafSparkleFloat", 5f);
                    _vfx.SetFloat("_vfxPhase2Rate", 10f);
                    _vfx.SetFloat("_vfxLeafSparkleSphereRadius", 0.2f);
                    _vfx.SetFloat("_vfxLeafSparkleSizeFloat", 0.2f);
                    break;
                default:
                    _vfx.SetFloat("_vfxPhase1Rate", 5f);
                    _vfx.SetFloat("_vfxLeafSparkleFloat", 5f);
                    _vfx.SetFloat("_vfxPhase2Rate", 0f);
                    _vfx.SetFloat("_vfxLeafSparkleSphereRadius", 0.1f);
                    _vfx.SetFloat("_vfxLeafSparkleSizeFloat", 0.1f);
                    break;
            }
        }
    }
}




