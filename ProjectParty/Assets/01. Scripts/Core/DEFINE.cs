using UnityEngine;

namespace OMG
{
    public static class DEFINE
    {
        public const string LOBBY_CLOSED = "closed";

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

        private static Transform minigameCanvas = null;
        public static Transform MinigameCanvas {
            get {
                if(minigameCanvas == null)
                    minigameCanvas = GameObject.Find("MinigameCanvas")?.transform;
                return minigameCanvas;
            }
        }
    }
}
