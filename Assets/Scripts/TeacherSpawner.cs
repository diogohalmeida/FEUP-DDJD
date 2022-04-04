using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject aprPrefab;
    [SerializeField]
    private GameObject cbmPrefab;
    [SerializeField]
    private GameObject asPrefab;

    [SerializeField]
    private GameObject teachersHolder;

    private float timer;
    private float maxTimer;

    [SerializeField]
    private GameObject obstacleHolder;

    private bool spawn;

    public float asSpeed;
    public float cbmSpeed;
    public float aprSpeed;

    public void multiplySpeed(float multiplyFactor)
    {
        asSpeed *= multiplyFactor;
        aprSpeed *= multiplyFactor;
        cbmSpeed *= multiplyFactor;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
        timer = 0;
        maxTimer = Random.Range(5f, 12f);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("SpawnEnemyTimer");
    }

    void SpawnEnemy(){
        if (!spawn) {
            return;
        }
        float x = 10.0f;
        float y = Random.Range(-3.0f, 3.7f);
        Vector3 spawnPoint = new Vector3(x, y, 0);
        bool valid = false;
        while (!valid){
            valid = true;
            foreach (Transform obstacle in obstacleHolder.transform){
                if (obstacle.position[0] >= -9){
                    if (spawnPoint.y <= obstacle.position[1] + 0.95f && spawnPoint.y >= obstacle.position[1] - 1.33f){
                        valid = false;
                        break;
                    }
                }
            }
            if (!valid){
                spawnPoint.y = Random.Range(-3.0f, 3.7f);
            }
        }
        int rotation = Random.Range(1,4);
        GameObject teacher;
        switch(rotation){
            case 1:
                teacher = GameObject.Instantiate(cbmPrefab, spawnPoint, new Quaternion(0,0,0,0));
                teacher.transform.SetParent(teachersHolder.transform);
                teacher.GetComponent<Rigidbody2D>().velocity = new Vector2(cbmSpeed, 0);
                teacher.GetComponent<TeacherController>().SetSpeedAndType(cbmSpeed, 3);
                break;
            case 2: 
                teacher = GameObject.Instantiate(asPrefab, spawnPoint, new Quaternion(0,0,0,0));
                teacher.transform.SetParent(teachersHolder.transform);
                teacher.GetComponent<Rigidbody2D>().velocity = new Vector2(asSpeed, 0);
                teacher.GetComponent<TeacherController>().SetSpeedAndType(asSpeed, 2);
                break;
            case 3: 
                teacher = GameObject.Instantiate(aprPrefab, spawnPoint, new Quaternion(0,0,0,0));
                teacher.transform.SetParent(teachersHolder.transform);
                float yVelocity = Random.Range(-3f, 3f);
                teacher.GetComponent<Rigidbody2D>().velocity = new Vector2(aprSpeed, yVelocity);
                teacher.GetComponent<TeacherController>().SetSpeedAndType(aprSpeed, 1);
                break;
        }
    }

    public void StopSpawner()
    {
        spawn = false;
    }

    IEnumerator SpawnEnemyTimer(){
        if (timer >= maxTimer){
            SpawnEnemy();
            timer = 0;
            maxTimer = Random.Range(10f,100f);
        }

        timer += 0.1f;
        yield return new WaitForSeconds(0.1f);
    }

    public void resumeSpawner()
    {
        spawn = true;
    }

}
