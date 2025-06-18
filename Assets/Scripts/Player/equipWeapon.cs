using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipWeapon : MonoBehaviour
{
    // Objeto da arma que será equipada
    public GameObject weaponObj;

    // Transform da arma no mundo (posição atual da arma)
    public Transform weaponTransform;

    // Transform do jogador para calcular distância até a arma
    public Transform player;

    // Transform da mão do jogador, onde a arma será "presa" ao ser equipada
    public Transform playerHand;

    // Método chamado a cada frame
    void Update()
    {
        // Verifica se a distância entre a arma e o jogador é menor que 1.5 unidades
        if (Vector3.Distance(weaponTransform.position, player.position) < 1.5f)
        {
            // Se a tecla 'F' estiver pressionada
            if (Input.GetKey(KeyCode.F))
            {
                // Define o pai da arma como sendo a mão do jogador (para que a arma siga o movimento da mão)
                weaponObj.transform.parent = playerHand.transform;

                // Posiciona a arma na posição exata da mão do jogador
                weaponObj.transform.position = playerHand.transform.position;

                // Zera a rotação local da arma para evitar rotações indesejadas
                weaponObj.transform.localRotation = Quaternion.identity;

                // Ajusta a rotação local da arma para um ângulo específico (-10° no X, 0° no Y, 80° no Z)
                weaponObj.transform.localRotation = Quaternion.Euler(-10f, 0f, 80f);
            }
        }
    }
}