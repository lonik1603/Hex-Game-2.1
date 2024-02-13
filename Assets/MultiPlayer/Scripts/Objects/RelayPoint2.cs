using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayPoint2 : RelayPoint1
{
    private GameObject thisCard2;
    private static bool isMarked;
    private static bool isCorrupted;
    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            thisCard2 = other.gameObject;
        }
    }
    private void OnMouseDown()
    {
        if(SF.getCardScript(cardToSwap).isMarked)
        {
            SF.getCardScript(thisCard2).GetComponent<Renderer>().material = SF.getCardScript(thisCard2).markedMterial;
        }
        else if (SF.getCardScript(cardToSwap).isCorrupted)
        {
            SF.getCardScript(thisCard2).GetComponent<Renderer>().material = SF.getCardScript(thisCard2).corruptedMterial;
        }
        else
        {
            SF.getCardScript(thisCard2).GetComponent<Renderer>().material = SF.getCardScript(thisCard2).defaultMaterial;
        }

        if (SF.getCardScript(thisCard2).isMarked)
        {
            SF.getCardScript(cardToSwap).GetComponent<Renderer>().material = SF.getCardScript(cardToSwap).markedMterial;
        }
        else if (SF.getCardScript(thisCard2).isCorrupted)
        {
            SF.getCardScript(cardToSwap).GetComponent<Renderer>().material = SF.getCardScript(cardToSwap).corruptedMterial;
        }
        else
        {
            SF.getCardScript(cardToSwap).GetComponent<Renderer>().material = SF.getCardScript(cardToSwap).defaultMaterial;
        }


        isMarked = SF.getCardScript(cardToSwap).isMarked;
        SF.getCardScript(cardToSwap).isMarked = SF.getCardScript(thisCard2).isMarked;
        SF.getCardScript(thisCard2).isMarked = isMarked;


        isCorrupted = SF.getCardScript(cardToSwap).isCorrupted;
        SF.getCardScript(cardToSwap).isCorrupted = SF.getCardScript(thisCard2).isCorrupted;
        SF.getCardScript(thisCard2).isCorrupted = isCorrupted;


        LocalGameManager.activeCard.GetComponent<Relay>().useThisCard();
        SF.changeMana(-2);
        if (GameManeger.myMana == 0)
        {
            SF.pass();
        }
        SF.tmpObjListClear();
    }
}
