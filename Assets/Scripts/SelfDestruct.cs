﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float delay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
