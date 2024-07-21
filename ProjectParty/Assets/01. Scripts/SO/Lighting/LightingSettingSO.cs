using UnityEditor;
using UnityEngine;

namespace OMG
{
    public enum EnvironmentSourceType
    {
        Skybox,
        Gradient,
        Color
    }

    [CreateAssetMenu(menuName = "SO/LightingSetting")]
    public class LightingSettingSO : ScriptableObject
    {
        [Header("Scene")]
        public LightingSettings LightingSettings;

        [Space]
        [Header("Environment")]
        public Material SkyboxMat;
        public EnvironmentSourceType Source;

        [Space]
        [Range(0, 8)]
        public float IntensityMultiplier = 1;

        [Space]
        [ColorUsage(false, true)]
        public Color SkyColor;
        [ColorUsage(false, true)]
        public Color EquatorColor;
        [ColorUsage(false, true)]
        public Color GroundColor;

        [Space]
        [ColorUsage(false, true)]
        public Color AmbientColor;

        [Space]
        [Header("Baked Lightmaps")]
        public LightingDataAsset LightingDataAsset;
    }
}