using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionPoint : MonoBehaviour
{

    private GameObject card;


    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
            card = other.gameObject;
    }

    private void OnMouseDown()
    {
        SF.getCardScript(card).corruptCard();

        LocalGameManager.corruptionCount++;
        if (LocalGameManager.corruptionCount == 3)
        {
            SF.tmpObjListClear();
            MarksChoseStage.playerIsReady();
        }
        Destroy(gameObject);
    }
}
