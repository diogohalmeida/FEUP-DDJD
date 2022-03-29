using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCoffeeSpawner : MonoBehaviour
{



    [SerializeField]
    private GameObject warningPrefab;

    private float timer;
    private float maxTimer;

    private bool spawn;

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
        StartCoroutine("SpawnWarningTimer");
    }

    void SpawnWarning()
    {
        if (!spawn) {
            return;
        }
        float x = 7.5f;
        float y = Random.Range(-3.0f, 3.7f);
        Vector3 spawnPoint = new Vector3(x, y, 0);
        GameObject warning;
        warning = GameObject.Instantiate(warningPrefab, spawnPoint, new Quaternion(0,0,0,0));

    }

    public void StopSpawner()
    {
        spawn = false;
    }

    IEnumerator SpawnWarningTimer()
    {
        if (timer >= maxTimer){
            SpawnWarning();
            timer = 0;
            maxTimer = Random.Range(10f,100f);
        }

        timer += 0.1f;
        yield return new WaitForSeconds(0.1f);
    }
}
