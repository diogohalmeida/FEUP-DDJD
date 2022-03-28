using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement1 : MonoBehaviour
{

    Rigidbody2D body;
    Animator animator;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite deadSprite;

    public enum KeyState { Space, Off }
    KeyState pressed = KeyState.Off;

    public bool gameOver;
    
    bool canMove;

    int coffeeSecs = 0;
    int notesSecs = 0;
    bool coffeeOn = false;
    bool notesOn = false;
    
    bool alternate1 = false;
    bool alternate2 = false;

    AudioSource[] sounds;

    [SerializeField]
    private CameraMovement cm;

    [SerializeField]
    private ObstacleSpawner os;

    [SerializeField]
    private MapController controller;

    [SerializeField]
    private ProjectileManager pm;

    [SerializeField]
    private GameObject coinSequence;

    [SerializeField]
    private GameObject teachersHolder;

    [SerializeField]
    private GameObject projectileHolder;

    [SerializeField]
    private TeacherSpawner teacherSpawner;

    [SerializeField]
    private IngameUIManager ingameUI;

    [SerializeField]
    public GameObject scoreBoard;

    int chosenSong;

    // Start is called before the first frame update
    void Start()
    {
        
        canMove = true;
        chosenSong = Random.Range(0,2);
        gameOver = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        sounds = GetComponents<AudioSource>(); // 0 -> ECTS Pickup; 1 -> Coffee PowerUp; 2 -> Notes PowerUp; 3 -> Running; 4 -> Jetpack Up; 5 -> Jetpack Down; 6 -> Obstacle Hit; 7 -> Teacher Hit; 8 -> GameMusic1; 9 -> GameMusic2;
        if(chosenSong == 0){
            if(!sounds[8].isPlaying) sounds[8].Play();
        }
        else if(chosenSong == 1){
            if(!sounds[9].isPlaying) sounds[9].Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove){
            if(!sounds[8].isPlaying && !sounds[9].isPlaying){
                chosenSong = 1-chosenSong;
                sounds[8+chosenSong].Play();
            }
            if (Input.GetKey(KeyCode.Space)){
                pressed = KeyState.Space;
            } else {
                pressed = KeyState.Off;
            }

            if (Input.GetKeyDown(KeyCode.P)){
                pm.Shoot(this.gameObject.transform.position[0], this.gameObject.transform.position[1]);
            }

            if (transform.position.y > -3.0){
                if(sounds[3].isPlaying) sounds[3].Stop();
                animator.SetBool("is_flying", true);
            }
            else {
                if(sounds[5].isPlaying) sounds[5].Stop();
                animator.SetBool("is_flying", false);
                if(!sounds[3].isPlaying) sounds[3].Play();
            }

            reduceUIOpacity();
        }
    }

    void FixedUpdate()
    {
        if(coffeeOn){
            coffeeSecs +=1;
        }
        if(notesOn){
            notesSecs +=1;
        }
        
        if(coffeeSecs >= 750 && coffeeSecs % 25 == 0){
            ingameUI.SetCoffeePowerup(alternate1);
            alternate1 = !alternate1;
        }
        if(coffeeSecs >= 1000){
            pm.SetPowerUp(0, false);
            ingameUI.SetCoffeePowerup(false);
            coffeeOn = false;
            coffeeSecs = 0;
        }
        if(notesSecs >= 750 && notesSecs % 25 == 0){
            ingameUI.SetNotesPowerup(alternate2);
            alternate2 = !alternate2;
        }
        if(notesSecs >= 1000){
            pm.SetPowerUp(1, false);
            ingameUI.SetNotesPowerup(false);
            notesOn = false;
            notesSecs = 0;
        }
        if (pressed == KeyState.Space){
            if (body.drag != 0){
                body.drag = 0;
            }
            if(sounds[5].isPlaying) sounds[5].Stop();
            if(!sounds[4].isPlaying && !gameOver) sounds[4].Play();
            body.AddForce(new Vector2(0, 50), ForceMode2D.Force);
        } else if (body.velocity[1] > 0) {
            if(!sounds[5].isPlaying && !gameOver) sounds[5].Play();
            body.drag = 4;
        } else {
            if(sounds[4].isPlaying) sounds[4].Stop();
            //if(sounds[5].isPlaying) sounds[5].Stop();
            if(!sounds[3].isPlaying && !gameOver) sounds[3].Play();
            body.drag = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "ects(Clone)"){
            // Hits ects
            if(!sounds[0].isPlaying) sounds[0].Play();
            ingameUI.UpdateScore(1);
            Destroy(collider.gameObject);
        } else if (collider.gameObject.name =="Obstacle(Clone)" || collider.gameObject.name =="apr(Clone)" || collider.gameObject.name =="as(Clone)" || collider.gameObject.name =="cbm(Clone)"){
            // Hits FEUP banner or teacher
            if(collider.gameObject.name == "Obstacle(Clone)"){
                if(!sounds[6].isPlaying) sounds[6].Play();
            }
            else{
                if(!sounds[7].isPlaying) sounds[7].Play();
            }
            controller.spawnActive = false;
            pressed = KeyState.Off;
            /*os.body.velocity = new Vector2(0, 0);
            os.body.angularVelocity = 0.0f;
            os.body.gravityScale = 1.0f;*/
            if(sounds[3].isPlaying) sounds[3].Stop();
            if(sounds[4].isPlaying) sounds[4].Stop();
            if(sounds[5].isPlaying) sounds[5].Stop();
            gameOver = true;
            canMove = false;
            cm.speed = 0f;
            stopCoins();
            stopProjectiles();
            stopTeachers();
            stopObstacles();
            teacherSpawner.StopSpawner();
            animator.enabled = false;
            spriteRenderer.sprite = deadSprite;
        } else if(collider.gameObject.name == "coffee(Clone)"){
            sounds[1].Play();
            pm.SetPowerUp(0,true);
            coffeeOn = true;
            Destroy(collider.gameObject);
            ingameUI.SetCoffeePowerup(true);
        } else if(collider.gameObject.name == "notes(Clone)"){
            sounds[2].Play();
            pm.SetPowerUp(1,true);
            notesOn = true;
            Destroy(collider.gameObject);
            ingameUI.SetNotesPowerup(true);
        }
    }

    void stopCoins()
    {
        foreach (Transform coin in coinSequence.transform){
            coin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void stopProjectiles()
    {
        foreach (Transform projectile in projectileHolder.transform){
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void stopTeachers()
    {
        foreach (Transform teacher in teachersHolder.transform){
            teacher.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void reduceUIOpacity(){
        GameObject board = scoreBoard.transform.GetChild(0).gameObject;
        GameObject ectsscore = scoreBoard.transform.GetChild(1).gameObject;
        GameObject metercounter = scoreBoard.transform.GetChild(2).gameObject;
        GameObject ectsicon = scoreBoard.transform.GetChild(3).gameObject;
        
        Image img1 = board.GetComponent<Image>();
        Text img2 = ectsscore.GetComponent<Text>();
        Text img3 = metercounter.GetComponent<Text>();
        Image img4 = ectsicon.GetComponent<Image>();

        Color newColor1 = img1.color;
        Color newColor2 = img2.color;
        Color newColor3 = img3.color;
        Color newColor4 = img4.color;
        
        if (transform.position.y > 2.0){
            newColor1.a = 0.5f;
            img1.color = newColor1;
            newColor2.a = 0.5f;
            img2.color = newColor2;            
            newColor3.a = 0.5f;
            img3.color = newColor3;
            newColor4.a = 0.5f;
            img4.color = newColor4;
        }
        else{
            newColor1.a = 1.0f;
            img1.color = newColor1;
            newColor2.a = 1.0f;
            img2.color = newColor2;            
            newColor3.a = 1.0f;
            img3.color = newColor3;            
            newColor4.a = 1.0f;
            img4.color = newColor4;
        }
    }
    void stopObstacles()
    {
        foreach (Transform obstacle in os.transform){
            obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
