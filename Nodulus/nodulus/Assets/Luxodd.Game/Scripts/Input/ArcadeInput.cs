using UnityEngine;

namespace Luxodd.Game.Scripts.Input
{
    public enum ArcadeButtonColor
    {
        Black,
        Red,
        Green,
        Yellow,
        Blue,
        Purple,
        Orange,
        White
    }

    public static class ArcadeInput
    {
        public static bool GetButton(ArcadeButtonColor buttonColor)
        {
            foreach (var code in ArcadeUnityMapping.GetKeyCodes(buttonColor))
            {
                if (UnityEngine.Input.GetKey(code)) return true;
            }
            return false;
        }
        
        public static bool GetButtonDown(ArcadeButtonColor buttonColor)
        {
            foreach (var code in ArcadeUnityMapping.GetKeyCodes(buttonColor))
            {
                if (UnityEngine.Input.GetKeyDown(code)) return true;
            }
            return false;
        }
        
        public static bool GetButtonUp(ArcadeButtonColor buttonColor)
        {
            foreach (var code in ArcadeUnityMapping.GetKeyCodes(buttonColor))
            {
                if (UnityEngine.Input.GetKeyUp(code)) return true;
            }
            return false;
        }
        
        public static float Horizontal => UnityEngine.Input.GetAxis("Horizontal");
        public static float Vertical => UnityEngine.Input.GetAxis("Vertical");
    }
}
