using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalysisManager : MonoBehaviour
{
    float [] precision = new float[5];
    float[] aimTime = new float[5];
    float[] reactionTime = new float[5];
    float[] tracking = new float[5];


    //TRACKING
    [Header("Tracking")]
    [Range(0, 100)]
    public float trackingTrackPond = 0;

    //DISPARO A OBJETOS MÓVILES
    [Header("Disparo a objetos móviles")]
    [Range(0, 100)]
    public float precisionMovPond = 0;
    [Range(0, 100)]
    public float aimTimeMovPond = 0;
    [Range(0, 100)]
    public float reactionTimeMovPond = 0;
    [Range(0, 100)]
    public float trackingMovPond = 0;

    //REFLEJOS
    [Header("Reflejos")]
    [Range(0, 100)]
    public float reactionTimeReflexPond = 0;

    //PRECISION
    [Header("Precision")]
    [Range(0, 100)]
    public float precisionPrecPond = 0;
    [Range(0, 100)]
    public float aimTimePrecPond = 0;
    [Range(0, 100)]
    public float reactionTimePrecPond = 0;

    //VELOCIDAD
    [Header("Velocidad")]
    [Range(0, 100)]
    public float precisionVelPond = 0;
    [Range(0, 100)]
    public float aimTimeVelPond = 0;
    [Range(0, 100)]
    public float reactionTimeVelPond = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
