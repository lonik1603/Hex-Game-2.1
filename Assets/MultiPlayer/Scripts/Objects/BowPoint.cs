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

    [SerializeField] private GameObject point;

    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(bowPointCheck());
        }
    }
    private void OnMouseDown()
    {
        
        SF.changeMana(-1);
        PhotonNetwork.Instantiate("CardDestroyer", gameObject.transform.position, Quaternion.identity);
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
        else
        {
            LocalGameManager.tmpGameObjects.Add(Instantiate(point, gameObject.transform.position, Quaternion.identity));
            LocalGameManager.tmpGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
