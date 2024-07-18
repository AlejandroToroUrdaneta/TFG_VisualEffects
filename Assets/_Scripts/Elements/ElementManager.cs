using StarterAssets;
using UnityEngine;


namespace Elements
{
    public class ElementManager : MonoBehaviour
    {
        public ThirdPersonController tpc;
        public Animator tpcAnimator;
        public GameObject currentWeapon;
        
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

            if (_currentAnimatorLayer == "Bow Layer")
            {
                tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),0);
                currentWeapon.SetActive(false);
            }
            
            tpc.AbilityFunction = _nature.CastAbility;
            tpc.ShootFunction = _nature.Shoot;
            tpc.leveUpMaterial.SetColor("_Color",tpc.nColor);
            tpc.currentElement = ThirdPersonController.Element.Nature;
        }

        public void ChangeToFire()
        {
            _currentWeaponEquiped = "BowEquiped";
            tpcAnimator.SetBool(_currentWeaponEquiped, true);
            currentWeapon.SetActive(true);
            
            tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),0);
            _currentAnimatorLayer = "Bow Layer";
            tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),1);
            
            tpc.AbilityFunction = _fire.CastAbility;
            tpc.ShootFunction = _fire.Shoot;
            tpc.currentElement = ThirdPersonController.Element.Fire;
            tpc.leveUpMaterial.SetColor("_Color",tpc.fColor);
        }

        public void ChangeToEnergy()
        {
            tpcAnimator.SetBool(_currentWeaponEquiped, false);

            if (_currentAnimatorLayer == "Bow Layer")
            {
                tpcAnimator.SetLayerWeight(tpcAnimator.GetLayerIndex(_currentAnimatorLayer),0);
                currentWeapon.SetActive(false);
            }
            
            tpc.AbilityFunction = _energy.CastAbility;
            tpc.ShootFunction = _energy.Shoot;
            tpc.leveUpMaterial.SetColor("_Color",tpc.eColor);
            tpc.currentElement = ThirdPersonController.Element.Energy;
        }
    }
}

