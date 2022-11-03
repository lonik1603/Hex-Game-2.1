using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TrapPoint : MonoBehaviour
{

    private static int trapCount;

    private void Start()
    {
        trapCount = 0;
        if((Mathf.Abs(Mathf.Abs(gameObject.transform.position.x) - 6) < 0.5f) && Mathf.Abs(gameObject.transform.position.y - 3 * SF.hexUp) < 0.5f 
            || (Mathf.Abs(Mathf.Abs(gameObject.transform.position.x) - 6) < 0.5f) && Mathf.Abs(gameObject.transform.position.y + 3 * SF.hexUp) < 0.5f)
        {
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
        StartCoroutine(VisaliseThisPoint());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        trapCount++;
        if (LocalGameManager.isBlue)
        {
            PhotonNetwork.Instantiate("BlueTrapIcon",
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 2), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("RedTrapIcon",
                 new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 2), Quaternion.identity);
        }
        if (trapCount < 3)
        {
            LocalGameManager.canClick = false;
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);

        }
        else
        {
            foreach (GameObject obj in LocalGameManager.tmpGameObjects)
            {
                if (obj != gameObject)
                {
                    Destroy(obj);
                }
            }
            LocalGameManager.canClick = true;
            LocalGameManager.activeCard.GetComponent<Trap>().useThisCard();
            SF.changeMana(-2);
            if (GameManeger.myMana == 0)
            {
                SF.pass();
            }
            LocalGameManager.tmpGameObjects.Clear();
            Destroy(gameObject);
        }

    }

    IEnumerator VisaliseThisPoint()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0 , 0.85f, 0.5f);
    }


}
