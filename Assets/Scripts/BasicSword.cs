using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSword : MonoBehaviour
{
    public List<StatBase> Stats { get; set; }

    public void PerformAttack()
    {
        Debug.Log("Perform Attack in Basic Sword");
    }
}
