using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int MaxHp;
    [SerializeField] private int Recovery;

    void Start()
    {
       Health = MaxHp = 3;
        Recovery = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
