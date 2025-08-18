using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using VIVE.OpenXR;

// SteamVR Input
using Valve.VR;

namespace Assets.Scripts.Util
{
    public class EyeGazeBehaviour : MonoBehaviour
    {
        [Header("Action Parameter")]
        [SerializeField] private InputActionReference PrimaryPlaceholder;
        [SerializeField] private InputActionAsset ActionAsset;

        // public UnityEvent OnTriggerDown;

        public Quaternion Rotation {get; private set;}
        public Vector3 Position {get; private set;}

        void Update()
        {
            Position = PrimaryPlaceholder.action.ReadValue<XrPosef>().position.ToUnityVector();
            Rotation = PrimaryPlaceholder.action.ReadValue<XrPosef>().orientation.ToUnityQuaternion();
            
            Debug.Log($"EyeGazeBehaviour: PositionXZ – {Position.x}, {Position.z}");
            // CheckForButtonDown();
            // CheckForNSelection();
            // CheckForSceneChange();
        }

        // private void CheckForButtonDown()
        // {
        //     if (_TriggerPressed || Input.GetKeyDown(KeyCode.Return))
        //         OnTriggerDown?.Invoke();
        // }

        // private void CheckForNSelection(){
        //     if (_PrimaryPressed || Input.GetKeyDown(KeyCode.A))
        //         OnPrimaryDown?.Invoke();
        //     // if (_SecondaryPressed || Input.GetKeyDown(KeyCode.B))
        //     //    OnSecondaryDown?.Invoke();
        // }

        // private void CheckForVSelection(){
        //     if (_PrimaryPressed || Input.GetKeyDown(KeyCode.A))
        //         OnPrimaryDown?.Invoke();
        //     // if (_SecondaryPressed || Input.GetKeyDown(KeyCode.B))
        //     //    OnSecondaryDown?.Invoke();
        // }

        // private void CheckForSceneChange(){
        //     if (_SecondaryPressed || Input.GetKeyDown(KeyCode.B))
        //         OnSecondaryDown?.Invoke();
        // }

    }
}
