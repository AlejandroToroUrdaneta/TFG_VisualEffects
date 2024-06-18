using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements
{
    public class NatureElement : MonoBehaviour
    {
        public Camera cam;
        public GameObject projectile;
        public Transform firePoint;
        public float fireRate = 4;

        private Vector3 _destination;
        private float _timeToFire;
        private Projectile _projectileScript;

        public void CastAbility()
        {
            _timeToFire = Time.time + 1 / fireRate;
            ShootProjectile();

        }

        private void ShootProjectile()
        {
            if (cam != null)
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                _destination = ray.GetPoint(1000);
                InstantiateProjectile();
            }
            else
            {
                Debug.Log("B");
                InstantiateProjectileAtFirePoint();
            }
        }

        private void InstantiateProjectile()
        {
            var projectileObj = Instantiate(projectile, firePoint.position, quaternion.identity) as GameObject;

            _projectileScript = projectileObj.GetComponent<Projectile>();
            RotateToDestination(projectileObj, _destination, true);
            projectileObj.GetComponent<Rigidbody>().velocity = transform.forward * _projectileScript.speed;
        }

        private void InstantiateProjectileAtFirePoint()
        {
            var projectileObj = Instantiate(projectile, firePoint.position, quaternion.identity) as GameObject;
            
            _projectileScript = projectileObj.GetComponent<Projectile>();
            RotateToDestination(projectileObj, firePoint.transform.forward * 1000, true);
            projectileObj.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * _projectileScript.speed;
        }

        private void RotateToDestination(GameObject obj, Vector3 destination, bool onlyY)
        {
            var direction = destination - obj.transform.position;
            var rotation = Quaternion.LookRotation(direction);

            if (onlyY)
            {
                rotation.x = 0;
                rotation.z = 0;
            }

            obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
        }

       
    }
}

