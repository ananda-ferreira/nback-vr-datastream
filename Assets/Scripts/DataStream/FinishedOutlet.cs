using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;

using UnityEngine.SceneManagement;
using Task;


namespace Assets.Scripts.DataStream
{
    public class FinishedOutlet : AStringOutlet
    {
        public NBackTask Task {get; set;}
        public bool JustFinished {get; set;}
        public string MarkerText {get; set;}
        public void Reset()
        {
            StreamName = "Unity." + SceneManager.GetActiveScene().name + ".Finished";
            StreamType = "Unity.Marker";
            moment = MomentForSampling.EndOfFrame; // Update instead?
            IrregularRate = true; // because only when there is a trigger input
        }

        public override List<string> ChannelNames
        {
            get
            {
                List<string> chanNames = new();
                chanNames.AddRange(new string[]{"TaskFinished"});
                return chanNames;
            }
        }

        protected override bool BuildSample()
        {
            
            if (JustFinished)
            {
                sample[0] = MarkerText;
                Debug.Log("FinishedOutlet: " + sample[0]);
                JustFinished = false;
                return true;
            }
            return false;
        }
    }
}