using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        private PlayerVisualType visualType;
        public PlayerVisualType VisualType => visualType;

        private SkinnedMeshRenderer render;

        private void Awake()
        {
            render = transform.Find("Skin").GetComponent<SkinnedMeshRenderer>();
        }

        private void Start()
        {
            SetVisual(PlayerVisualType.BoxHead);//test
        }

        public void SetVisual(PlayerVisualType newVisualType)
        {
            visualType = newVisualType;

            Debug.Log(PlayerManager.Instance);
            Debug.Log(PlayerManager.Instance.playerVisualInfo);
            Debug.Log(PlayerManager.Instance.playerVisualInfo[visualType]);
            Debug.Log(render);

            render.sharedMesh = PlayerManager.Instance.playerVisualInfo[visualType];
        }
    }
}