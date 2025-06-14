using UnityEngine;
using UnityEngine.SceneManagement;  // Para carregar cenas

public class Menu : MonoBehaviour
{
    // Função para iniciar o jogo
    public void StartGame()
    {
        // Aqui você pode carregar a cena do jogo. Exemplo:
        SceneManager.LoadScene("School"); // "GameScene" deve ser o nome da cena do seu jogo.
    }

    // Função para sair do jogo
    public void QuitGame()
    {
        // Fecha o jogo
        Application.Quit();
    }
}