using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcesadoTargets : IProcessing
{
    public float midScore = 250;
    public float maxScore = 500;

    public override void Process(string sessionName)
    {
        List<TrackerEvent> events = Tracker.instance.GetTestEvents(sessionName);

        //System.TimeSpan[] targetTimes = new System.TimeSpan[NUM_TARGETS];

        System.DateTime starTime = System.DateTime.MinValue;
        System.TimeSpan totalTime = System.TimeSpan.Zero;

        float totalPunt = 0;
        int numDisparos = 0;
        int numAciertos = 0;
        int numFallos = 0;



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

                if (targetEvent.targetEventType == TargetEventType.DESTROYED)
                {
                    totalPunt += targetEvent.punt;
                    numAciertos++;
                }
                else
                    numFallos++;
            }
            else if (e.eventType == EventType.SHOOT)
            {
                numDisparos++;
            }
        }

        Debug.Log("Total time: " + totalTime.ToString(@"mm\:ss\.fff") + " / " + totalTime.ToString(@"mm\:ss\.fff"));
        Debug.Log("Total punt: " + totalPunt);
        float completion = ((float)(numAciertos) / (numAciertos + numFallos));
        float hit = ((float)(numAciertos) / (numDisparos));
        Debug.Log("Completion% " + completion * 100 + "%");
        Debug.Log("Hit% " + hit * 100 + "%");
        Debug.Log("Precision score: " + hit * completion * 100 + "%");
        /*
         * Diparas 1. Aciertas 1. Spawnean 10. -> 10%
         * Disparas 10. Aciertas 5. Spawnean 10 ->50%
         * DIsparas 10. Aciertas 1. Spawnean 10 -> 10% 
         * 
         * Acertados / Disparados. 
         * 
         * 
         */
        float aimTime = (totalPunt / maxScore);
        aimTime = Mathf.Clamp(aimTime, 0, 1) * 100;
        FindObjectOfType<AnalysisManager>().addStadistic(stat.precision, hit * completion * 100);
        FindObjectOfType<AnalysisManager>().addStadistic(stat.aimTime, totalPunt);
    }
}
