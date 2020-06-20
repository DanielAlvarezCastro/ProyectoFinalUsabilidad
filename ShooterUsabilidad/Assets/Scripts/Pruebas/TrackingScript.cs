using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    Ray ray;
    Camera cameraObj;
    bool dentro = false;

    bool started = false;
    public float startTime = 3;
    float actualStartTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        actualStartTime = startTime;
        cameraObj = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraObj == null)
            cameraObj = GetComponentInChildren<Camera>();
        else track();
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
                print("mando evento dentro");
            }
            else if (dentro && !hit.collider.CompareTag("Enemy"))
            {
                dentro = false;
                print("mando evento fuera");
                actualStartTime = startTime;
            }
            //Para empezar el test
            if(!started && dentro && actualStartTime > 0)
            {
                actualStartTime -= Time.deltaTime;
            }
            else if(!started && actualStartTime <= 0)
            {
                started = true;
                GameObject.FindObjectOfType<TrackingTest>().startTest();
            }
        }
        else if(dentro)
        {
            dentro = false;
            print("mando evento fuera");
            actualStartTime = startTime;
        }
    }
}
