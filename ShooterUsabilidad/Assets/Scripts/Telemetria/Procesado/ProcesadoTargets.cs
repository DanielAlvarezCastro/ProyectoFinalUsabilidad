using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcesadoTargets : IProcessing
{
    public override void Process(string sessionName)
    {
        List<TrackerEvent> events = Tracker.instance.GetTestEvents(sessionName);

        //System.TimeSpan[] targetTimes = new System.TimeSpan[NUM_TARGETS];

        System.DateTime starTime = System.DateTime.MinValue;
        System.TimeSpan totalTime = System.TimeSpan.Zero;

        float totalPunt = 0;
        int numDisparos = 0;
        int numAciertos = 0;



        foreach (TrackerEvent e in events)
        {
            if (e.eventType == EventType.SESSION_START)
                starTime = e.time;
            else if (e.eventType == EventType.SESSION_END)
                totalTime = (e.time - starTime);
            //Si es un evento de tipo AIM, lo procesamos
            if (e.eventType == EventType.TARGET)
            {
                TargetEvent targetEvent = (TargetEvent)e;

                if(targetEvent.targetEventType == TargetEventType.DESTROYED)
                {
                    totalPunt += targetEvent.punt;
                    numAciertos++;
                }
            }
            else if (e.eventType == EventType.SHOOT)
            {
                numDisparos++;
            }
        }

        Debug.Log("Total time: " + totalTime.ToString(@"mm\:ss\.fff") + " / " + totalTime.ToString(@"mm\:ss\.fff"));
        Debug.Log("Total punt: " + totalPunt);
        Debug.Log("Hit% " + ((float)numAciertos / numDisparos) * 100 + "%");
    }
}
