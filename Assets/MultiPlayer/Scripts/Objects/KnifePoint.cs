using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class KnifePoint : MonoBehaviour
{
    private GameObject otherCard;
    private bool thisPointEat;

    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(knifePointCheck());
        }
    }
    private void OnMouseDown()
    {
        LocalGameManager.activeCard.transform.position = new Vector3(LocalGameManager.activeCard.transform.position.x, LocalGameManager.activeCard.transform.position.y, -1);
       
        SF.tmpObjListClear();
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            otherCard = other.gameObject;
        }
    }

    IEnumerator knifePointCheck()
    {
        yield return new WaitForSeconds(0.05f);
        if (otherCard != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1f, 0f, 1f);
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            if (SF.getCardScript(LocalGameManager.activeCard).hasEaten)
            {
                LocalGameManager.tmpGameObjects.Remove(gameObject);
                Destroy(gameObject);
            }

        }
    }
}
