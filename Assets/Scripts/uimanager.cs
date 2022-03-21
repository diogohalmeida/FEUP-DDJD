using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uimanager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject ingameUI;

    public PlayerMovement1 pm;

    private bool gameOverScreenActive;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreenActive = false;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (pm.gameOver && !gameOverScreenActive){
            gameOverScreenActive = true;
        }

        if (pm.gameOver){
            ingameUI.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
