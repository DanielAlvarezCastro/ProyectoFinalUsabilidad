using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public enum EventType { SESSION_START, SESSION_END,SHOOT}
public class TrackerEvent
{
    public EventType eventType;
    public string time;

    public TrackerEvent(string time, EventType eventType)
    {
        this.time = time;
        this.eventType = eventType;
    }
}
