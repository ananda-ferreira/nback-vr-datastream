using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;

using UnityEngine.SceneManagement;
using Task;

namespace Assets.Scripts.DataStream
{
    public class InGameOutlet : AFloatOutlet
    {
        public NBackTask Task {get; set;}
        public bool JustTriggered {get; set;}
        public bool JustStarted {get; set;}
        public bool JustFinished {get; set;}
        public void Reset()
        {
            StreamName = "Unity." + SceneManager.GetActiveScene().name + ".InGame";
            StreamType = "Unity.Marker";
            moment = MomentForSampling.EndOfFrame;
            IrregularRate = true; // because only when there is a trigger input
        }

        public override List<string> ChannelNames
        {
            get
            {
                List<string> chanNames = new List<string>();
                chanNames.AddRange(new string[] 
                {
                    "Triggered", 
                    "AnswerCorrect", 
                    "ReactionTime",
                    "N", // Alternatively move to seperate stream with irregular rate
                    "Started",
                    "Finished",
                });
                return chanNames;
            }
        }

        // protected override void ExtendHash(Hash128 hash)
        // {
        //     hash.Append(transformFormat.ToString());
        // }

        protected override bool BuildSample()
        {

            if (JustStarted || JustFinished) // not workng properly
            {
                sample[0] = 0.0f;
                sample[1] = 0.0f;
                sample[2] = 0.0f;
                sample[3] = 0.0f;
                sample[4] = JustStarted ? 1.0f : 0.0f;
                sample[5] = JustFinished ? 1.0f : 0.0f;
                Debug.Log($"InGameOutlet: Started {JustStarted}, Finished {JustFinished}");
                JustStarted = false;
                JustFinished = false; 
                return true;
            }
            //if (JustFinished)
            //{
            //    sample[0] = 0.0f;
            //    sample[1] = 0.0f;
            //    sample[2] = 0.0f;
            //    sample[3] = 0.0f;
            //    sample[4] = 0.0f;
            //    sample[5] = 1.0f;
            //    Debug.Log($"InGameOutlet: Started {JustStarted}, Finished {JustFinished}");
            //    JustFinished = false;
            //    return true;
            //}
            
            if ( Task == null ) return false;
            
            if (JustTriggered)
            {
                sample[0] = 1.0f;
                sample[1] = Task.IsCorrect ? 1.0f : 0.0f;
                sample[2] = Task.ReactionTime;
                sample[3] = Task.N;
                sample[4] = 0.0f;
                sample[5] = 0.0f;

                Debug.Log($"InGameOutlet: Triggered {Task.IsCorrect}");
                JustTriggered = false;
                return true;
            }
            else if (Task.IsCorrectChar && !JustTriggered)
            {
                sample[0] = 0.0f;
                sample[1] = Task.IsCorrect ? 1.0f : 0.0f; // should be false
                sample[2] = 0.0f; // no reaction time without trigger
                sample[3] = Task.N; // maybe cast to float
                sample[4] = 0.0f;
                sample[5] = 0.0f;
                Debug.Log($"InGameOutlet: Missed Trigger");
                return true;
            }
            return false;
        }
    }
}