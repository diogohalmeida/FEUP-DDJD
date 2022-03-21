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

    public Texture2D map;
    public colorToObject[] colorsToObjects;

    public GameObject coinSequence;

    // Start is called before the first frame update
    void Start()
    {
        GenerateEctsMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateEctsMap()
    {
        for (int i = 0; i < map.width; i++){
            for (int j = 0; j < map.height; j++){
                GenerateEcts(i, j);
            }
        }
    }

    void GenerateEcts(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        foreach (colorToObject obj in colorsToObjects){
            if (!obj.color.Equals(pixelColor)){
                GameObject inst = Instantiate(obj.prefab, new Vector2(x-3.5f, y-3.5f), Quaternion.identity, coinSequence.transform);
                inst.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 0);
            }
        }

    }


}
