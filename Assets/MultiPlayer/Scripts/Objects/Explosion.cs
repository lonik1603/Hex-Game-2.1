using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.position.x < 14 || gameObject.transform.position.x > -14 || gameObject.transform.position.y > -13 || gameObject.transform.position.y < 13)
        {
            if(SF.cardClassList.Contains(other.gameObject.tag))
            {
                if (SF.getCardScript(other.gameObject).pView.IsMine)
                {
                    GameManeger.myCards.Remove(other.gameObject);
                    if (SF.getCardScript(other.gameObject).isMarked)
                    {
                        Board.giveOtherPlayerMark();
                    }
                }
                else
                {
                    GameManeger.enemyCards.Remove(other.gameObject);
                }
                Destroy(other.gameObject);
            }
        }
    }
}
