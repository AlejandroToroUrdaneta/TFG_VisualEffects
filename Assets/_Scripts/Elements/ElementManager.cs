using StarterAssets;
using UnityEngine;


namespace Elements
{
    public class ElementManager : MonoBehaviour
    {
        public ThirdPersonController tpc;
        public Animator tpcAnimator;
        
        private NatureElement _nature;
        private FireElement _fire;
        private EnergyElement _energy;
        private string _currentAnimatorLayer;
        private string _currentWeaponEquiped;
        
        
        
        
        private void Awake()
        {
            _nature = GetComponent<NatureElement>();
            _fire = GetComponent<FireElement>();
            _energy = GetComponent<EnergyElement>();
            _currentAnimatorLayer = "Bow Layer";
            _currentWeaponEquiped = "BowEquiped";
            tpc.AbilityFunction = ChangeToFire;
        }

        public void ChangeToNature()
        {
            tpcAnimator.SetBool(_currentWeaponEquiped, false);
            
            if(_currentAnimatorLayer == "Bow Layer") tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),0);
            
            tpc.AbilityFunction = _nature.CastAbility;
            tpc.ShootFunction = _nature.Shoot;
        }

        public void ChangeToFire()
        {
            _currentWeaponEquiped = "BowEquiped";
            tpcAnimator.SetBool(_currentWeaponEquiped, true);
            
            tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),0);
            _currentAnimatorLayer = "Bow Layer";
            tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),1);
            
            tpc.AbilityFunction = _fire.CastAbility;
            tpc.ShootFunction = _fire.Shoot;
        }

        public void ChangeToEnergy()
        {
            tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),0);
            /*tpcAnimator.SetBool(_currentWeaponEquiped, false);
             
            _currentWeaponEquiped = "CastEquiped";
            //set bool GunEquiped to true
            tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),0);
            //set cast Layer to 1*/
            
            _currentAnimatorLayer = "Base Layer";
            tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),1);
            
            tpc.AbilityFunction = _energy.CastAbility;
            tpc.ShootFunction = _energy.Shoot;
        }
    }
}

