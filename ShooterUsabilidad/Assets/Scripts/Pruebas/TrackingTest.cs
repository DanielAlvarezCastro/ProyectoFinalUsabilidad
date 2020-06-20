using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTest : MonoBehaviour
{
    public float speed = 1;
    public float maxHoriz = 5;
    public float maxVert = 3;

    public float minRandTime = 2;
    public float maxRandTime = 5;
    float actualRandTime = 0;


    bool started = false;
    Vector3 dirVec;
    Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void startTest()
    {
        initPos = transform.position;
        changeDir();
    }
    private void Update()
    {
        move();
        limits();
    }
    void invokeChange()
    {
        actualRandTime = Random.Range(minRandTime, maxRandTime);
        Invoke("changeDir", actualRandTime);
    }
    void changeDir()
    {
        dirVec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 0f), 0).normalized;
        invokeChange();
    }

    void move()
    {
        transform.Translate(dirVec*Time.deltaTime*speed);
    }
    void limits()
    {
        if (transform.position.x > initPos.x+maxHoriz)
        {
            dirVec.x = -Mathf.Abs(dirVec.x);
            CancelInvoke();
            invokeChange();
        }
        if (transform.position.x < initPos.x-maxHoriz)
        {
            dirVec.x = Mathf.Abs(dirVec.x);
            CancelInvoke();
            invokeChange();
        }
        if (transform.position.y > initPos.y+maxVert)
        {
            dirVec.y = -Mathf.Abs(dirVec.y);
            CancelInvoke();
            invokeChange();
        }
        if (transform.position.y < initPos.y-maxVert)
        {
            dirVec.y = Mathf.Abs(dirVec.y);
            CancelInvoke();
            invokeChange();
        }
    }
}

