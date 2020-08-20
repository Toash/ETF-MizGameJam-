using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float MaxDifficultyFactor;
    
    
    public static LevelManager i;

    public static float DifficultyFactor;
    private void Awake()
    {
        DifficultyFactor = 1;
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DifficultyFactor = Mathf.Min(MaxDifficultyFactor,DifficultyFactor += (Time.deltaTime * .02f));
    }
}
