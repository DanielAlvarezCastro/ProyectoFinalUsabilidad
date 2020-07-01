using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStart : MonoBehaviour
{
    public string testName = "default";
    public IProcessing proccesing;

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
        Tracker.getInstance().StartTest(testName,eventOptions.values);
    }


    public void Proccess()
    {
        proccesing.Process(testName);
    }

}
