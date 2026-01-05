using System.Collections.Generic;
using UnityEngine;

namespace Luxodd.Game.Scripts.Input
{
    public static class ArcadeUnityMapping
    {
        public static readonly IReadOnlyDictionary<ArcadeButtonColor, KeyCode[]> ButtonToKeyCodes =
            new Dictionary<ArcadeButtonColor, KeyCode[]>()
            {
                // Many gamepads use 0 or 1 for the main confirm button (X on PS4 / A on Xbox)
                { ArcadeButtonColor.Black, new[] { KeyCode.JoystickButton0, KeyCode.JoystickButton1 } },
                { ArcadeButtonColor.Red, new[] { KeyCode.JoystickButton2, KeyCode.JoystickButton3 } },
                { ArcadeButtonColor.Green, new[] { KeyCode.JoystickButton4 } },
                { ArcadeButtonColor.Yellow, new[] { KeyCode.JoystickButton5 } },
                { ArcadeButtonColor.Blue, new[] { KeyCode.JoystickButton6 } },
                { ArcadeButtonColor.Purple, new[] { KeyCode.JoystickButton7 } },
                { ArcadeButtonColor.Orange, new[] { KeyCode.JoystickButton8 } },
                { ArcadeButtonColor.White, new[] { KeyCode.JoystickButton9 } },
            };

        public static readonly IReadOnlyDictionary<ArcadeButtonColor, KeyCode> ButtonToKeyboardCode =
            new Dictionary<ArcadeButtonColor, KeyCode>()
            {
                { ArcadeButtonColor.Black, KeyCode.Space },
                { ArcadeButtonColor.Red, KeyCode.Z },
                { ArcadeButtonColor.Green, KeyCode.X },
                { ArcadeButtonColor.Yellow, KeyCode.C },
                { ArcadeButtonColor.Blue, KeyCode.V },
                { ArcadeButtonColor.Purple, KeyCode.B },
                { ArcadeButtonColor.Orange, KeyCode.H },
                { ArcadeButtonColor.White, KeyCode.Escape },
            };

        public static KeyCode[] GetKeyCodes(ArcadeButtonColor buttonColor)
        {
            return ButtonToKeyCodes[buttonColor];
        }

        public static KeyCode GetKeyboardKeyCode(ArcadeButtonColor buttonColor)
        {
            return ButtonToKeyboardCode[buttonColor];
        }
    }
}