using System.Collections;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class Invisibility : MazeAdventureItem
    {
        [SerializeField] private float invisibilityTime;
        private IInvisibility playerInvisibility;
        public override void Init(Transform playerTrm)
        {
            base.Init(playerTrm);
            playerInvisibility = playerTrm.GetComponent<IInvisibility>();
        }

        public override void OnActive()
        {
            StartCoroutine(TemporaryInvisibility());
        }

        private IEnumerator TemporaryInvisibility()
        {
            playerInvisibility.EnterInvisibil();
            yield return new WaitForSeconds(invisibilityTime);
            playerInvisibility.ExitInvisibil();
        }
    }
}
