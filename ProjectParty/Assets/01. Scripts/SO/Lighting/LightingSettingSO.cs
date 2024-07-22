using OMG.Editors;
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

        [ConditionalField("Source", (int)EnvironmentSourceType.Skybox)] [Range(0, 8)]
        public float IntensityMultiplier = 1;

        [ConditionalField("Source", (int)EnvironmentSourceType.Gradient)] [ColorUsage(false, true)]
        public Color SkyColor;
        [ConditionalField("Source", (int)EnvironmentSourceType.Gradient)] [ColorUsage(false, true)]
        public Color EquatorColor;
        [ConditionalField("Source", (int)EnvironmentSourceType.Gradient)] [ColorUsage(false, true)]
        public Color GroundColor;

        [ConditionalField("Source", (int)EnvironmentSourceType.Color)] [ColorUsage(false, true)]
        public Color AmbientColor;
    }
}