using UnityEngine;

public class plrMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;

    public float moveSpeed = 3f;

    private Animator animator;

    // Armazena o estado atual da animação para evitar transições desnecessárias
    private string currentState = "";

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Captura o input do teclado para movimentação (setas/WASD)
        float horizontal = Input.GetAxisRaw("Horizontal"); // Movimento lateral (A/D)
        float vertical = Input.GetAxisRaw("Vertical");     // Movimento frontal/trás (W/S)

        // Combina os inputs em um vetor de direção e normaliza (evita velocidades maiores na diagonal)
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Se houver alguma entrada do jogador (movimento)
        if (inputDirection != Vector3.zero)
        {
            // Obtém a referência da câmera principal para movimentação baseada na direção da câmera
            Transform cameraTransform = Camera.main.transform;

            // Pega os vetores frente e direita da câmera
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Zera o eixo Y para evitar movimentação inclinada (apenas no plano XZ)
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            // Calcula a direção final do movimento com base na direção da câmera
            Vector3 moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;

            // Gira o personagem suavemente na direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        HandleAnimation(inputDirection);
    }

    // Gerencia a lógica de troca de animações com base na entrada do jogador
    void HandleAnimation(Vector3 inputDirection)
    {
        // Se o jogador está se movendo e NÃO está segurando Shift
        if (inputDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 3f; // Velocidade de caminhada
            ChangeAnimationState("walk"); // Troca para animação de caminhada
        }
        // Se o jogador está se movendo E segurando Shift
        else if (inputDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
            ChangeAnimationState("run");
        }
        // Se não há movimento
        else
        {
            moveSpeed = 3f;
            ChangeAnimationState("idle");
        }
    }

    // Método responsável por mudar o estado da animação sem repetir o mesmo
    void ChangeAnimationState(string newState)
    {
        // Se o novo estado é o mesmo que o atual, não faz nada (evita reiniciar animações desnecessariamente)
        if (currentState == newState) return;

        // Faz a transição suave para a nova animação
        animator.CrossFade(newState, 0.03f);

        // Atualiza o estado atual
        currentState = newState;
    }
}
