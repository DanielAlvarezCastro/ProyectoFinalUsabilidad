using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcesadoTracking : IProcessing
{
    [HideInInspector]
    public float notaFinal = 0;

    public override void Process(string sessionName)
    {
        List<TrackerEvent> events = Tracker.instance.GetTestEvents(sessionName);

        System.DateTime starTime = System.DateTime.MinValue;
        System.TimeSpan totalTime = System.TimeSpan.Zero;

        System.DateTime lastAimEvent = System.DateTime.MinValue;
        System.TimeSpan totalTimeIn = System.TimeSpan.Zero;


        bool isIn = false; //Para evitar dobles

        foreach (TrackerEvent e in events)
        {
            if (e.eventType == EventType.SESSION_START)
                starTime = e.time;
            else if (e.eventType == EventType.SESSION_END)
                totalTime = (e.time - starTime);
            //Si es un evento de tipo AIM, lo procesamos
            if (e.eventType == EventType.AIM)
            {
                AimEvent aimEvent = ((AimEvent)e);

                //Si el jugador apunta la bola, apuntamos el tiempo
                if (!isIn && aimEvent.aimEventType == AimEventType.AIM_IN)
                {
                    lastAimEvent = e.time;
                    isIn = true;
                }
                else if (isIn && aimEvent.aimEventType == AimEventType.AIM_OUT)
                {
                    System.DateTime timeOut = e.time;

                    totalTimeIn += (timeOut - lastAimEvent);
                    isIn = false;
                }

                Debug.Log("Evento aim: " + ((AimEvent)e).aimEventTypeString);
            }
        }

        Debug.Log("Time in: " + totalTimeIn.ToString(@"mm\:ss\.fff") + " / " + totalTime.ToString(@"mm\:ss\.fff"));
        Debug.Log("Percentage: " + (totalTimeIn.TotalMilliseconds / totalTime.TotalMilliseconds) * 100 + "%");

        //Analisis
        notaFinal = (float)((totalTimeIn.TotalMilliseconds / totalTime.TotalMilliseconds) * 100);

        if(GameObject.FindObjectOfType<GameSessionManager>() != null && GameSessionManager.Instance.GetCompleteTest())
            GameObject.FindObjectOfType<AnalysisManager>().addStadistic(stat.tracking, notaFinal);
    }
}
