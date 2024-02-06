using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class KnifePoint : MonoBehaviour
{
    public bool thisCardIsBlue;
    public string thisCardClass;
    public bool thisCardIsActivated;
    private GameObject otherCard;

    [SerializeField] private GameObject point;

    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }

        StartCoroutine(visualisePoint());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.tag))
        {
            otherCard = other.gameObject;
        }

    }
    private void OnMouseDown()
    {

        if (otherCard != null)
        {
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
                CardsController.moveThisCardTo(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), LocalGameManager.activeCard, otherCard);
            }
        }
        else
        {
            CardsController.moveThisCardTo(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), LocalGameManager.activeCard);
            SF.changeMana(-1);
        }
        SF.tmpObjListClear();
    }

    IEnumerator visualisePoint()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.85f, 0.6f);
    }
}
