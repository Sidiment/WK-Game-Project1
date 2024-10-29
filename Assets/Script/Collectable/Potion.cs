using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, ICollectable, IUseable
{
    [SerializeField]PotionSO potionSO;
    // public static event Action potionCollect;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Use();
        }
    }
    public void Collect()
    {
        //Add music to delegate
        //Add Score to delegate
    }

    public void Use()
    {
        potionSO.Use();
    }
}
