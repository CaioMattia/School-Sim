using System;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
{
    // Referência para o componente Text que exibirá as horas na UI
    public Text hourText;

    // Referência para o componente Text que exibirá os minutos na UI
    public Text minuteText;

    // Variáveis internas para armazenar a hora e minuto atuais do relógio
    private int hour = 0;
    private int minute = 0;

    // Timer que acumula o tempo passado em segundos desde a última atualização dos minutos
    private float timer = 0f;

    // Método chamado a cada frame
    void Update()
    {
        // Incrementa o timer com o tempo real passado desde o último frame
        timer += Time.deltaTime;

        // Quando o timer alcançar ou ultrapassar 1 segundo, incrementa o relógio em 1 minuto
        if (timer >= 1f)
        {
            // Reseta o timer para começar a contar o próximo segundo
            timer = 0f;

            // Incrementa o minuto em 1
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

            // Atualiza o texto da UI com a hora formatada em 2 dígitos (ex: 01, 09)
            hourText.text = hour.ToString("00");

            // Atualiza o texto da UI com os minutos formatados em 2 dígitos
            minuteText.text = minute.ToString("00");

            // Loga no console a hora atual para debug
            Debug.Log(hour + ":" + minute);
        }
    }
}