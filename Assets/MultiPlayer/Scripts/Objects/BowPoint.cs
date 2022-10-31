using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPoint : MonoBehaviour
{
    public bool thisCardIsBlue;
    public bool thisCardIsActivated;
    private GameObject otherCard;

    [SerializeField] private GameObject point;

    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        
        SF.changeMana(-1);
        SF.getCardScript(LocalGameManager.activeCard).MoveTo(gameObject.transform.position);
        SF.tmpObjListClear();
    }
    protected void OnTriggerEnter(Collider other)
    {
        otherCard = other.gameObject;
    }
    
    IEnumerator bowPointCheck()
    {
        yield return new WaitForSeconds(0.3f);
        if(otherCard != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1f, 0f, 1f);
        }
        else
        {
            LocalGameManager.tmpGameObjects.Add(Instantiate(point, gameObject.transform.position, Quaternion.identity));
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
