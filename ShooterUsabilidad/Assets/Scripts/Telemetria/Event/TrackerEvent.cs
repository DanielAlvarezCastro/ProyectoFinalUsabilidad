using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;


[System.Serializable]
public class EventOption
{
    [HideInInspector]
    public string typeString;
    [HideInInspector]
    public EventType type;
    public bool activated;

    public EventOption(EventType type)
    {
        this.type = type;
        typeString = type.ToString();
    }
}


[System.Serializable]
public enum EventType { SESSION_START, SESSION_END,SHOOT,AIM,SPAWN,CLICK, LENGTH}

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
