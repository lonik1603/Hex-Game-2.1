using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayPoint1 : MonoBehaviour
{
    [SerializeField] private GameObject relayPoint2;

    protected static GameObject cardToSwap;
    private GameObject thisCard;
    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            thisCard = other.gameObject;
        }
    }
    private void OnMouseDown()
    {
        cardToSwap = thisCard;
        SF.tmpObjListDisable();
        foreach(GameObject card in GameManeger.myCards)
        {
            if (SF.getCardScript(card).cardClass == SF.getCardScript(cardToSwap).cardClass)
            {
                    LocalGameManager.tmpGameObjects.Add(Instantiate(relayPoint2,
                        new Vector3(card.transform.position.x, card.transform.position.y, -1), Quaternion.identity));
            }
        }
    }

}
