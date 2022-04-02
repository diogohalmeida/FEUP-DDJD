using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject ingameUI;

    [SerializeField]
    private GameObject leaderboardScreen;

    [SerializeField]
    private GameObject settingsScreen;

    [SerializeField]
    private LeaderboardController leaderboardController;

    [SerializeField]
    private PlayerMovement1 player;

    [SerializeField]
    private GameObject arrow;

    private bool female;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        ingameUI.SetActive(true);
        player.StartGame();
        Time.timeScale = 1;
    }

    public void ShowLeaderboard()
    {
        mainMenu.SetActive(false);
        leaderboardScreen.SetActive(true);
        leaderboardController.ShowLeaderboardEntries();
    }

    public void SwitchSprite()
    {

        female = !female;
        player.SetSprite(female);
        int x = -860;
        if(female) x = -680;
        Vector3 position = new Vector3(x, -130, 0);
        arrow.GetComponent<RectTransform>().anchoredPosition = position;
    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        settingsScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
        mainMenu.SetActive(true);
    }
}
