using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Elements
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 30f;
        public float slowDownRate = 0.01f;
        public float detectingDistance = 0.1f;
        public float destroyDelay = 5f;
        public float objectsToDetachDelay = 2f;
        public List<GameObject> objectsToDetacth = new List<GameObject>();
        [Space]
        public float erodeInRate = 0.06f;
        public float erodeOutRate = 0.03f;
        public float erodeRefreshRate = 0.01f;
        public float erodeAwayDelay = 1.25f;
        public List<SkinnedMeshRenderer> objectsToErode = new List<SkinnedMeshRenderer>();

        private Rigidbody _rb;
        private bool _stopped;

        private void Start()
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            if (GetComponent<Rigidbody>() != null)
            {
                _rb = GetComponent<Rigidbody>();
                StartCoroutine(SlowDown());
            }else Debug.Log("No RigidBody");

            if (objectsToDetacth != null) StartCoroutine(DetachObjects());

            if (objectsToErode != null) StartCoroutine(ErodeObjects());
            
            Destroy(gameObject, destroyDelay);
        }

        private void FixedUpdate()
        {
            if (!_stopped)
            {
                RaycastHit hit;
                Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

                if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectingDistance))
                {
                    transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                }
                Debug.DrawRay(distance,transform.TransformDirection(-Vector3.up * detectingDistance), Color.red);
            }
        }

        IEnumerator SlowDown()
        {
            float t = 1;

            while (t > 0)
            {
                _rb.velocity = Vector3.Lerp(Vector3.zero, _rb.velocity, t);
                t -= slowDownRate;
                yield return new WaitForSeconds(0.1f);
            }

            _stopped = true;
        }

        IEnumerator DetachObjects()
        {
            yield return new WaitForSeconds(objectsToDetachDelay);

            for (int i = 0; i < objectsToDetacth.Count; i++)
            {
                objectsToDetacth[i].transform.parent = null;
                Destroy(objectsToDetacth[i], objectsToDetachDelay);
            }
        }

        IEnumerator ErodeObjects()
        {
            for (int i = 0; i < objectsToErode.Count; i++)
            {
                float t = 1;

                while (t > 0)
                {
                    t -= erodeInRate;
                    objectsToErode[i].material.SetFloat("_Erode",t);
                    yield return new WaitForSeconds(erodeRefreshRate);
                }
            }

            yield return new WaitForSeconds(erodeAwayDelay);
            
            for (int i = 0; i < objectsToErode.Count; i++)
            {
                float t = 0;
                while (t < 1)
                {
                    t += erodeOutRate;
                    objectsToErode[i].material.SetFloat("_Erode",t);
                    yield return new WaitForSeconds(erodeRefreshRate);
                }
            }
            
        }
    }
}

