using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum stat {precision, aimTime, reactionTime, tracking};
public class AnalysisManager : MonoBehaviour
{
    List<float> precisionValues;
    List<float> aimTimeValues;
    List<float> reactionTimeValues;
    List<float> trackingValues;

    public float mediaPrecision = 0;
    public float mediaAimTime = 0;
    public float mediaReactionTime = 0;
    public float mediaTracking = 0;

    float notaFinal = 0;

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


    //NOTA FINAL
    [Header("Nota final")]
    [Range(0, 100)]
    public float precisionTotalPond = 0;
    [Range(0, 100)]
    public float aimTimeTotalPond = 0;
    [Range(0, 100)]
    public float reactionTimeTotalPond = 0;
    [Range(0, 100)]
    public float trackingTotalPond = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<AnalysisManager>() && GameObject.FindObjectOfType<AnalysisManager>().gameObject != gameObject) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);

        precisionValues = new List<float>();
        aimTimeValues = new List<float>();
        reactionTimeValues = new List<float>();
        trackingValues = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addStadistic(stat category, float value)
    {
        switch(category)
        {
            case stat.precision:
                precisionValues.Add(value);
                break;
            case stat.aimTime:
                aimTimeValues.Add(value);
                break;
            case stat.reactionTime:
                reactionTimeValues.Add(value);
                break;
            case stat.tracking:
                trackingValues.Add(value);
                break;
        }
    }

    public void ponderateStatsAndFinalScore()
    {
        //Ponderar cada estadística
        ponderateStat(stat.precision, precisionValues.ToArray());
        ponderateStat(stat.aimTime, aimTimeValues.ToArray());
        ponderateStat(stat.reactionTime, reactionTimeValues.ToArray());
        ponderateStat(stat.tracking, trackingValues.ToArray());

        //Nota Final
        generateFinalScore();
    }

    public void testStats()
    {
        List<float> values1 = new List<float>();
        List<float> values2 = new List<float>();
        List<float> values3 = new List<float>();
        List<float> values4 = new List<float>();
        for(int i = 0; i < 3; i++)
        {
            values1.Add(50);
        }
        for (int i = 0; i < 3; i++)
        {
            values2.Add(50);
        }
        for (int i = 0; i < 4; i++)
        {
            values3.Add(50);
        }
        for (int i = 0; i < 2; i++)
        {
            values4.Add(50);
        }
        //Ponderar cada estadística
        ponderateStat(stat.precision, values1.ToArray());
        ponderateStat(stat.aimTime, values2.ToArray());
        ponderateStat(stat.reactionTime, values3.ToArray());
        ponderateStat(stat.tracking, values4.ToArray());

        //Nota Final
        generateFinalScore();
    }

    void ponderateStat(stat category, float[] stats)
    {
        float media = 0;
        if (category == stat.precision)
        {
            stats[0] *= precisionMovPond/100;
            stats[1] *= precisionPrecPond/100;
            stats[2] *= precisionVelPond/100;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            mediaPrecision = media;
        }
        else if(category == stat.aimTime)
        {
            stats[0] *= aimTimeMovPond/100;
            stats[1] *= aimTimePrecPond/100;
            stats[2] *= aimTimeVelPond/100;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            mediaAimTime = media;
        }
        else if (category == stat.tracking)
        {
            stats[0] *= trackingTrackPond/100;
            stats[1] *= trackingMovPond/100;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            mediaTracking = media;
        }
        else if (category == stat.reactionTime)
        {
            stats[0] *= reactionTimeMovPond/100;
            stats[1] *= reactionTimeReflexPond/100;
            stats[2] *= reactionTimePrecPond/100;
            stats[3] *= reactionTimeVelPond/100;

            for (int i = 0; i < stats.Length; i++)
            {
                media += stats[i];
            }
            mediaReactionTime = media;
        }

    }

    public void generateFinalScore()
    {
        notaFinal = 0;
        notaFinal += mediaPrecision*precisionTotalPond/100;
        notaFinal += mediaAimTime * aimTimeTotalPond/100;
        notaFinal += mediaReactionTime * reactionTimeTotalPond/100;
        notaFinal += mediaTracking * trackingTotalPond/100;


        //Ya la tenemos
        print(notaFinal);
    }
}
