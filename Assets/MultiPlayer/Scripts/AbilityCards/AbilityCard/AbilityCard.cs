using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : MonoBehaviour
{
    void Start()
    {
        if(LocalGameManager.isBlue == false)
        {
            gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

}
