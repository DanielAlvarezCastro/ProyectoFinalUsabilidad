using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum PERSISTENCE_TYPE { FILE,SERVER};
public enum SERIALIZER_TYPE { JSON,XML};

public class Tracker : MonoBehaviour
{

    static public Tracker instance = null;


    [Header("Persistence Settings")]
    public PERSISTENCE_TYPE persistenceType = PERSISTENCE_TYPE.FILE;
    public string folderName = "Tracking";
    IPersistence persistenceObject;

    [Header("Serializer Settings")]
    public SERIALIZER_TYPE serializerType = SERIALIZER_TYPE.JSON;
    ISerializer serializerObject;
    public Formatting jsonFormatting;


    //Timestamp configurable
    [Header("Timestamp Settings")]
    public bool date;
    public bool hour;
    public bool minutes;
    public bool seconds;
    public bool milliseconds;
    //Inicializaciones etc

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
            Init();
        }
    }

    public void Init()
    {
        print("Initializing Tracker...");
        //Perparamos el serializador
        SetSerializer();
        serializerObject.Init();

        //Y preparamos la persistencia
        SetPersistence();
        persistenceObject.Init();


    }

    public void End()
    {
        serializerObject.End();
        persistenceObject.End();
    }

    //Crea la serializacion en funcion de las opciones del editor
    private void SetSerializer()
    {
        switch (serializerType)
        {
            case SERIALIZER_TYPE.JSON:
                serializerObject = new JSONSerializer();
                break;
            case SERIALIZER_TYPE.XML:
                break;
            default:
                break;

        }
    }

    //Crea la persistencia en funcion a las opciones del editor
    private void SetPersistence()
    {
        switch (persistenceType)
        {
            case PERSISTENCE_TYPE.FILE:
                persistenceObject = new FilePersistence(serializerObject, folderName);
                break;
            case PERSISTENCE_TYPE.SERVER:
                break;
            default:
                break;
        }
    }


    public void TrackEvent(TrackerEvent e)
    {
        persistenceObject.Send(e);
    }

    public TrackerEvent GenerateTrackerEvent(EventType type)
    {
        return new TrackerEvent(type);
    }


    public void StartTest(string testName)
    {
        persistenceObject.NewSession(testName);
    }

    public void EndTest()
    {
        persistenceObject.Flush();
    }

    //Devuelve una cadena que contiene la timestamp dependiendo del tipo elegido en el editor
    public string GetTimeStamp()
    {
        System.DateTime now;
        now = System.DateTime.Now;
        string timeStamp;
        string format;

        return now.ToString("hh:mm:ss:fff");
    }

    static public Tracker getInstance()
    {
        //Inicializamos la instancia
        if (instance == null)
        {
 
            instance = FindObjectOfType<Tracker>();
            // fallback, might not be necessary.
            if (instance == null)
            {
                Debug.Log("Creating new Tracker instance");
                instance = new GameObject(typeof(Tracker).Name).AddComponent<Tracker>();
                instance.Init();
            }
            DontDestroyOnLoad(instance.gameObject);

        }
        return instance;
    }
}
