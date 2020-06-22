using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimEventType { AIM_IN, AIM_OUT}
public class AimEvent : TrackerEvent
{

    public AimEventType aimEventType;
    public string aimEventTypeString;
    public AimEvent(AimEventType aimEventType) : base(EventType.AIM)
    {
        this.aimEventType = aimEventType;
        aimEventTypeString = aimEventType.ToString();
    }
}
