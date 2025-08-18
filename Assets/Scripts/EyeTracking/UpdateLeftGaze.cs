using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

namespace Assets.Scripts.EyeTracking {
    public class UpdateLeftGaze : MonoBehaviour
    {
        void Update()
        {
            XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
            XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
            
            if(leftGaze.isValid)
            {
                Vector3 position = leftGaze.gazePose.position.ToUnityVector();
                Quaternion rotation = leftGaze.gazePose.orientation.ToUnityQuaternion();
                transform.SetPositionAndRotation(position, rotation);
            }
        }
    }
}

