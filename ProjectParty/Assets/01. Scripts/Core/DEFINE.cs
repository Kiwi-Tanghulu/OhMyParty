using OMG.UI;
using UnityEngine;

namespace OMG
{
    public static class DEFINE
    {
        public const string JOIN_CODE_KEY = "JoinCode";
        public const string OWNER_NAME_KEY = "OwnerName";

        public const string INTRO_SCENE = "IntroScene";
        public const string LOBBY_SCENE = "LobbyScene";

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

        private static MinigameCanvas minigameCanvas = null;
        public static MinigameCanvas MinigameCanvas {
            get {
                if(minigameCanvas == null)
                    minigameCanvas = GameObject.Find("MinigameCanvas")?.GetComponent<MinigameCanvas>();
                return minigameCanvas;
            }
        }
    }
}
