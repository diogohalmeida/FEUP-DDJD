using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uimanager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject ingameUI;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private PlayerMovement1 player;

    public PlayerMovement1 pm;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);

        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
