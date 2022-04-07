using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement1 : MonoBehaviour
{

    Rigidbody2D body;
    Animator animator;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite main_char_m;

    [SerializeField]
    private Sprite main_char_f;


    [SerializeField]
    private Sprite deadSprite_m;

    [SerializeField]
    private Sprite deadSprite_f;

    private Sprite deadSprite;

    [SerializeField]
    private RuntimeAnimatorController animator_m;
    
    [SerializeField]
    private RuntimeAnimatorController animator_f;

    public enum KeyState { Space, Off }
    KeyState pressed = KeyState.Off;

    public bool gameOver;

    private bool female = false;

    bool paused = false;
    
    bool canMove;

    int cameraSecs = 0;
    int coffeeSecs = 0;
    int notesSecs = 0;
    bool coffeeOn = false;
    bool notesOn = false;
    
    bool alternate1 = false;
    bool alternate2 = false;

    bool gameRunning = false;

    bool shouldUpdateVelocities = true;

    float levelVelocityFactor = 1.1f;

    int numberOfVelocityIncreases = 0;

    public int maxVelocityIncreases;

    AudioSource[] sounds;

    [SerializeField]
    private CameraMovement cm;

    [SerializeField]
    private LeaderboardController leaderboard;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Text gameOverScore;

    [SerializeField]
    private ObstacleSpawner os;

    [SerializeField]
    private ScoreController scoreController;

    [SerializeField]
    private MapController controller;

    [SerializeField]
    private EctsGenerator ectsGenerator;

    [SerializeField]
    private PowerUpSpawner powerUpHolder;

    [SerializeField]
    private FlyingCoffeeSpawner flyingCoffeeHolder;

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
    private FlyingCoffeeSpawner flyingCoffeeSpawner;

    [SerializeField]
    private IngameUIManager ingameUI;

    [SerializeField]
    private uimanager uIManager;

    [SerializeField]
    public GameObject scoreBoard;

    int chosenSong;

    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1f);
        }

        if (!PlayerPrefs.HasKey("effectsVolume")){
            PlayerPrefs.SetFloat("effectsVolume", 1f);
        }
        
        if(female){
            deadSprite = deadSprite_f;
        }
        else{
            deadSprite = deadSprite_m;
        }
        chosenSong = Random.Range(0,2);
        gameOver = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        sounds = GetComponents<AudioSource>(); // 0 -> ECTS Pickup; 1 -> Coffee PowerUp; 2 -> Notes PowerUp; 3 -> Running; 4 -> Jetpack Up; 5 -> Jetpack Down; 6 -> Obstacle Hit; 7 -> Teacher Hit; 8 -> GameMusic1; 9 -> GameMusic2; 10 -> Coffee Hit; 11 -> MainMenu Music; 12 -> GameOver Music;
        if(!sounds[11].isPlaying) sounds[11].Play();
        flyingCoffeeSpawner.StopSpawner();
        teacherSpawner.StopSpawner();
        powerUpHolder.StopSpawner();

        controller.spawnActive = false;
        pressed = KeyState.Off;
        if(sounds[3].isPlaying) sounds[3].Stop();
        if(sounds[4].isPlaying) sounds[4].Stop();
        if(sounds[5].isPlaying) sounds[5].Stop();
        canMove = false;
        shouldUpdateVelocities = false;
        cm.Stop();
        stopCoins();
        stopProjectiles();
        stopTeachers();
        stopObstacles();
        stopPowerups();
        stopFlyingCoffees();
        stopScore();
        animator.enabled = false;
    }

    public void SetSprite(bool isFemale)
    {
        female = isFemale;
        if(female){
            spriteRenderer.sprite = main_char_f;
            deadSprite = deadSprite_f;
            GetComponent<Animator>().runtimeAnimatorController = animator_f;
        }
        else{
            spriteRenderer.sprite = main_char_m;
            deadSprite = deadSprite_m;
            GetComponent<Animator>().runtimeAnimatorController = animator_m;
        }
    }

    public void StartGame()
    {
        canMove = true;
        shouldUpdateVelocities = true;
        controller.spawnActive = true;
        cm.Resume();
        resumeCoins();
        resumeProjectiles();
        resumeObstacles();
        resumePowerups();
        resumeFlyingCoffees();
        resumeScore();
        controller.GenerateSection(Random.Range(0, 5));
        gameRunning = true;
        flyingCoffeeSpawner.ResumeSpawner();
        teacherSpawner.resumeSpawner();
        powerUpHolder.ResumeSpawner();
        animator.enabled = true;
        if(sounds[11].isPlaying) sounds[11].Stop();
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
        if (canMove && gameRunning){
            if(!sounds[8].isPlaying && !sounds[9].isPlaying){
                chosenSong = 1-chosenSong;
                sounds[8+chosenSong].Play();
            }
            if (Input.GetKey(inputManager.GetKey("JetpackUp")) /*Input.GetKey(KeyCode.Space)*/){
                pressed = KeyState.Space;
            } else {
                pressed = KeyState.Off;
            }

            if (Input.GetKeyDown(/*KeyCode.Z*/inputManager.GetKey("Fire"))){
                pm.Shoot(this.gameObject.transform.position[0], this.gameObject.transform.position[1]);
            } else if (Input.GetKeyDown(/*KeyCode.P*/inputManager.GetKey("Pause")) && !paused){
                uIManager.Pause();
            }

            if (transform.position.y > -3.0){
                if(sounds[3].isPlaying) sounds[3].Stop();
                animator.SetBool("is_flying", true);
            }
            else {
                if(sounds[5].isPlaying) sounds[5].Stop();
                animator.SetBool("is_flying", false);
                if(!sounds[3].isPlaying && !gameOver && gameRunning) sounds[3].Play();
            }

            reduceUIOpacity();
        }
    }

    void FixedUpdate()
    {

        if ((numberOfVelocityIncreases < maxVelocityIncreases) && shouldUpdateVelocities){
            cameraSecs += 1;
            if (cameraSecs >= 500){
                updateVelocities();
                cameraSecs = 0;
            }
        }

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
            if(!sounds[4].isPlaying && !gameOver && gameRunning) sounds[4].Play();
            body.AddForce(new Vector2(0, 50), ForceMode2D.Force);
        } else if (body.velocity[1] > 0) {
            if(!sounds[5].isPlaying && !gameOver && gameRunning) sounds[5].Play();
            body.drag = 4;
        } else {
            if(sounds[4].isPlaying) sounds[4].Stop();
            if(!sounds[3].isPlaying && !gameOver && gameRunning) sounds[3].Play();
            body.drag = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "ects(Clone)"){
            // Hits ects
            if(!sounds[0].isPlaying) sounds[0].Play();
            ingameUI.UpdateScore(1);
            Destroy(collider.gameObject);
        } else if (collider.gameObject.name == "flying_coffee(Clone)" || collider.gameObject.name =="Obstacle(Clone)" || collider.gameObject.name =="apr(Clone)" || collider.gameObject.name =="as(Clone)" || collider.gameObject.name =="cbm(Clone)"){
            // Hits FEUP banner or teacher
            if(collider.gameObject.name == "Obstacle(Clone)"){
                if(!sounds[6].isPlaying) sounds[6].Play();
            }
            else{
                if(collider.gameObject.name == "flying_coffee(Clone)"){
                    if(!sounds[10].isPlaying) sounds[10].Play();
                }
                else{
                    if(!sounds[7].isPlaying) sounds[7].Play();
                }
            }
            controller.spawnActive = false;
            pressed = KeyState.Off;
            if(sounds[3].isPlaying) sounds[3].Stop();
            if(sounds[4].isPlaying) sounds[4].Stop();
            if(sounds[5].isPlaying) sounds[5].Stop();
            if(sounds[8].isPlaying) sounds[8].Stop();
            if(sounds[9].isPlaying) sounds[9].Stop();
            gameOver = true;
            if(!sounds[12].isPlaying) sounds[12].Play();
            uIManager.GameOver();
            int s = scoreController.GetDistanceTravelled() + ingameUI.GetScore();
            leaderboard.SetScore(s);
            gameOverScore.text = s.ToString();
            canMove = false;
            shouldUpdateVelocities = false;
            cm.Stop();
            stopCoins();
            stopProjectiles();
            stopTeachers();
            stopObstacles();
            stopPowerups();
            stopFlyingCoffees();
            stopScore();
            flyingCoffeeHolder.StopSpawner();
            teacherSpawner.StopSpawner();
            flyingCoffeeSpawner.StopSpawner();
            animator.enabled = false;
            spriteRenderer.sprite = deadSprite;
        } else if(collider.gameObject.name == "coffee(Clone)"){
            sounds[1].Play();
            pm.SetPowerUp(0,true);
            coffeeSecs = 0;
            coffeeOn = true;
            Destroy(collider.gameObject);
            ingameUI.SetCoffeePowerup(true);
        } else if(collider.gameObject.name == "notes(Clone)"){
            sounds[2].Play();
            pm.SetPowerUp(1,true);
            notesSecs = 0;
            notesOn = true;
            Destroy(collider.gameObject);
            ingameUI.SetNotesPowerup(true);
        }
    }

    void stopCoins()
    {
        foreach (Transform coin in coinSequence.transform){
            coin.GetComponent<EctsLogic>().Stop();
        }
    }

    void resumeCoins()
    {
        foreach (Transform coin in coinSequence.transform){
            coin.GetComponent<EctsLogic>().Resume();
        }
    }

    void stopProjectiles()
    {
        foreach (Transform projectile in projectileHolder.transform){
            projectile.GetComponent<ProjectileBehavior>().Stop();
        }
    }

    void resumeProjectiles()
    {
        foreach (Transform projectile in projectileHolder.transform){
            projectile.GetComponent<ProjectileBehavior>().Resume();
        }
    }

    void stopTeachers()
    {
        foreach (Transform teacher in teachersHolder.transform){
            teacher.GetComponent<TeacherController>().Stop();
        }
    }

    void resumeTeachers()
    {
        foreach (Transform teacher in teachersHolder.transform){
            teacher.GetComponent<TeacherController>().Resume();
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
            obstacle.GetComponent<ObstacleBehavior>().Stop();
        }
    }

    void resumeObstacles()
    {
        foreach (Transform obstacle in os.transform){
            obstacle.GetComponent<ObstacleBehavior>().Resume();
        }
    }

    void stopPowerups()
    {
        foreach (Transform powerup in powerUpHolder.transform){
            powerup.GetComponent<PowerUpController>().Stop();
        }
    }

    void resumePowerups()
    {
        foreach (Transform powerup in powerUpHolder.transform){
            powerup.GetComponent<PowerUpController>().Resume();
        }
    }

    void stopFlyingCoffees()
    {

        foreach (Transform obj in flyingCoffeeHolder.transform){
            if (obj.gameObject.name == "flying_coffee(Clone)"){
                obj.GetComponent<FlyingCoffeeController>().Stop();
            } else {
                obj.GetComponent<WarningController>().stopTimer();
            }
        }

    }

    void resumeFlyingCoffees()
    {
        

        foreach (Transform obj in flyingCoffeeHolder.transform){
            if (obj.gameObject.name == "flying_coffee(Clone)"){
                obj.GetComponent<FlyingCoffeeController>().Resume();
            } else {
                obj.GetComponent<WarningController>().resumeTimer();
            }
        }
    }

    void stopScore()
    {
        scoreBoard.transform.Find("MeterCounter").GetComponent<ScoreController>().stopScore();
    }

    void resumeScore()
    {
        scoreBoard.transform.Find("MeterCounter").GetComponent<ScoreController>().resumeScore();
    }

    void updateVelocities(){
        if (paused){
            return;
        }
        numberOfVelocityIncreases++;

        scoreController.incrementVelocityIncrease();

        cm.UpdateSpeed(levelVelocityFactor);
        
        os.multiplySpeed(levelVelocityFactor);
        foreach (Transform obstacle in os.transform){
            obstacle.GetComponent<ObstacleBehavior>().UpdateVelocity(levelVelocityFactor);
        }

        ectsGenerator.multiplySpeed(levelVelocityFactor);
        foreach (Transform coin in coinSequence.transform){
            coin.gameObject.GetComponent<EctsLogic>().UpdateVelocity(levelVelocityFactor);
        }
        
        powerUpHolder.multiplySpeed(levelVelocityFactor);
        foreach (Transform powerup in powerUpHolder.transform){
            powerup.GetComponent<PowerUpController>().UpdateVelocity(levelVelocityFactor);
        }

        teacherSpawner.multiplySpeed(levelVelocityFactor);
        foreach (Transform teacher in teachersHolder.transform){
            teacher.gameObject.GetComponent<TeacherController>().UpdateVelocity(levelVelocityFactor);
        }

        flyingCoffeeSpawner.multiplySpeed(levelVelocityFactor);
    }

    public void Pause(){
        paused = true;
        controller.spawnActive = false;
        pressed = KeyState.Off;
        if(sounds[3].isPlaying) sounds[3].Stop();
        if(sounds[4].isPlaying) sounds[4].Stop();
        if(sounds[5].isPlaying) sounds[5].Stop();
        if(sounds[8].isPlaying) sounds[8].Pause();
        if(sounds[9].isPlaying) sounds[9].Pause();
        canMove = false;
        gameRunning = false;
        shouldUpdateVelocities = false;
        /*cm.Stop();
        stopCoins();
        stopProjectiles();
        stopTeachers();
        stopObstacles();
        stopPowerups();
        stopFlyingCoffees();
        stopScore();*/
        flyingCoffeeHolder.StopSpawner();
        foreach (GameObject warnings in GameObject.FindGameObjectsWithTag("Warnings")){
            warnings.GetComponent<AudioSource>().Pause();
        }
        teacherSpawner.StopSpawner();
        animator.enabled = false;
        Time.timeScale = 0;
    }

    public void Resume(){
        Time.timeScale = 1;
        paused = false;
        canMove = true;
        controller.spawnActive = true;
        gameRunning = true;
        animator.enabled = true;
        shouldUpdateVelocities = true;
        if(!sounds[8].isPlaying) sounds[8].UnPause();
        if(!sounds[9].isPlaying) sounds[9].UnPause();
        /*cm.Resume();
        resumeCoins();
        resumeProjectiles();
        resumeTeachers();
        resumeObstacles();
        resumePowerups();
        resumeFlyingCoffees();
        resumeScore();
        */
        flyingCoffeeHolder.ResumeSpawner();
        foreach (GameObject warnings in GameObject.FindGameObjectsWithTag("Warnings")){
            warnings.GetComponent<AudioSource>().UnPause();
        }
        teacherSpawner.resumeSpawner();
        powerUpHolder.ResumeSpawner();
    }

}
