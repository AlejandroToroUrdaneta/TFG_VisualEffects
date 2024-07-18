using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.VFX;

namespace Elements
{
    public class EnergyElement : Element
    {
        [SerializeField]
        private List<Vector3> _points = new List<Vector3>();
        

        
        private void Start()
        {
            _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            _vfx = abilityPrefab.GetComponentInChildren<VisualEffect>();
            CastAbilityTrigger = Animator.StringToHash("ShootBullet");
        }

        public override void CastAbility() 
        {
            base.CastAbility();
            _tpc.Shoot();
        }

        public override void Shoot()
        {
            GameObject instance = Instantiate(abilityPrefab, instantiateTransform.position, Quaternion.identity);
            instance.GetComponent<ObjectDamageDealer>().explosion = explosionPrefab;
            if(_tpc.Level >= 3){
                for (int i = 0; i < _points.Count; i++)
                {
                    GameObject ins = Instantiate(abilityPrefab, _points[i]+instance.transform.position, instance.transform.rotation,instance.transform);
                    ins.GetComponent<SphereCollider>().enabled = false;
                    ins.transform.localScale *= 0.5f;
                }
            }
        }

        protected override void SetAbilitySettingsByLevel()
        {
            int currentLevel = _tpc.Level;
            VisualEffect vfxpld = explosionPrefab.GetComponent<VisualEffect>();
            
            switch (currentLevel)
            {
                case 3:
                    _vfx.SetFloat("_vfxPhase2Rate", 1);
                    vfxpld.SetFloat("_vfxPhase2RateFloat", 1);
                    break;
                case 2:
                    _vfx.SetFloat("_vfxPhase2Rate", 1);
                    vfxpld.SetFloat("_vfxPhase2RateFloat", 1);
                    break;
                default:
                    _vfx.SetFloat("_vfxPhase2Rate", 0);
                    vfxpld.SetFloat("_vfxPhase2RateFloat", 0);
                    break;
            }
        }
        
    }
}



