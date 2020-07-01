using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultadosManager : MonoBehaviour
{
    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI precisionText;
    public TextMeshProUGUI tiempoApuntadoText;
    public TextMeshProUGUI velReaccionText;
    public TextMeshProUGUI trackingText;
    public TextMeshProUGUI notaText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameSessionManager.Instance != null)
        {
            nombreText.text = GameSessionManager.Instance.GetPlayerName();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        GameSessionManager.Instance.EndGame();
    }
}
