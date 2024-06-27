using StarterAssets;
using UnityEngine;
using UnityEngine.VFX;

namespace Elements
{
    public class EnergyElement : Element
    {
       
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

        }

        public override void Shoot()
        {

        }

        protected override void SetAbilitySettingsByLevel()
        {
            int currentLevel = _tpc.Level;

            switch (currentLevel)
            {
                case 3:

                    break;
                case 2:

                    break;
                default:
                    break;
            }
        }
    }
}



