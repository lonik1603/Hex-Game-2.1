using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool thisCardIsBlue;
    public string thisCardClass;
    public bool thisCardIsActivated;
    private GameObject otherCard;
    private Object activeCardScript;

    [SerializeField] private GameObject point;

    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
        activeCardScript = LocalGameManager.activeCard.GetComponent<Card>();
        StartCoroutine(visualisePoint());
    }

        private void OnTriggerEnter(Collider other)
    {        
        if(SF.cardClassList.Contains(other.tag))
        {
            otherCard = other.gameObject;
        }

    }
    private void OnMouseDown()
    {

        SF.changeMana(-1);
        //MoveTo
        if (otherCard != null)
        {
            //Destroy(otherCard)
        }
        SF.tmpObjListClear();
    }

    IEnumerator visualisePoint()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.85f, 0.6f);
    }
}
