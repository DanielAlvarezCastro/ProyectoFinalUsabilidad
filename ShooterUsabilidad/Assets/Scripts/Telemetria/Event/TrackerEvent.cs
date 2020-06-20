using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EventType { SESSION_START, SESSION_END, TEST_START, TEST_END}

[System.Serializable]
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
