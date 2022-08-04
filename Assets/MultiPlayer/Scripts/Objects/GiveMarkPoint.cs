using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMarkPoint : MonoBehaviour
{
    private void OnMouseDown()
    {
        Board.giveMeMark();
        SF.changeMana(-1);
        SF.getCardScript(LocalGameManager.activeCard).giveMark();
        SF.tmpObjListClear();
    }
}
