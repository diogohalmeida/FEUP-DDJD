using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class colorToObject
{
    public GameObject prefab;
    public Color color;
}


public class EctsGenerator : MonoBehaviour
{
    public colorToObject[] colorsToObjects;

    public MapController controller;

    public GameObject coinSequence;

    public Texture2D[] maps;

    public Texture2D coinsOnTopMap;
    public Texture2D coinsOnBottomMap;

    bool isActive;

    float speed = -4f;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateEctsMap();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (coinSequence.transform.childCount == 0 && isActive){
            controller.NextSection();
            isActive = false;
        }
        /*
        if (generateEcts){
            GenerateEctsMap(SelectMap());
            generateEcts = false;
        }
        */
    }

    public void multiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }


    public void SpawnEcts(int type){
        if (type == 0){
            // Normal spawn
            GenerateEctsMap(SelectMap());
        } else if (type == 1) {
            // Ects on top
            GenerateEctsMap(coinsOnTopMap);
        } else if (type == 2){
            GenerateEctsMap(coinsOnBottomMap);
        }
        
    }

    Texture2D SelectMap()
    {
        return maps[Random.Range(0, maps.Length)];
    }

    void GenerateEctsMap(Texture2D map)
    {
        isActive = true;
        for (int i = 0; i < map.width; i++){
            for (int j = 0; j < map.height; j++){
                GenerateEcts(i, j, map);
            }
        }
    }

    void GenerateEcts(int x, int y, Texture2D map)
    {
        Color pixelColor = map.GetPixel(x, y);

        foreach (colorToObject obj in colorsToObjects){
            if (!obj.color.Equals(pixelColor)){
                GameObject inst = Instantiate(obj.prefab, new Vector2(x, y-3.5f), Quaternion.identity, coinSequence.transform);
                inst.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            }
        }

    }


}
