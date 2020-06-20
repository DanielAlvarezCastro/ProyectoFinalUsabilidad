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
    public void Init()
    {
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
                persistenceObject = new FilePersistence(serializerObject, "");
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Devuelve una cadena que contiene la timestamp dependiendo del tipo elegido en el editor
    public string GetTimeStamp()
    {
        System.DateTime now;
        now = System.DateTime.Now;
        string timeStamp;
        string format;

        return now.ToString("h:mm:ss:fff");
    }

    static public Tracker getInstance()
    {
        //Inicializamos la instancia
        if(instance == null)
        {
            instance = new Tracker();
            instance.Init();
        }
        return instance;
    }
}
