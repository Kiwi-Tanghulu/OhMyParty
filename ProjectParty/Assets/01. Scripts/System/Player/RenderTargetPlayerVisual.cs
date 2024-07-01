using System;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class RenderTargetPlayerVisual : PlayerVisual
    {
        private Animator anim;

        private RenderTexture renderTexture;
        public RenderTexture RenderTexture => renderTexture;

        [SerializeField] private Camera renderCamera;

        private Dictionary<RenderTargetPlayerPoseType, int> PoseHashDic;

        public override void Init(ulong ownerID)
        {
            base.Init(ownerID);

            anim = GetComponent<Animator>();

            CreateRenderTexture();

            PoseHashDic = new Dictionary<RenderTargetPlayerPoseType, int>();
            foreach(RenderTargetPlayerPoseType type in Enum.GetValues(typeof(RenderTargetPlayerPoseType)))
            {
                PoseHashDic[type] = Animator.StringToHash(type.ToString());
            }
        }

        public void SetOwenrID(ulong ownerID)
        {
            this.ownerID = ownerID;
        }

        public void SetPose(RenderTargetPlayerPoseType poseType)
        {
            anim.SetTrigger(PoseHashDic[poseType]);
        }

        private void CreateRenderTexture()
        {
            RenderTexture rt = new RenderTexture(256, 256, 16);
            rt.Create();
            renderTexture = rt;

            renderCamera.targetTexture = renderTexture;
        }
    }
}