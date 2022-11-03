using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ActButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject blueSquer;
    [SerializeField] private GameObject redSquer;
    private GameObject squer;
    private List<GameObject> thisCardSquers;
    private int activeTurnsCount;

    private bool canBeActivated;
    private bool isBlue;
    public string cardClass;
    private PhotonView pView;
    public bool isActivated;


    private void OnMouseDown()
    {

        if (GameManeger.myMana >= 2 && SF.isMyTurn() && pView.IsMine && canBeActivated)
        {
            SF.tmpObjListClear();
            SF.changeMana(-2);
            Board.activateThisClass(cardClass);

        }

    }

    void Start()
    {
        activeTurnsCount = 0;
        thisCardSquers = new List<GameObject>();
        isActivated = false;
        canBeActivated = true;
        cardClass = gameObject.tag;
        pView = GetComponent<PhotonView>();
        if (gameObject.transform.position.y < 0)
        {
            isBlue = true;
            squer = blueSquer;
        }
        else
        {
            isBlue = false;
            squer = redSquer;
        }
        if (LocalGameManager.isBlue == false)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 90);
        }

        if (pView.IsMine)
        {
            GameManeger.myActButtons.Add(gameObject);
        }
        else if (!pView.IsMine)
        {
            GameManeger.enemyActButtons.Add(gameObject);
        }
        gameObject.transform.SetParent(LocalGameManager.Board.transform);

        if (LocalGameManager.isBlue)
        {
            if (isBlue)
            {
                for (int i = 0; i < 3; i++)
                {
                    thisCardSquers.Add(Instantiate(squer, new Vector3(gameObject.transform.position.x - 1.114f + i * 1.114f, gameObject.transform.position.y - 2.186f, 0), Quaternion.identity));
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    thisCardSquers.Add(Instantiate(squer, new Vector3(gameObject.transform.position.x - 1.114f + i * 1.114f, gameObject.transform.position.y + 2.186f, 0), Quaternion.identity));
                }
            }
        }
        else
        {
            if (isBlue)
            {
                for (int i = 0; i < 3; i++)
                {
                    thisCardSquers.Add(Instantiate(squer, new Vector3(gameObject.transform.position.x + 1.114f - i * 1.114f, gameObject.transform.position.y - 2.186f, 0), Quaternion.identity));
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    thisCardSquers.Add(Instantiate(squer, new Vector3(gameObject.transform.position.x + 1.114f - i * 1.114f, gameObject.transform.position.y + 2.186f, 0), Quaternion.identity));
                }
            }
        }
    }

    public void flipActCard()
    {
        StartCoroutine(flipThisActCard());
    }

    IEnumerator flipThisActCard()
    {
        LocalGameManager.canClick = false;
        float x = Mathf.Abs(gameObject.transform.rotation.eulerAngles.x);
        float y = Mathf.Abs(gameObject.transform.rotation.eulerAngles.y);
        float z = Mathf.Abs(gameObject.transform.rotation.eulerAngles.z);
        if (LocalGameManager.isBlue)
        {
            if (y == 180f)
            {
                while (y != 360)
                {

                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    yield return new WaitForSeconds(0.01f);
                    y += 5f;
                }
                gameObject.transform.rotation = Quaternion.Euler(x, 0, z);
            }
            else
            {
                while (y != 180)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    yield return new WaitForSeconds(0.01f);
                    y += 5f;
                }
                gameObject.transform.rotation = Quaternion.Euler(x, -180, z);
            }
        }
        else
        {
            if (y == 0f)
            {
                while (y != 180)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    y += 5;
                    yield return new WaitForSeconds(0.01f);
                }
                gameObject.transform.rotation = Quaternion.Euler(x, -180, z);
            }
            else
            {
                while (y != 360)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    y += 5;
                    yield return new WaitForSeconds(0.01f);
                }
                gameObject.transform.rotation = Quaternion.Euler(x, 0, z);
            }

        }
        if (pView.IsMine && isActivated)
        {
            SF.pass();
        }
        else if(pView.IsMine)
        {
            canBeActivated = true;
        }
        LocalGameManager.canClick = true;
    }


    public void activateThis()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        isActivated = true;
        if (pView.IsMine)
        {
            activeTurnsCount = 4;
        }
        else
        {
            activeTurnsCount = 4;
        }

        foreach(GameObject obj in thisCardSquers)
        {
            obj.SetActive(true);
        }
    }
    public void diactivateThis()
    {
        canBeActivated = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        isActivated = false;
        if(pView.IsMine)
        {
            foreach (GameObject card in GameManeger.myCards)
            {
                if (cardClass == SF.getCardScript(card).cardClass)
                {
                    SF.getCardScript(card).diactivateThisCard();
                }
            }
            gameObject.GetComponent<ActButton>().flipActCard();
        }
        else
        {
            foreach (GameObject card in GameManeger.enemyCards)
            {
                if (cardClass == SF.getCardScript(card).cardClass)
                {
                    SF.getCardScript(card).diactivateThisCard();
                }
            }
            gameObject.GetComponent<ActButton>().flipActCard();
        }
    }

    public void checkThisActButtton()
    {
        if (activeTurnsCount > 0)
        {
            activeTurnsCount -= 1;
            for (int i = 0; i < 3; i++)
            {
                if(i < activeTurnsCount)
                {
                    thisCardSquers[i].SetActive(true);
                }
                else
                {
                    thisCardSquers[i].SetActive(false);
                }
            }
            if(activeTurnsCount == 0)
            {

                diactivateThis();
            }
        }
    }
}
