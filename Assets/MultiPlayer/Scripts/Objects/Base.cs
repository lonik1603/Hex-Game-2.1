using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Base : MonoBehaviour
{
    [SerializeField] private Material BlueCrown;
    [SerializeField] private Material RedCrown;

    [SerializeField] private Material BlueCrownM;
    [SerializeField] private Material RedCrownM;
    private GameObject newCrown;
    private bool isBlue;
    private bool thisCardisMarked;
    private bool thisCardCanMove;
    private string cardName;
    private void Start()
    {
        if(gameObject.transform.position.y < 0)
        {
            isBlue = true;
            cardName = "RedCrown";
        }
        else
        {
            isBlue = false;
            cardName = "BlueCrown";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (LocalGameManager.gameStarted)
        {
            if ((SF.cardClassList.Contains(other.gameObject.tag) && other.gameObject.tag != "Crown")
                && (isBlue && !SF.getCardScript(other.gameObject).cardIsBlue || !isBlue && SF.getCardScript(other.gameObject).cardIsBlue))
            {

                if (SF.getCardScript(other.gameObject).pView.IsMine)
                {

                    GameManeger.myCards.Remove(other.gameObject);
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, 10);
                    thisCardisMarked = SF.getCardScript(other.gameObject).isMarked;
                    thisCardCanMove = SF.getCardScript(other.gameObject).canMove;
                    SF.getCardScript(other.gameObject).canMove = false;
                    newCrown = PhotonNetwork.Instantiate(cardName,
                        new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, 0), Quaternion.Euler(-90, 0, -180));
                    if (thisCardisMarked)
                    {
                        SF.getCardScript(newCrown).markCard();
                    }
                    SF.getCardScript(newCrown).canMove = thisCardCanMove;
                }
                else
                {
                    GameManeger.enemyCards.Remove(other.gameObject);
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, 10);
                }
            }
        }
    }
}
