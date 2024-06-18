using System;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class VisionField : MonoBehaviour
    {
        [SerializeField] ActiveNPC _activeNpc;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                _activeNpc.target = other.gameObject;
                _activeNpc.isChasing = true;
            }
        }
        
    }
}
