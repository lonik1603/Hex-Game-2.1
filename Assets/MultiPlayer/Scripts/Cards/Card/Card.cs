using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Card : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] public Material defaultMaterial;
    [SerializeField] public Material markedMterial;
    [SerializeField] protected GameObject point;
    [SerializeField] protected GameObject giveMarkPoint;

    public bool cardIsBlue;
    public string cardClass;
    public bool isActivated;
    public bool isMarked;
    public PhotonView pView;
    public bool canGiveMark;

    public bool canMove;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isActivated);
            stream.SendNext(isMarked);
        }
        else
        {
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
            cardIsBlue = !LocalGameManager.isBlue;
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
            else if(cardClass == "Shild" && isActivated)
            {
                LocalGameManager.tmpGameObjects.Remove(other.gameObject);
                Destroy(other.gameObject);
            }

        }            
        else if(SF.cardClassList.Contains(other.gameObject.tag))
        {
            if(pView.IsMine && SF.isMyTurn())
            {
                if(SF.getCardScript(other.gameObject).isMarked)
                {
                    Board.giveMeMark();
                }
                canMove = false;
                GameManeger.enemyCards.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
            else if (photonView.IsMine)
            {
                GameManeger.myCards.Remove(gameObject);
                Destroy(gameObject);
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
    public virtual void MoveTo(Vector3 finPos)
    {
        StartCoroutine(MoveToCur(finPos));
    }

    IEnumerator MoveToCur(Vector3 finPos)
    {
        LocalGameManager.canClick = false;
        Vector3 startPos = gameObject.transform.position;
        for (float i = 0; i < 1; i += Time.deltaTime * 10)
        {
            gameObject.transform.position = Vector3.Lerp(startPos, finPos, i);
            yield return null;
        }
        LocalGameManager.activeCard.transform.position = finPos;
        LocalGameManager.canClick = true;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        yield return new WaitForSeconds(0.1f);
        canGiveMark = false;
        if (GameManeger.myMana > 0)
        {
            spawnPoints();
        }
        else
        {
            SF.pass();
            SF.tmpObjListClear();
        };
    }

    protected virtual void spawnPoints()
    {
        if (canMove)
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
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + SF.hexUp, -1),
                Quaternion.identity));
            if (canGiveMark)
            {
                LocalGameManager.tmpGameObjects.Add(Instantiate(giveMarkPoint,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity));
            }
        }
    }

    public virtual void cardCheck()
    {
        canMove = true;
        if (isMarked)
        {
            if (cardIsBlue)
            {
                if ((Mathf.Abs(Mathf.Abs(gameObject.transform.position.x) - 6) < 0.5f) && Mathf.Abs(gameObject.transform.position.y - 3 * SF.hexUp) < 0.5f)
                {
                    canGiveMark = true;
                }
            }
            else
            {
                if ((Mathf.Abs(Mathf.Abs(gameObject.transform.position.x) - 6) < 0.5f) && Mathf.Abs(gameObject.transform.position.y + 3 * SF.hexUp) < 0.5f)
                {
                    canGiveMark = true;
                }
            }
        }
    }
    public void giveMark()
    {
        isMarked = false;
        canGiveMark = false;
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }
}
