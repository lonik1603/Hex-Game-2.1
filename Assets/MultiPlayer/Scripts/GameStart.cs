using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameStart : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public Text nick1;
    public Text nick2;

    public GameObject gameCamera;

    public GameObject gameStart; 

    public GameObject textManager;

    private int masterIsBlue;

    public GameObject gfg;

    private void Start()
    {
        CardsChoiseStage.g = 0;
        MarksChoseStage.p = 0;


        if(PhotonNetwork.IsMasterClient)
        {
            Instantiate(gfg, new Vector3(-20,0,0), Quaternion.identity);           
            masterIsBlue = Random.Range(0, 2);
            if (masterIsBlue == 1)
            {
                LocalGameManager.isBlue = true;
                PhotonNetwork.RaiseEvent(1, null, SF.StandertEventOptions, SF.StandatSendOptions);
            }
            else if (masterIsBlue == 0)
            {
                LocalGameManager.isBlue = false;
                PhotonNetwork.RaiseEvent(2, null, SF.StandertEventOptions, SF.StandatSendOptions);
            }
        }
        if (LocalGameManager.isBlue)
        {
            nick1.text = PhotonNetwork.NickName;
            nick2.text = PhotonNetwork.PlayerListOthers[0].NickName;
        }
        else
        {
            nick2.text = PhotonNetwork.NickName;
            nick1.text = PhotonNetwork.PlayerListOthers[0].NickName;
        }
    }

    private void nextStage()
    {
        if (LocalGameManager.isBlue == false)
        {
            gameCamera.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {

        }
        textManager.SetActive(true);
        gameStart.SetActive(false);   
    }

    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {
        switch(photonEvent.Code)
        {
            case 1:
            if(PhotonNetwork.IsMasterClient == false)
            {
                LocalGameManager.isBlue = false;
            }
            nextStage();

            break;

            case 2:
            if(PhotonNetwork.IsMasterClient == false)
            {
                LocalGameManager.isBlue = true;
            }
            nextStage();

            break;
        }
    }
}
