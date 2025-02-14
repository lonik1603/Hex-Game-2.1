using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MarksChoseStage : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private GameObject markPoint;
    private static int readyPlayers;
    public static int p;
    [SerializeField] private GameObject endOfChoiseStage;

    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 7:

                readyPlayers++;
                if (readyPlayers == 2)
                {
                    PhotonNetwork.RaiseEvent(8, null, SF.StandertEventOptions, SF.StandatSendOptions);
                }
                break;

            case 8:
                endOfChoiseStage.SetActive(true);
                gameObject.SetActive(false);
                break;
        }
    }
    void Start()
    {
        if (p == 0)
        {
            if (LocalGameManager.isBlue)
            {
                foreach (GameObject obj in TextManager.textList)
                {
                    obj.transform.position = new Vector3(-4, -22, 0);
                }
            }
            else
            {
                foreach (GameObject obj in TextManager.textList)
                {
                    obj.transform.position = new Vector3(4, 22, 0);
                }
            }
            TextManager.activateThisText(TextManager.mark3ofCards);
            readyPlayers = 0;
            foreach (GameObject obj in GameManeger.myCards)
            {
                LocalGameManager.tmpGameObjects.Add(Instantiate(
                markPoint, new Vector3(obj.transform.position.x, obj.transform.position.y, -1), Quaternion.identity));
            }
            p = 1;
        }
    }

    public static void playerIsReady()
    {
        PhotonNetwork.RaiseEvent(7, null, SF.StandertEventOptions, SF.StandatSendOptions);
    }    
    

}
