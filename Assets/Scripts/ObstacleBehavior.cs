using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{

    int type;
    private MapController controller;

    void Start()
    {
        controller = GameObject.Find("MapController").GetComponent<MapController>();
    }

    void setType(int t){
        type = t;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position[0] <= -16){
            Destroy(this.gameObject);
            if (type != 1 && type != 2){
                controller.NextSection();
            }
        }
    }
}
