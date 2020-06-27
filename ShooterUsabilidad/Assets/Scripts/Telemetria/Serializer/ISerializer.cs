using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISerializer
{
    virtual public void Init() { }
    virtual public void End() { }
    virtual public string Serialize(TrackerEvent e) { return null; }

    virtual public TrackerEvent Deserialize(string s) { return null; }
}
