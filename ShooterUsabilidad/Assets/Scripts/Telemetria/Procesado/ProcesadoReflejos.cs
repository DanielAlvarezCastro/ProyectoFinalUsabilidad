using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcesadoReflejos : IProcessing
{
    public int NUM_TARGETS = 5;
    public override void Process(string sessionName)
    {
        List<TrackerEvent> events = Tracker.instance.GetTestEvents(sessionName);

        //System.TimeSpan[] targetTimes = new System.TimeSpan[NUM_TARGETS];

        System.DateTime starTime = System.DateTime.MinValue;
        System.TimeSpan totalTime = System.TimeSpan.Zero;

        System.DateTime lastTargetEvent = System.DateTime.MinValue;
        System.TimeSpan totalReactionTime = System.TimeSpan.Zero;
        int failCount = 0;


        bool isIn = false; //Para evitar dobles

        foreach (TrackerEvent e in events)
        {
            if (e.eventType == EventType.SESSION_START)
                starTime = e.time;
            else if (e.eventType == EventType.SESSION_END)
                totalTime = (e.time - starTime);
            //Si es un evento de tipo AIM, lo procesamos
            if (e.eventType == EventType.SPAWN)
            {

                    lastTargetEvent = e.time;

                    isIn = false;
            }
            else if (e.eventType == EventType.CLICK)
            {
                if (lastTargetEvent != System.DateTime.MinValue)
                {
                    totalReactionTime += (e.time - lastTargetEvent);
                    lastTargetEvent = System.DateTime.MinValue;
                }
                else
                    failCount++;

            }      
        }

        Debug.Log("Total time: " + totalTime.ToString(@"mm\:ss\.fff") + " / " + totalTime.ToString(@"mm\:ss\.fff"));
        Debug.Log("Average aim time: " + (totalReactionTime.TotalMilliseconds / NUM_TARGETS)); 
    }
}
