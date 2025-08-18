using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

namespace EyeTracking {
    public class UpdateLeftPupil : MonoBehaviour
    {
        // private void Start()
        // {
        //    if (!VIVE.OpenXR.EyeTracker.IsInitialized())
        //    {
            //    VIVE.OpenXR.EyeTracker.Initialize();
        //    }
        // }

        // Update is called once per frame
        void Update()
        {
            XR_HTC_eye_tracker.Interop.GetEyePupilData(out XrSingleEyePupilDataHTC[] out_pupils);
            XrSingleEyePupilDataHTC leftPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
            
            if(leftPupil.isDiameterValid) {
                float leftPupilDiameter = leftPupil.pupilDiameter;
                Debug.Log("diameter:" + leftPupilDiameter);
            }
            if(leftPupil.isPositionValid) {
                XrVector2f leftPupilPosition = leftPupil.pupilPosition;
                Debug.Log("position:" + leftPupilPosition);
            }
        }
    }
}
