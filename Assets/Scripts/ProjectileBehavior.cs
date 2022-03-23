using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileBehavior : MonoBehaviour
{

    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position[0] >= 9){
            Destroy(this.gameObject);
        }
    }

}
