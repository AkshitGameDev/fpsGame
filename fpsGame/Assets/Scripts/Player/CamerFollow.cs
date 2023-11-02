using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager.instance.camMain.transform.rotation = transform.rotation;
        PlayerManager.instance.camMain.transform.position = transform.position;
    }
}
