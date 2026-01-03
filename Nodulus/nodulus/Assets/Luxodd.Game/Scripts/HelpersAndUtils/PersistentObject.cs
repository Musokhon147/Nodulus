using UnityEngine;

namespace Luxodd.Game.Scripts.HelpersAndUtils
{
    /// <summary>
    /// Simple component to make a GameObject persistent across scenes.
    /// Useful for the Network prefab.
    /// </summary>
    public class PersistentObject : MonoBehaviour
    {
        private static PersistentObject _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
