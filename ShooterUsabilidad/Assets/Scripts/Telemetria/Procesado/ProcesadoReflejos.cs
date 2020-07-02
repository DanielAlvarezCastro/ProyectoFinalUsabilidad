using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcesadoReflejos : IProcessing
{
    int NUM_TARGETS = 5;
    [HideInInspector]
    public float notaFinal = 0;

    public float maxPunt = 0.25f;
    public float midPunt = 0.35f;
    public float minPunt = 0.5f;

    private void Start()
    {
        NUM_TARGETS = GameObject.FindObjectOfType<ReflexTest>().maxNumObj;
    }
    public override void Process(string sessionName)
    {
        List<TrackerEvent> events = Tracker.instance.GetTestEvents(sessionName);

        //System.TimeSpan[] targetTimes = new System.TimeSpan[NUM_TARGETS];

        System.DateTime starTime = System.DateTime.MinValue;
        System.TimeSpan totalTime = System.TimeSpan.Zero;

        System.DateTime lastTargetEvent = System.DateTime.MinValue;
        System.TimeSpan totalReactionTime = System.TimeSpan.Zero;
        int failCount = 0;


        bool targetActive = false; //Para evitar dobles

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

                    targetActive = true;
            }
            else if (e.eventType == EventType.CLICK)
            {
                if (targetActive)
                {
                    totalReactionTime += (e.time - lastTargetEvent);
                    targetActive = false;
                }
                else
                    failCount++;

            }      
        }

        Debug.Log("Total time: " + totalTime.ToString(@"mm\:ss\.fff") + " / " + totalTime.ToString(@"mm\:ss\.fff"));
        //Debug.Log("Average aim time: " + (totalReactionTime.TotalMilliseconds / NUM_TARGETS));

        //Analisis
        notaFinal = (float)(totalReactionTime.TotalMilliseconds / Mathf.Max(NUM_TARGETS-failCount, 1));
        print(notaFinal);
        if (notaFinal <= maxPunt * 1000)
            notaFinal = 100;
        else if (notaFinal < midPunt * 1000)
            notaFinal = 100 - 50 * (notaFinal - maxPunt*1000) / ((midPunt - maxPunt)*1000);
        else if (notaFinal < minPunt * 1000)
            notaFinal = 50 - 50 * (notaFinal - midPunt*1000) / ((minPunt - midPunt)*1000);
        else notaFinal = 0;
        print(notaFinal);
        GameObject.FindObjectOfType<GUIManager>().SetNota(notaFinal);
        if (GameObject.FindObjectOfType<GameSessionManager>() != null && GameSessionManager.Instance.GetCompleteTest())
            GameObject.FindObjectOfType<AnalysisManager>().addStadistic(stat.tracking, notaFinal);
    }


}
