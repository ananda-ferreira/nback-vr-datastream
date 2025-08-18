using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// SteamVR Input
using Valve.VR;

namespace Assets.Scripts.Util
{
    public class KeyboardInputBehaviour : MonoBehaviour
    {
        [Header("Action Parameter")]
        // SteamVR Input
        public SteamVR_Input_Sources HandType;
        public SteamVR_Action_Boolean Begin;
        public SteamVR_Action_Boolean NextScene;
        public SteamVR_Action_Boolean NextN;
        public SteamVR_Action_Boolean NextV;

        public UnityEvent OnTriggerDown;
        public UnityEvent OnSecondaryDown;
        public UnityEvent OnPrimaryDown;
        private bool _TriggerPressed;
        private bool _SecondaryPressed;
        private bool _PrimaryPressed;

        void Update()
        {
            // SteamVR input
            _TriggerPressed = Begin.GetStateDown(HandType);
            _PrimaryPressed = NextN.GetStateDown(HandType);
            _SecondaryPressed = NextScene.GetStateDown(HandType);
            CheckForButtonDown();
            CheckForNSelection();
            CheckForSceneChange();
        }

        private void CheckForButtonDown()
        {
            if (_TriggerPressed || Input.GetKeyDown(KeyCode.Return))
                OnTriggerDown?.Invoke();
        }

        private void CheckForNSelection(){
            if (_PrimaryPressed || Input.GetKeyDown(KeyCode.A))
                OnPrimaryDown?.Invoke();
            // if (_SecondaryPressed || Input.GetKeyDown(KeyCode.B))
            //    OnSecondaryDown?.Invoke();
        }

        private void CheckForVSelection(){
            if (_PrimaryPressed || Input.GetKeyDown(KeyCode.A))
                OnPrimaryDown?.Invoke();
            // if (_SecondaryPressed || Input.GetKeyDown(KeyCode.B))
            //    OnSecondaryDown?.Invoke();
        }

        private void CheckForSceneChange(){
            if (_SecondaryPressed || Input.GetKeyDown(KeyCode.B))
                OnSecondaryDown?.Invoke();
        }

    }
}
