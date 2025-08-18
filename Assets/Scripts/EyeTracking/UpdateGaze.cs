// using System.Numerics;
using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

namespace EyeTracking 
{
    public class UpdateGaze : MonoBehaviour
    {
        public Vector3 Position {get; private set;}
        public Quaternion Rotation {get; private set;}

        public enum EyeSelection
        {
            Left = XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC,
            Right = XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC
        };
        [SerializeField] private EyeSelection _eyeToTrack = EyeSelection.Left;
        void Update()
        {
            if (XR_EXT_user_presence.Interop.IsUserPresent()) { // not working; checks if user is wearing headset
                XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
                XrSingleEyeGazeDataHTC gaze = out_gazes[(int)_eyeToTrack];
                
                if(gaze.isValid)
                {
                    Position = gaze.gazePose.position.ToUnityVector();
                    Rotation = gaze.gazePose.orientation.ToUnityQuaternion();
                    transform.SetPositionAndRotation(Position, Rotation);
                }
            }
        }
    }
}

