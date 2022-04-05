using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    int mapSection;

    public EctsGenerator ectsGenerator;
    public ObstacleSpawner obstacleSpawner;

    bool sectionActive;

    public bool spawnActive;

    // Start is called before the first frame update
    void Start()
    {
        spawnActive = true;
        sectionActive = false;
        mapSection = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sectionActive && spawnActive){
            sectionActive = true;
            GenerateSection();
        }
    }

    void GenerateSection(){
        switch (mapSection){
            case 1:
                ectsGenerator.SpawnEcts(0);
                break;
            case 0:
            case 5:
                obstacleSpawner.SpawnObstacle(0);
                break;
            case 2:
                // Coins on top, obstacle on bottom
                ectsGenerator.SpawnEcts(1);
                obstacleSpawner.SpawnObstacle(1);
                break;
            case 3:
                ectsGenerator.SpawnEcts(2);
                obstacleSpawner.SpawnObstacle(2);
                // Coins on bottom, obstacle on top
                break;
        }
    }

    public void NextSection(){
        sectionActive = false;
        mapSection = Random.Range(0, 6);
    }
}
