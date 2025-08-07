using System;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
{
    public Text hourText;

    public Text minuteText;

    private int hour = 0;
    private int minute = 0;

    private float timer = 0f;

    // Método chamado a cada frame
    void Update()
    {
        timer += Time.deltaTime;

        // Quando o timer alcançar ou ultrapassar 1 segundo, incrementa o relógio em 1 minuto
        if (timer >= 1f)
        {
            timer = 0f;

            minute++;

            // Se os minutos alcançarem 60, reinicia os minutos e incrementa a hora
            if (minute >= 60)
            {
                minute = 0;
                hour++;
            }

            // Se as horas alcançarem 24, reinicia a contagem de horas para 0
            if (hour >= 24)
            {
                hour = 0;
            }

            hourText.text = hour.ToString("00");

            minuteText.text = minute.ToString("00");

            Debug.Log(hour + ":" + minute);
        }
    }
}
