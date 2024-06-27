using System;
using UnityEngine;
using UnityEngine.VFX;
using StarterAssets;

namespace Elements
{
    public class FireElement : Element
    {        
        private void Start()
        {
            _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            _vfx = abilityPrefab.GetComponentInChildren<VisualEffect>();
            CastAbilityTrigger = Animator.StringToHash("ShootArrow");
            if (explosionPrefab != null) abilityPrefab.GetComponent<ObjectDamageDealer>().explosion = explosionPrefab;
        }
        
        public override void Shoot()
        {
            GameObject instance = Instantiate(abilityPrefab, instantiateTransform.position, Quaternion.identity);
            if (_tpc.Level == 2) instance.transform.GetChild(2).gameObject.SetActive(true);
            if (_tpc.Level == 3) instance.transform.GetChild(0).gameObject.SetActive(true);
        }

        protected override void SetAbilitySettingsByLevel()
        {
            int currentLevel = _tpc.Level;

            switch (currentLevel)
            {
                case 3:
                    _vfx.SetFloat("_vfxPhase2Rate", 100);
                    _vfx.SetFloat("_vfxFireSphereRadius", 2f);
                    _vfx.SetFloat("_vfxFireLifetimeFloat", 1f);
                    _vfx.SetFloat("_vfxFireSizeFloatA", 0.001f);
                    _vfx.SetVector3("_vfxFireMinVelocity", new Vector3(0f, 5f, -10f));
                    _vfx.SetVector3("_vfxFireMaxVelocity", new Vector3(0f, 20f, -30f));
                    break;
                case 2:
                    _vfx.SetFloat("_vfxPhase2Rate", 35);
                    _vfx.SetFloat("_vfxFireSphereRadius", 1f);
                    _vfx.SetFloat("_vfxFireLifetimeFloat", 0.8f);
                    _vfx.SetFloat("_vfxFireSizeFloatA", 0.3f);
                    _vfx.SetVector3("_vfxFireMinVelocity", new Vector3(-0.2f, 0.4f, -0.2f));
                    _vfx.SetVector3("_vfxFireMaxVelocity", new Vector3(0.35f, 2f, 0.35f));
                    break;
                default:
                    _vfx.SetFloat("_vfxPhase2Rate", 0);
                    break;
            }
        }
    }
}

