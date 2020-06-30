using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStart : MonoBehaviour
{
    public string testName = "default";

    private const int COUNT = (int)EventType.LENGTH;
    [SerializeField]

    [System.Serializable]
    public class ArrayInitializer
    {
        public EventOption[] values;
        public ArrayInitializer(string[] defaults) { values = new EventOption[COUNT]; for (int i = 0; i < COUNT; i++) values[i] = new EventOption((EventType)i); }
    }

    public ArrayInitializer eventOptions = new ArrayInitializer(new string[] { "a", "b", "c" });

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
            List<TrackerEvent> events = Tracker.instance.GetTestEvents(testName);

            System.DateTime starTime = System.DateTime.MinValue;
            System.TimeSpan totalTime = System.TimeSpan.Zero;

            System.DateTime lastAimEvent = System.DateTime.MinValue;
            System.TimeSpan totalTimeIn = System.TimeSpan.Zero;


            bool isIn = false; //Para evitar dobles

            foreach(TrackerEvent e in events)
            {
                if (e.eventType == EventType.SESSION_START)
                    starTime = e.time;
                else if (e.eventType == EventType.SESSION_END)
                    totalTime = (e.time - starTime);
                //Si es un evento de tipo AIM, lo procesamos
                if(e.eventType == EventType.AIM)
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
            Debug.Log("Percentage: " + (totalTimeIn.TotalMilliseconds / totalTime.TotalMilliseconds)*100  + "%");
        }

    }


}
