using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;

using UnityEngine.SceneManagement;
using EyeTracking;

namespace Assets.Scripts.DataStream
{
    public class PupilOutlet : AFloatOutlet
    {
        public enum Eye { Left, Right };
        [SerializeField] private Eye eye = Eye.Left;
        private UpdatePupil PupilUpdater;
        public void Reset()
        {
            StreamName = $"Unity.{SceneManager.GetActiveScene().name}.{eye}PD";
            StreamType = "Unity.Transform";
            moment = MomentForSampling.EndOfFrame;
        }

        public override List<string> ChannelNames
        {
            get
            {
                List<string> chanNames = new List<string>();
                chanNames.AddRange(new string[] { "PosX", "PosY", "Diameter" });
                return chanNames;
            }
        }

        protected override void Start()
        {
            base.Start();
            PupilUpdater = GetComponent<UpdatePupil>();
        }

        // protected override void ExtendHash(Hash128 hash)
        // {
        //     hash.Append(transformFormat.ToString());
        // }

        protected override bool BuildSample()
        {
            // get data from UpdatePupil script instead
            sample[0] = PupilUpdater.Position.x;
            sample[1] = PupilUpdater.Position.y;
            sample[2] = PupilUpdater.Diameter;
            return true;
        }
    }
}