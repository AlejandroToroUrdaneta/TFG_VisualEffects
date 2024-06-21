using StarterAssets;
using UnityEngine;

namespace Elements
{
    // Declarate Atributes to perform the ability and visuals
    
    public class EnergyElement : MonoBehaviour
    {
        //attributes
        private ThirdPersonController _tpc;

        private void Start()
        {
            //Initialize some attributes
            _tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        }

        public void CastAbility( )
        {
            SetAbilitySettingsByLevel();
        }

        public void Shoot()
        {
            //instantiate the projectile thats goin to be cast
        }

        private void SetAbilitySettingsByLevel()
        {
            int currentLevel = _tpc.Level;

            switch (currentLevel)
            {
                case 3:
                    
                    print("Energy on phase3");
                    break;
                case 2:
                    print("Energy on phase2");
                    break;
                default:
                    print("Energy on phase1");
                    break;
            }
            
        }
    }
}



