using UnityEngine;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace GoragarXGameDev.Utils
{
    /// <summary>
    /// A singleton is a class whose instantiation is restricted to a singular
    /// instance (Instance) with global access. 
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this as T;
        }
    }
}
