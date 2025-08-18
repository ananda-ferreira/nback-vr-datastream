using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

namespace EyeTracking {
    public class UpdatePupil : MonoBehaviour
    { 
        public float Diameter {get; private set;}
        public XrVector2f Position {get; private set;}
        public enum EyeSelection
        {
            Left = XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC,
            Right = XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC
        };

        [SerializeField] private EyeSelection _eyeToTrack = EyeSelection.Left;
        
        void Update()
        {
            XR_HTC_eye_tracker.Interop.GetEyePupilData(out XrSingleEyePupilDataHTC[] out_pupils);
            XrSingleEyePupilDataHTC pupil = out_pupils[(int)_eyeToTrack];
            
            if(pupil.isDiameterValid) {
                float pupilDiameter = pupil.pupilDiameter;
                Debug.Log("diameter:" + pupilDiameter);
            }
            if(pupil.isPositionValid) {
                XrVector2f pupilPosition = pupil.pupilPosition;
                Debug.Log("position x:" + pupilPosition.x);
                Debug.Log("position y:" + pupilPosition.y);
            }
        }
    }
}
