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

    public GameObject obstacle;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        maxTimer = Random.Range(5f, 12f);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("SpawnEnemyTimer");
    }

    void SpawnEnemy(){
        float x = 10.0f;
        float y = Random.Range(-3.0f, 3.7f);
        Vector3 spawnPoint = new Vector3(x, y, 0);
        if (obstacle.activeSelf){
            Vector2[] points = obstacle.GetComponent<EdgeCollider2D>().points;
            while (y >= points[0][1] && y <= points[4][1]){
                y = Random.Range(-3.0f, 3.7f);
            }
        }
        spawnPoint.z = 0;
        int rotation = Random.Range(1,4);
        GameObject teacher;
        switch(rotation){
            case 1:
                teacher = GameObject.Instantiate(cbmPrefab, spawnPoint, new Quaternion(0,0,0,0));
                teacher.transform.SetParent(teachersHolder.transform);
                break;
            case 2: 
                teacher = GameObject.Instantiate(asPrefab, spawnPoint, new Quaternion(0,0,0,0));
                teacher.transform.SetParent(teachersHolder.transform);
                break;
            case 3: 
                teacher = GameObject.Instantiate(aprPrefab, spawnPoint, new Quaternion(0,0,0,0));
                teacher.transform.SetParent(teachersHolder.transform);
                break;
        }
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

}
