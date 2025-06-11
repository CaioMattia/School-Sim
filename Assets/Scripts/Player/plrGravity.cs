using UnityEngine;

public class GravidadeManual : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speedY = 0f;
    public LayerMask floorLayer;
    public float rayHeight = 0.1f;

    void Update()
    {
        // Verifica se está no chão
        bool onFloor = Physics.Raycast(transform.position, Vector3.down, rayHeight, floorLayer);

        if (onFloor)
        {
            // Se está no chão, zera a velocidade vertical
            speedY = 0f;
        }
        else
        {
            // Aplica gravidade
            speedY += gravity * Time.deltaTime;
            transform.position += new Vector3(0, speedY * Time.deltaTime, 0);
        }
    }
}