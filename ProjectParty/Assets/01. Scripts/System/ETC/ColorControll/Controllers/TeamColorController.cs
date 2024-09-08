using UnityEngine;

namespace OMG.ColorControlling
{
    public class TeamColorController : ColorController
    {
        [SerializeField] bool teamFlag = true;

        protected override Color GetColor()
        {
            return PlayerManager.Instance.GetTeamColor(teamFlag);
        }
    }
}