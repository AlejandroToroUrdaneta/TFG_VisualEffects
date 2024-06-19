using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class EquipmentSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject weaponHolder;

        [SerializeField]
        private GameObject weapon;

        [SerializeField]
        private GameObject weaponSheath;


        private GameObject _currentWeaponInHand;
        private GameObject _currentWeaponInSheath;

        private void Start()
        {
            if (weapon != null) _currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        }

        public void DrawWeapon()
        {
            _currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
            Destroy(_currentWeaponInSheath);
        }

        public void SheathWeapon()
        {
            _currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
            Destroy(_currentWeaponInHand);
        }
    }

}
