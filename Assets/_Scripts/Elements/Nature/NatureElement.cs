using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements
{
    public class NatureElement : MonoBehaviour{
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
                    
                    print("Nature on phase3");
                    break;
                case 2:
                    print("Nature on phase2");
                    break;
                default:
                    print("Nature on phase1");
                    break;
            }
            
        }
    }
}

