using UnityEngine;

public class plrMovement : MonoBehaviour
{
    public float rotationSpeed = 10f; // Velocidade com que o personagem gira para a direção do movimento
    public float moveSpeed = 3f; // Velocidade inicial de movimento

    private Animator animator; // Referência para o componente Animator

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        animator = GetComponent<Animator>();  // Pega o Animator no mesmo GameObject
        Debug.Log("Animator inicializado.");
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Debug.Log($"Input recebido - Horizontal: {horizontal}, Vertical: {vertical}");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection != Vector3.zero)
        {
            Debug.Log($"Direção de entrada detectada: {inputDirection}");

            Transform cameraTransform = Camera.main.transform;

            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            Vector3 moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;
            Debug.Log($"Direção de movimento calculada: {moveDirection}");

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Debug.Log($"Rotação aplicada: {transform.rotation.eulerAngles}");

            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            Debug.Log($"Nova posição do personagem: {transform.position}");
        }

        if ((inputDirection != Vector3.zero) && (!Input.GetKey((KeyCode.LeftShift))))
        {
            moveSpeed = 3f;
            animator.Play("walk");
            Debug.Log("Estado: Andando");
        }
        else if ((inputDirection != Vector3.zero) && (Input.GetKey((KeyCode.LeftShift))))
        {
            moveSpeed = 5f;
            animator.Play("run");
            Debug.Log("Estado: Correndo");
        }
        else
        {
            moveSpeed = 3f;
            animator.Play("idle");
            Debug.Log("Estado: Parado");
        }
    }
}