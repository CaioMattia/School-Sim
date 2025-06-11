using UnityEngine;

public class plrCamera : MonoBehaviour
{
    // Referência ao objeto que a câmera deve seguir (geralmente o jogador)
    public Transform target;

    // Posição relativa da câmera em relação ao alvo (altura e distância)
    public Vector3 offset = new Vector3(0, 2, -5);

    // Velocidade de rotação da câmera com o movimento do mouse
    public float rotationSpeed = 5f;

    // Valor acumulado do ângulo de rotação horizontal (eixo Y)
    private float yaw = 0f;

    void Start()
    {
        // Trava o cursor no centro da tela para uma melhor experiência de câmera em terceira pessoa
        Cursor.lockState = CursorLockMode.Locked;

        // Oculta o cursor
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Incrementa o ângulo horizontal com base na movimentação do mouse no eixo X
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;

        // Cria uma rotação apenas no eixo Y com base no ângulo calculado
        Quaternion rotation = Quaternion.Euler(-12, yaw, 0);

        // Calcula a posição desejada da câmera aplicando a rotação ao offset
        Vector3 desiredPosition = target.position + rotation * offset;

        // Move a câmera para a posição calculada
        transform.position = desiredPosition;

        // Faz a câmera olhar para o alvo, ajustado para olhar um pouco acima (altura do offset)
        transform.LookAt(target.position + Vector3.up * offset.y);
    }
}