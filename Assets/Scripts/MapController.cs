using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    int mapSection;

    public EctsGenerator ectsGenerator;
    public ObstacleSpawner obstacleSpawner;

    bool sectionActive;

    // Start is called before the first frame update
    void Start()
    {
        sectionActive = false;
        mapSection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(sectionActive);
        if (!sectionActive){
            sectionActive = true;
            GenerateSection();
        }
    }

    void GenerateSection(){
        Debug.Log("Generate Section");
        switch (mapSection){
            case 0:
            case 2:
                ectsGenerator.SpawnEcts();
                break;
            case 1:
                obstacleSpawner.respawnObstacle();
                break;
            case 3:
                // Coins on top, obstacle on bottom
                
                break;
            case 4:
                // Coins on bottom, obstacle on top
                break;
        }
    }

    public void NextSection(){
        sectionActive = false;
        if (mapSection == 4){
            mapSection = 0;
        } else {
            mapSection++;
        }
    }
}
