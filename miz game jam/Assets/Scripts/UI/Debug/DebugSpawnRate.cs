using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugSpawnRate : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = SpawnManager.SpawnRate.ToString();
    }
}
