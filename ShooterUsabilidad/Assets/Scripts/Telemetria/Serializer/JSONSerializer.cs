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
}
