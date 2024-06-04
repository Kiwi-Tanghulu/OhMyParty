using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class FeedbackPlayer : MonoBehaviour
    {
        private List<Feedback> feedbacks;

        private void Awake()
        {
            feedbacks = new List<Feedback>();

            foreach (Transform feedback in transform)
            {
                feedbacks.Add(feedback.GetComponent<Feedback>());
            }
        }

        //private void Update()
        //{
        //    if(Input.GetKeyDown(KeyCode.Space))
        //        Play(transform);
        //}

        public void Play(Vector3 playPos)
        {
            for (int i = 0; i < feedbacks.Count; i++)
                feedbacks[i].Play(playPos);
        }
    }
}
