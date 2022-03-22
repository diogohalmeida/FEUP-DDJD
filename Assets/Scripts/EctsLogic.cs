using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EctsLogic : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position[0] <= -16){
            Destroy(this.gameObject);
        }
    }
}
