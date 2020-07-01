using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : Target
{
    //Multiplicador a la velocidad del objetivo
    float speed;

    //Origen de la oscilación
    Vector3 origin;
    //Destino de la oscilación
    Vector3 destination;

    bool direction = false;

    //Distancia mínima y máxima en ambos en el eje X e Y a la que puede estar el límite del movimiento
    public float minDistanceX;
    public float maxDistanceX;
    public float minDistanceY;
    public float maxDistanceY;

    Vector3 minLimit;
    Vector3 maxLimit;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        origin = gameObject.transform.position;
        destination = gameObject.transform.position;
        minLimit = GameObject.Find("MinPos").transform.position;
        maxLimit = GameObject.Find("MaxPos").transform.position;

        if(destination.x - maxDistanceX < minLimit.x) destination.x += Random.Range(minDistanceX, maxDistanceX);
        else if (destination.x + maxDistanceX > maxLimit.x) destination.x -= Random.Range(minDistanceX, maxDistanceX);
        else if (Random.Range(0f, 1f) < 0.5f) destination.x -= Random.Range(minDistanceX, maxDistanceX);
        else destination.x += Random.Range(minDistanceX, maxDistanceX);

        if (destination.y - maxDistanceY < minLimit.y) destination.y += Random.Range(minDistanceY, maxDistanceY);
        else if (destination.y + maxDistanceY > maxLimit.y) destination.y -= Random.Range(minDistanceY, maxDistanceY);
        else if (Random.Range(0f, 1f) < 0.5f) destination.y -= Random.Range(minDistanceY, maxDistanceY);
        else destination.y += Random.Range(minDistanceY, maxDistanceY);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!direction)
        {
            gameObject.transform.Translate((destination - origin).normalized * speed* Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, destination) < 0.1f) direction = true;

        }
        else {
            gameObject.transform.Translate((origin - destination).normalized * speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, origin) < 0.1f) direction = false;
        }
    }

    public void setSpeed(float _speed)
    {
        speed = _speed;
        info.speed = speed;
    }
}
