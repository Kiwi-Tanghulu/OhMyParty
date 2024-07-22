using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OMG
{
    public static class LightingManager
    {
        public static void SetLightingSetting(LightingSettingSO lightingSettingSO)
        {
            if (lightingSettingSO == null)
                return;

            RenderSettings.skybox = lightingSettingSO.SkyboxMat;

            switch (lightingSettingSO.Source)
            {
                case EnvironmentSourceType.Skybox:
                    RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
                    RenderSettings.ambientIntensity = lightingSettingSO.IntensityMultiplier;
                    break;
                case EnvironmentSourceType.Gradient:
                    RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
                    RenderSettings.ambientSkyColor = lightingSettingSO.SkyColor;
                    RenderSettings.ambientEquatorColor = lightingSettingSO.EquatorColor;
                    RenderSettings.ambientGroundColor = lightingSettingSO.GroundColor;
                    break;
                case EnvironmentSourceType.Color:
                    RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
                    RenderSettings.ambientLight = lightingSettingSO.AmbientColor;
                    break;
            }
        }
    }
}
