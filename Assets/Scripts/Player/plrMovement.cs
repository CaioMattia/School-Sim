using UnityEngine;

// Classe responsável pelo controle de movimento e animação do jogador
public class plrMovement : MonoBehaviour
{
    // Velocidade de rotação do personagem (usada para suavizar a rotação)
    public float rotationSpeed = 10f;

    // Velocidade padrão de movimento (pode ser alterada dinamicamente)
    public float moveSpeed = 3f;

    // Referência ao componente Animator para controlar as animações
    private Animator animator;

    // Armazena o estado atual da animação para evitar transições desnecessárias
    private string currentState = "";

    // Função chamada ao iniciar o jogo (antes do primeiro frame)
    void Start()
    {
        // Trava o cursor no centro da tela para evitar que ele saia durante o controle do jogador
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Oculta o cursor

        // Obtém o componente Animator que está no mesmo GameObject
        animator = GetComponent<Animator>();
    }

    // Função chamada a cada frame
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

            // Move o personagem na direção desejada com a velocidade atual
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        // Controla qual animação deve ser executada com base no movimento e na tecla Shift
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
            moveSpeed = 5f; // Aumenta a velocidade para corrida
            ChangeAnimationState("run"); // Troca para animação de corrida
        }
        // Se não há movimento
        else
        {
            moveSpeed = 3f; // Mantém a velocidade padrão (pode ser usada em futuras ações paradas)
            ChangeAnimationState("idle"); // Troca para animação de idle (parado)
        }
    }

    // Método responsável por mudar o estado da animação sem repetir o mesmo
    void ChangeAnimationState(string newState)
    {
        // Se o novo estado é o mesmo que o atual, não faz nada (evita reiniciar animações desnecessariamente)
        if (currentState == newState) return;

        // Faz a transição suave para a nova animação
        animator.CrossFade(newState, 0.02f);

        // Atualiza o estado atual
        currentState = newState;
    }
}
