using UnityEngine;

namespace OMG.Skins
{
    [CreateAssetMenu(menuName = "SO/Skins/SkinData")]
    public class SkinSO : ScriptableObject
    {
        public string SkinName = "";
        public Skin SkinPrefab = null;
    }
}
