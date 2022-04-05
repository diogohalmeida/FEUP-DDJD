using System.Collections.Generic;
using UnityEngine;



public class InputManager : MonoBehaviour
{

    Dictionary<string, KeyCode> inputs = new Dictionary<string, KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        inputs["Fire"] = KeyCode.Z;
        inputs["JetpackUp"] = KeyCode.Space;
        inputs["Pause"] = KeyCode.P;
    }

    public void SetKey(string action, KeyCode key)
    {
        if (inputs.ContainsKey(action)){
            inputs[action] = key;
        }
    }

    public KeyCode GetKey(string action)
    {
        return inputs[action];
    }

}
