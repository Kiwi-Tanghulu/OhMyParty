using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public interface IInvisibility
    {
        public bool IsInvisibil { get; }
        public void EnterInvisibil();
        public void ExitInvisibil();
    }

}
