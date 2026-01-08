using System;
using Core.Data;
using UnityEngine;

namespace View.Control
{
    /// <summary>
    /// Defines game actions for Nodulus, not physical buttons.
    /// This abstraction keeps gameplay logic hardware-independent.
    /// </summary>
    public interface INodulusInputAdapter
    {
        /// <summary>
        /// Current joystick direction for navigation
        /// </summary>
        Direction NavigationDirection { get; }
        
        /// <summary>
        /// True when joystick is centered (no navigation input)
        /// </summary>
        bool IsNavigationCentered { get; }
        
        /// <summary>
        /// Fired when player wants to perform an action on selected node
        /// </summary>
        event Action ActionButtonPressed;
        
        /// <summary>
        /// Fired when player wants to perform a directional action (push/pull)
        /// </summary>
        event Action<Direction> DirectionalActionPressed;
    }
}
