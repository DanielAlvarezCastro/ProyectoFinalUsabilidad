using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JSONSerializer : ISerializer
{
    public override void Init() { }
    public override void End() { }
    public override string Serialize(TrackerEvent e) 
    {
        return JsonConvert.SerializeObject(e,Tracker.getInstance().jsonFormatting).ToString();
    }

    override public TrackerEvent Deserialize(string s) {
        TrackerEvent e = JsonConvert.DeserializeObject<TrackerEvent>(s);

        switch (e.eventType)
        {
            case EventType.AIM:
                return JsonConvert.DeserializeObject<AimEvent>(s);
            default:
                return e;
        }
    }
}
