using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (LocalGameManager.canClick)
        {
            SF.pass();
        }
    }

}
