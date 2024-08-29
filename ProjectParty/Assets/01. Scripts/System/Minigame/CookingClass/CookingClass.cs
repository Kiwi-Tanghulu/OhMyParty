using UnityEngine;

namespace OMG.Minigames.CookingClass
{
    public class CookingClass : PlayableMinigame
    {
        protected override void OnGameInit()
        {
            base.OnGameInit();
            // Camera.main.clearFlags = CameraClearFlags.SolidColor;
            // Camera.main.backgroundColor = Color.black;
        }

        protected override void OnGameRelease()
        {
            base.OnGameRelease();
            // Camera.main.clearFlags = CameraClearFlags.Skybox;
        }
    }
}
