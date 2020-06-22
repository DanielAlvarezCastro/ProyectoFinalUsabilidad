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
        this.basePath = Application.persistentDataPath + "/" + basePath;
    }

    //Inicializacion
    public override void Init( ) {

        Debug.Log("Initializing File Persistance...");

        events = new List<string>();

        //Creamos la carpeta de la sesion
        try
        {
            Debug.Log("Creating Directory " + basePath);
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }

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
            File.WriteAllText(sessionPath, string.Join("\n",events.ToArray()));
            events.Clear();
        }
        catch (Exception e)
        {
            Debug.LogError("Error guardando " + sessionPath + " : " + e.Message);
        }
    }

    //Crea un nuevo archivo para la sesion correspondiente
    public override void NewSession(string sessionName)
    {
        sessionPath = basePath + "/" + sessionName;
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
            Debug.Log("Problema con el fichero json " + sessionPath + " : " + e.Message);
        }

    }

}
