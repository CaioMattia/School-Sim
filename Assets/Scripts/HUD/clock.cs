using System;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
{
    public Text hourText;
    public Text minuteText;

    private int hour = 0;
    private int minute = 0;
    private float timer = 0f; // Acumula o tempo em segundos

    void Update()
    {
        timer += Time.deltaTime;  // Soma o tempo passado desde o último frame

        if (timer >= 1f) // A cada 1 segundo real, incrementa 1 minuto no relógio do jogo
        {
            timer = 0f;
            minute++;

            if (minute >= 60)
            {
                minute = 0;
                hour++;
            }
            if (hour >= 24)
            {
                hour = 0;
            }

            hourText.text = hour.ToString("00");   // Para mostrar sempre 2 dígitos (ex: 01, 09)
            minuteText.text = minute.ToString("00");
            Debug.Log(hour + ":" + minute);
        }
    }
}