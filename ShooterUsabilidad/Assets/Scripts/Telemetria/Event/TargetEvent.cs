using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetEventType {DESTROYED,DESPAWN };

public class TargetEvent : TrackerEvent
{
    public TargetEventType targetEventType;
    public string targetEventTypeString;
    public float punt = 0;

    public TargetEvent(TargetEventType targetEventType, float punt) : base(EventType.TARGET)
    {
        this.targetEventType = targetEventType;
        targetEventTypeString = targetEventType.ToString();
        this.punt = punt;
    }
}
