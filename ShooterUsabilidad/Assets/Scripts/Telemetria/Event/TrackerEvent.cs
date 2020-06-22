using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public enum EventType { SESSION_START, SESSION_END,SHOOT,AIM}
public class TrackerEvent
{
    public EventType eventType;
    public string eventTypeString;
    public string time;

    public TrackerEvent(EventType eventType)
    {
        time = Tracker.instance.GetTimeStamp();
        eventTypeString = eventType.ToString();
        this.eventType = eventType;
    }
}
