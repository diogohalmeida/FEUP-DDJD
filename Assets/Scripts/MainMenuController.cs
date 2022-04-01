using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject ingameUI;

    [SerializeField]
    private PlayerMovement1 player;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        ingameUI.SetActive(true);
        player.StartGame();
        Time.timeScale = 1;
    }

    public void LoadLevel()
    {
        Debug.Log("Load");
        SceneManager.LoadScene("Game");
    }
}
