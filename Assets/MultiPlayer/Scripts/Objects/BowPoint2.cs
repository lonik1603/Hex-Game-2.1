using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPoint2 : MonoBehaviour
{
    private GameObject otherCard;

    [SerializeField] private GameObject point;

    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            otherCard = null;
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }

        StartCoroutine(visualisePoint());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.tag))
        {
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }

    }
    private void OnMouseDown()
    {

        SF.changeMana(-1);
        if (otherCard != null)
        {
            if (otherCard.tag == "Shild" && SF.getCardScript(otherCard).isActivated)
            {
                CardsController.diactivateThisCard(otherCard);
                SF.getCardScript(LocalGameManager.activeCard).canMove = false;

            }
            else
            {
                CardsController.moveThisCardTo(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), LocalGameManager.activeCard, otherCard);
                SF.getCardScript(LocalGameManager.activeCard).hasEaten = true;
                SF.getCardScript(LocalGameManager.activeCard).canMove = false;
            }
        }
        else
        {
            CardsController.moveThisCardTo(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), LocalGameManager.activeCard);
        }
        if (GameManeger.myMana == 0)
        {
            SF.pass();
        }
        SF.tmpObjListClear();
    }

    IEnumerator visualisePoint()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.85f, 0.6f);
    }
}
