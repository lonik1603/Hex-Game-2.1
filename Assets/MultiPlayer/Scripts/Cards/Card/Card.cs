using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Card : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material markedMterial;
    [SerializeField] protected GameObject point;

    protected bool cardIsBlue;
    public string cardClass;
    public bool isActivated;
    protected bool isMarked;
    protected PhotonView pView;

    public bool canMove;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(cardIsBlue);
            stream.SendNext(isActivated);
            stream.SendNext(isMarked);
        }
        else
        {
            cardIsBlue = (bool)stream.ReceiveNext();
            isActivated = (bool)stream.ReceiveNext();
            isMarked = (bool)stream.ReceiveNext();
        }
    }
    protected void Start()
    {
        pView = GetComponent<PhotonView>();
        isActivated = false;
        isMarked = false;
        canMove = true;
        cardClass = gameObject.tag;

        if (pView.IsMine)
        {
            cardIsBlue = LocalGameManager.isBlue;
            GameManeger.myCards.Add(gameObject);
        }
        else
        {
            GameManeger.enemyCards.Add(gameObject);
        }

        if (LocalGameManager.isBlue == false)
        {
            gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
    public void markCard()
    {
        gameObject.GetComponent<Renderer>().material = markedMterial;
        isMarked = true;
    }

    protected void OnMouseDown()
    {
        if (SF.isMyTurn() && GameManeger.myMana > 0 && pView.IsMine && canMove)
        {
            LocalGameManager.activeCard = gameObject;
            SF.tmpObjListClear();
            spawnPoints();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            if((cardIsBlue && LocalGameManager.isBlue) || (!cardIsBlue && !LocalGameManager.isBlue))
            {
                LocalGameManager.tmpGameObjects.Remove(other.gameObject);
                Destroy(other.gameObject);
            }

        }            
        else if(SF.cardClassList.Contains(other.gameObject.tag))
        {
            if((!cardIsBlue && GameManeger.isBlueTurn) || (cardIsBlue && !GameManeger.isBlueTurn))
            {
                if(isMarked)
                {
                    Board.giveMark();
                }
                SF.getCardScript(other.gameObject).canMove = false;
                LocalGameManager.cantMoveCards.Add(other.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    public void activateThisCard()
    {
        isActivated = true;
        StartCoroutine(flipThisCard());
    }
    public void diactivateThisCard()
    {
        isActivated = false;
        StartCoroutine(flipThisCard());
    }
    IEnumerator flipThisCard()
    {
        float x = Mathf.Abs(gameObject.transform.rotation.eulerAngles.x);
        float y = Mathf.Abs(gameObject.transform.rotation.eulerAngles.y);
        float z = Mathf.Abs(gameObject.transform.rotation.eulerAngles.z);
        if (LocalGameManager.isBlue)
        {
            if (z == 0f)
            {
                while (z != 180f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    yield return new WaitForSeconds(0.01f);
                    z += 5f;
                }
                gameObject.transform.rotation = Quaternion.Euler(x, y, -180f);
            }
            else if (z == 180f)
            {
                while (z != 360f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    yield return new WaitForSeconds(0.01f);
                    z += 5f;
                }
                gameObject.transform.rotation = Quaternion.Euler(x, y, 0f);
            }
        }
        else
        {
            if (z == 0)
            {
                while (z != 180)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    z += 5;
                    yield return new WaitForSeconds(0.01f);
                }
                gameObject.transform.rotation = Quaternion.Euler(x, y, -180);
            }
            else if (z == 180f)
            {
                while (z != 360)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    z += 5;
                    yield return new WaitForSeconds(0.01f);
                } 
                gameObject.transform.rotation = Quaternion.Euler(x, y, 0);
            }
           
        }
    }



    protected virtual void spawnPoints()
    {
        LocalGameManager.tmpGameObjects.Add
            (Instantiate(point,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 2, -1),
            Quaternion.identity));
        LocalGameManager.tmpGameObjects.Add
            (Instantiate(point,
            new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y + SF.hexUp, -1),
            Quaternion.identity));
        LocalGameManager.tmpGameObjects.Add
            (Instantiate(point,
            new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y - SF.hexUp, -1),
            Quaternion.identity));
        LocalGameManager.tmpGameObjects.Add
            (Instantiate(point,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - SF.hexUp * 2, -1),
            Quaternion.identity));
        LocalGameManager.tmpGameObjects.Add
            (Instantiate(point,
            new Vector3(gameObject.transform.position.x -3, gameObject.transform.position.y - SF.hexUp, -1),
            Quaternion.identity));
        LocalGameManager.tmpGameObjects.Add
            (Instantiate(point,
            new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + SF.hexUp, -1),
            Quaternion.identity));
    }
    public void tryToSpawnPoints()
    {
        if (canMove)
        {
            Color pointColor = new Color(1, 0, 0.85783f, 0.5f);
            List<GameObject> newPoints = new List<GameObject>();

            newPoints.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 2, -1),
                Quaternion.identity));
            newPoints.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y + SF.hexUp, -1),
                Quaternion.identity));
            newPoints.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            newPoints.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - SF.hexUp * 2, -1),
                Quaternion.identity));
            newPoints.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            newPoints.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + SF.hexUp, -1),
                Quaternion.identity));
            foreach (GameObject obj1 in newPoints)
            {
                obj1.GetComponent<SpriteRenderer>().color = pointColor;
                obj1.GetComponent<BoxCollider>().enabled = true;
                LocalGameManager.tmpGameObjects.Add(obj1);
            }
            newPoints.Clear();
        }
    }


}
