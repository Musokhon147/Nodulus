using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace View.Control
{
    /// <summary>
    /// Automatically switches to a specified scene after a delay.
    /// </summary>
    public class TimedSceneSwitcher : MonoBehaviour
    {
        [Tooltip("The name of the scene to load.")]
        [SerializeField] private string _nextSceneName;

        [Tooltip("Delay in seconds before switching.")]
        [SerializeField] private float _delaySeconds;

        private IEnumerator Start()
        {
            if (string.IsNullOrEmpty(_nextSceneName))
            {
                Debug.LogWarning($"[{gameObject.name}] Next scene name is not set!");
                yield break;
            }

            yield return new WaitForSeconds(_delaySeconds);
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}
