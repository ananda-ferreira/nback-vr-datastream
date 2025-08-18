using System.Collections.Generic;
using LSL4Unity.Utils;
using UnityEngine.SceneManagement;
public class ImmersionOutlet : AStringOutlet
{
    private DetectUser _userDetector;
    public override List<string> ChannelNames
    {
        get
        {
            List<string> chanNames = new List<string>{ "Immersion" };
            return chanNames;
        }
    }

    public void Reset()
    {
        StreamName = $"Unity.{SceneManager.GetActiveScene().name}.Immersion";
        StreamType = "Marker";
        moment = MomentForSampling.EndOfFrame;
        IrregularRate = true;
    }

    protected override void Start()
    {
        base.Start();
        _userDetector = GetComponent<DetectUser>();
    }

    protected override bool BuildSample()
    {
        if (_userDetector == null) return false;
        if (_userDetector.IsPresent && !_userDetector.WasPresent)
        {
            sample[0] = "Entered Env";
            return true;
        } 
        else if (!_userDetector.IsPresent && _userDetector.WasPresent)
        {
            sample[0] = "Exited Env";
            return true;
        }
        return false;
    }


}
