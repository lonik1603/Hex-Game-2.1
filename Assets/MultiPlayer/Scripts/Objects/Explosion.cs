using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(explosionCur());
    }
    
    
    IEnumerator explosionCur()
    {
        float scale = 1;
        while(scale < 8)
        {
            scale += Time.deltaTime * 30;
            gameObject.transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
        Destroy(gameObject);
        if(GameManeger.myMana == 0)
        {
            SF.pass();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (gameObject.transform.position.x < 14 || gameObject.transform.position.x > -14 || gameObject.transform.position.y > -13 || gameObject.transform.position.y < 13)
        {
            if(SF.cardClassList.Contains(other.gameObject.tag))
            {
                if(GetComponent<PhotonView>().IsMine)
                {
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    if (SF.getCardScript(other.gameObject).pView.IsMine)
                    {

                        if (SF.getCardScript(other.gameObject).isMarked)
                        {
                         //   Board.giveOtherPlayerMark();
                        }                      
                    }

                    CardsController.eatThisCard(other.gameObject);
                }

            }
        }
    }
}
