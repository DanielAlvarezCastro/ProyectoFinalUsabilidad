using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPersistence
{

    virtual public void Init() { }
    virtual public void End() { }
    virtual public void Send(TrackerEvent e) { }
    virtual public void Flush() { }

    virtual public void NewSession(string sessionName) { }

    virtual public void LoadSession(string sessionName) { }
    virtual public List<TrackerEvent> GetTrackerEvents() { return null; }
}
