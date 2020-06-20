using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilePersistence : IPersistence
{

    string basePath;
    string sessionPath;
    ISerializer serializer;

    List<string> events;
    public FilePersistence(ISerializer serializer, string basePath)
    {
        this.serializer = serializer;
        this.basePath = Application.persistentDataPath + basePath;
    }

    //Inicializacion
    public override void Init( ) {
        
        events = new List<string>();
    }
    public override void End() { }
    public override void Send(TrackerEvent e) {
        string s = serializer.Serialize(e);
        Debug.Log("Registrado evento: " + s);
        events.Add(s);
    }

    //Vuelca los eventos almacenados en el archivo de la sesion
    public override void Flush() {
        try
        {
            File.WriteAllText(Application.persistentDataPath + sessionPath, string.Join("\n",events.ToArray()));
            events.Clear();
        }
        catch (Exception e)
        {
            Debug.LogError("Error guardando " + sessionPath);
        }
    }

    //Crea un nuevo archivo para la sesion correspondiente
    public override void NewSession(string sessionName)
    {
        sessionPath = basePath + "_" + sessionName;
        CreateFile(sessionName);
    }


    //Crea un nuevo archivo con el nombre especificado
    public void CreateFile(string sessionName)
    {
        try
        {
            //Si existe lo cargamos
            if (File.Exists(sessionPath))
            {
                Debug.Log("Existe el archivo " + sessionPath);
            }
            //Y si no lo creamos
            else
            {
                FileStream fs = File.Create(sessionPath);
                fs.Close();
            }
 
        }
        catch (Exception e)
        {
            Debug.Log("Problema con el fichero json " + sessionPath);
        }

    }

}
