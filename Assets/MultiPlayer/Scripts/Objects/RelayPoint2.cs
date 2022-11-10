using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayPoint2 : RelayPoint1
{
    private GameObject thisCard2;
    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            thisCard2 = other.gameObject;
        }
    }
    private void OnMouseDown()
    {
        SF.getCardScript(cardToSwap).isMarked = false;
        SF.getCardScript(thisCard2).isMarked = true;

        SF.getCardScript(cardToSwap).GetComponent<Renderer>().material = SF.getCardScript(cardToSwap).defaultMaterial;
        SF.getCardScript(thisCard2).GetComponent<Renderer>().material = SF.getCardScript(thisCard2).markedMterial;

        LocalGameManager.activeCard.GetComponent<Relay>().useThisCard();
        SF.changeMana(-2);
        if (GameManeger.myMana == 0)
        {
            SF.pass();
        }
        SF.tmpObjListClear();
    }
}
