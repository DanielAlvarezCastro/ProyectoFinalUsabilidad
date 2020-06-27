using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    Ray ray;
    Camera cameraObj;
    bool dentro = false;

    bool started = false;
    bool ended = false;
    public float startTime = 3;
    float actualStartTime = 0;
    TrackingTest tT;
    // Start is called before the first frame update
    void Start()
    {
        tT = GameObject.FindObjectOfType<TrackingTest>();
        actualStartTime = startTime;
        cameraObj = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraObj == null)
            cameraObj = GetComponentInChildren<Camera>();
        else if(!ended)track();
    }
    public void endTest()
    {
        Tracker.instance.TrackEvent(new AimEvent(AimEventType.AIM_OUT));
        Tracker.instance.TrackEvent(Tracker.instance.GenerateTrackerEvent(EventType.SESSION_END));
        Tracker.getInstance().EndTest();
        ended = true;

    }
    void track()
    {
        ray = new Ray(cameraObj.transform.position, cameraObj.transform.forward);
        RaycastHit hit;

        //Si chocamos contra algo NO TRIGGER
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (!dentro && hit.collider.CompareTag("Enemy"))
            {
                dentro = true;
                // print("mando evento dentro");
                if(started)
                    Tracker.instance.TrackEvent(new AimEvent(AimEventType.AIM_IN));
                tT.enterRay();
            }
            else if (dentro && !hit.collider.CompareTag("Enemy"))
            {
                dentro = false;
                //print("mando evento fuera");
                if (started)
                    Tracker.instance.TrackEvent(new AimEvent(AimEventType.AIM_OUT));
                tT.exitRay();
                actualStartTime = startTime;
                tT.startTimeTest(startTime, actualStartTime);
            }
            //Para empezar el test
            if(!started && dentro && actualStartTime > 0)
            {
                actualStartTime -= Time.deltaTime;
                tT.startTimeTest(startTime, actualStartTime);
            }
            else if(!started && actualStartTime <= 0)
            {
                started = true;
                tT.startTest();
                Tracker.getInstance().TrackEvent(Tracker.getInstance().GenerateTrackerEvent(EventType.SESSION_START));
                Tracker.instance.TrackEvent(new AimEvent(AimEventType.AIM_IN));
            }
        }
        else if(dentro)
        {
            if (started)
                Tracker.instance.TrackEvent(new AimEvent(AimEventType.AIM_OUT));
            dentro = false;
            tT.exitRay();
            actualStartTime = startTime;
            tT.startTimeTest(startTime, actualStartTime);
        }
    }
}
