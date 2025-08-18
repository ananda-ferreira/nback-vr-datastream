// Temporary Copy of GazeOutlet
using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;

using UnityEngine.SceneManagement;
using Assets.Scripts.Util;

namespace Assets.Scripts.DataStream
{
    
    public class EyeGazeOutlet : AFloatOutlet
    {
        private EyeGazeBehaviour GazeUpdater;
        public void Reset()
        {
            StreamName = $"Unity.{SceneManager.GetActiveScene().name}.Gaze";
            StreamType = "Unity.Transform";
            moment = MomentForSampling.EndOfFrame;
        }

        public override List<string> ChannelNames
        {
            get
            {
                List<string> chanNames = new List<string>();
                chanNames.AddRange(new string[] { "PosX", "PosY", "PosZ", "QuaX", "QuaY", "QuaZ", "QuaW" });
                return chanNames;
            }
        }

        protected override void Start()
        {
            base.Start();
            GazeUpdater = GetComponent<EyeGazeBehaviour>();
        }

        // protected override void ExtendHash(Hash128 hash)
        // {
        //     hash.Append(transformFormat.ToString());
        // }

        protected override bool BuildSample()
        {
            if(GazeUpdater.Position != null && GazeUpdater.Rotation != null)
            {
                // sample Position
                sample[0] = GazeUpdater.Position.x;
                sample[1] = GazeUpdater.Position.y;
                sample[2] = GazeUpdater.Position.z;
                // sample Rotation
                sample[3] = GazeUpdater.Rotation.x;
                sample[4] = GazeUpdater.Rotation.y;
                sample[5] = GazeUpdater.Rotation.z;
                sample[6] = GazeUpdater.Rotation.w;
                Debug.Log($"EyeGazeOutlet: Sampled");
                return true;
            }
            return false;
        }
    }
}