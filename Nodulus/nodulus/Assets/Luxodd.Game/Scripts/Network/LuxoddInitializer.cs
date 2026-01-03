using UnityEngine;
using Luxodd.Game.Scripts.Network;
using Luxodd.Game.Scripts.Network.CommandHandler;

namespace Luxodd.Game.Scripts.Network
{
    /// <summary>
    /// Handles the initialization of the Luxodd network services.
    /// To be placed in the "Blank" scene.
    /// </summary>
    public class LuxoddInitializer : MonoBehaviour
    {
        [SerializeField] private WebSocketService _webSocketService;
        [SerializeField] private HealthStatusCheckService _healthStatusCheckService;

        private void Start()
        {
            if (_webSocketService == null)
            {
                Debug.LogError("[LuxoddInitializer] WebSocketService is not assigned!");
                return;
            }

            Debug.Log("[LuxoddInitializer] Connecting to Luxodd server...");
            _webSocketService.ConnectToServer(OnConnectionSuccess, OnConnectionFailure);
        }

        private void OnConnectionSuccess()
        {
            Debug.Log("[LuxoddInitializer] Connected to server successfully.");
            if (_healthStatusCheckService != null)
            {
                _healthStatusCheckService.Activate();
            }
        }

        private void OnConnectionFailure()
        {
            Debug.LogError("[LuxoddInitializer] Failed to connect to Luxodd server.");
        }
    }
}
