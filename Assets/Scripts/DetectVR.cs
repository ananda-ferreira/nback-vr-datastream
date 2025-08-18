using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour // change MonoBehavior to ScriptableObject, runs at start of the game not scene
{
    public bool startInVR = false;
    public GameObject xrOrigin;
    public GameObject DesktopCharacter;
    void Start()
    {
        if (startInVR) {
            var xrSettings = XRGeneralSettings.Instance;
            if (xrSettings == null ){
                Debug.Log("XRGeneralSettings is null");
                return;
            }
            var xrManager = xrSettings.Manager;
            if (xrManager == null ){
                Debug.Log("Manager is null");
                return;
            }
            var xrLoader = xrManager.activeLoader;
            if (xrLoader == null ){
                Debug.Log("Loader is null; No headset is connected");
                ActivateDesktopCharacter();
                return;
            }

            Debug.Log("Headset is connected");
            xrOrigin.SetActive(true);
            DesktopCharacter.SetActive(false);

        } else {
            ActivateDesktopCharacter();
        }
    }

    void ActivateDesktopCharacter(){
        xrOrigin.SetActive(false);
        DesktopCharacter.SetActive(true);
    }

}
