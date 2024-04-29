using UnityEngine;

namespace OMG
{
    public static class DEFINE
    {
        public const int FOCUSED_PRIORITY = 20;
        public const int UNFOCUSED_PRIORITY = 1;

        public static AudioSource GlobalAudioPlayer { get; set; }

        private static Transform mainCanvas = null;
        public static Transform MainCanvas {
            get {
                if(mainCanvas == null)
                    mainCanvas = GameObject.Find("MainCanvas")?.transform;
                return mainCanvas;
            }
        }
    }
}
