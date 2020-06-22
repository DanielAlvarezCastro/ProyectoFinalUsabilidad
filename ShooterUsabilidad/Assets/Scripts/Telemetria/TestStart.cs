using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStart : MonoBehaviour
{
    public string testName = "default";
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Starting test " + testName);
        Tracker.getInstance().StartTest(testName);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Tracker.instance.TrackEvent(Tracker.instance.GenerateTrackerEvent(EventType.SESSION_END));
            Tracker.getInstance().EndTest();
        }

    }

}
