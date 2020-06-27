using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public enum EventType { SESSION_START, SESSION_END,SHOOT,AIM}
public class TrackerEvent
{
    public EventType eventType;
    public string eventTypeString;
    public string formattedTime;
    public System.DateTime time;

    public TrackerEvent(EventType eventType)
    {
        time = Tracker.instance.GetTimeStamp();
        formattedTime = Tracker.instance.GetFormattedTimeStamp();
        eventTypeString = eventType.ToString();
        this.eventType = eventType;
    }
}
