using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BowPoint : MonoBehaviour
{
    public bool thisCardIsBlue;
    public bool thisCardIsActivated;
    private GameObject otherCard;
    public int bowPointCount;
    private GameObject newBowPoint;

    [SerializeField] private GameObject point;
    [SerializeField] private GameObject bowPoint;
    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
        else
        {
            if (GameManeger.myMana > bowPointCount)
            {
                if (LocalGameManager.isBlue)
                {
                    newBowPoint = Instantiate(bowPoint,
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 2, -1),
                    Quaternion.identity);
                }
                else
                {
                    newBowPoint = Instantiate(bowPoint,
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - SF.hexUp * 2, -1),
                    Quaternion.identity);
                    LocalGameManager.tmpGameObjects.Add(newBowPoint);
                }
                LocalGameManager.tmpGameObjects.Add(newBowPoint);
                newBowPoint.GetComponent<BowPoint>().bowPointCount = bowPointCount + 1;
            }
            StartCoroutine(bowPointCheck());
        }


    }
    private void OnMouseDown()
    {
        
        SF.changeMana(-bowPointCount);
        if (otherCard.tag == "Shild" && SF.getCardScript(otherCard).isActivated)
        {
            CardsController.diactivateThisCard(otherCard);
            if (GameManeger.myMana == 0)
            {
                SF.pass();
            }
        }
        else
        {
            CardsController.eatThisCard(otherCard);
        }
        SF.tmpObjListClear();
    }
    protected void OnTriggerEnter(Collider other)
    {
        otherCard = other.gameObject;
    }
    
    IEnumerator bowPointCheck()
    {
        yield return new WaitForSeconds(0.05f);
        if(otherCard != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1f, 0f, 1f);
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else if (bowPointCount == 1)
        {
            LocalGameManager.tmpGameObjects.Add(Instantiate(point, gameObject.transform.position, Quaternion.identity));
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
        if (GameManeger.myMana == 0)
        {
            SF.pass();
        }
    }
}
