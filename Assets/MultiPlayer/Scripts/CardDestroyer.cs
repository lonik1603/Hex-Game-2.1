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
        StartCoroutine(destroyObject());
    }
    private void OnTriggerStay(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            if (SF.getCardScript(other.gameObject).pView.IsMine != pView.IsMine)
            {
                if(SF.getCardScript(other.gameObject).pView.IsMine == false && SF.getCardScript(other.gameObject).isMarked)
                {
                    Board.giveMeMark();
                }
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
