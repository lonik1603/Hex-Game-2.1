using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CardDestroyer : MonoBehaviour
{
    private PhotonView pView;
    private void Start()
    {
        pView = GetComponent<PhotonView>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError(other.gameObject.tag);
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {

            if(SF.getCardScript(other.gameObject).isMarked && pView.IsMine)
            {
                Board.giveMeMark();
            }
            if(SF.getCardScript(other.gameObject).pView.IsMine)
            {
                GameManeger.myCards.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                GameManeger.enemyCards.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }

    }
}
