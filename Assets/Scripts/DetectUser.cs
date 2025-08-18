using UnityEngine;
using VIVE.OpenXR;
public class DetectUser : MonoBehaviour
{
    public bool IsPresent {get; private set;} = false;
    public bool WasPresent {get; private set;} = false;

    void Update()
    {
        IsPresent = XR_EXT_user_presence.Interop.IsUserPresent();

        if (IsPresent && !WasPresent) 
        {
            Debug.Log("DetectUser: User is now present"); 
            WasPresent = true;
        } 
        else if (!IsPresent && WasPresent) {
            WasPresent = false;
            Debug.Log("User is not present anymore"); 
        }
       
    }
}
