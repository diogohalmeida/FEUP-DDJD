using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uimanager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject ingameUI;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private PlayerMovement1 player1;

    [SerializeField] private PlayerMovement1 player2;

    private PlayerMovement1 player;

    // Start is called before the first frame update
    void Start()
    {
        if(player1.gameObject.activeSelf){
            player = player1;
        }
        else{
            if(player2.gameObject.activeSelf){
                player = player2;
            }
        }
        gameOverScreen.SetActive(false);

        pauseScreen.SetActive(false);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause(){
        player.Pause();
        pauseScreen.SetActive(true);
    }

    public void Resume(){
        pauseScreen.SetActive(false);
        player.Resume();
    }

    public void GameOver(){
        ingameUI.SetActive(false);
        gameOverScreen.SetActive(true);
    }

}
