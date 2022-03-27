using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraECTSBehavior : MonoBehaviour
{
    int frame_counter;
    Color objColor;
    // Start is called before the first frame update
    void Start()
    {
        frame_counter = 0;
        objColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(frame_counter < 20){
            frame_counter+=1;
            Vector3 pos = this.gameObject.transform.position;
            pos.y += 0.05f;
            this.gameObject.transform.position = pos;
            objColor.a = 1.0f - 0.05f*frame_counter;
            this.gameObject.GetComponent<SpriteRenderer>().color = objColor;
        }
        else{
            Destroy(this.gameObject);
        }
    }
}
