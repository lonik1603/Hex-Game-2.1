using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EndOfChoiceStage : MonoBehaviour
{
    [SerializeField] private GameObject clickProtect;

    private void Start()
    {
        LocalGameManager.gameStarted = true;
        LocalGameManager.Board.SetActive(true);
        Destroy(clickProtect);

        if(LocalGameManager.isBlue)
        {
            PhotonNetwork.Instantiate("BlueRelay", new Vector3(3.5f, -16.27f), Quaternion.Euler(-90, 0, 0));
            PhotonNetwork.Instantiate("BlueChecker", new Vector3(6.5f, -16.27f), Quaternion.Euler(-90, 0, 0));
            PhotonNetwork.Instantiate("BlueTrap", new Vector3(9.5f, -16.27f), Quaternion.Euler(-90, 0, 0));
        }
        else
        {
            PhotonNetwork.Instantiate("RedRelay", new Vector3(-3.5f, 16.27f), Quaternion.Euler(-90, 0, 0));
            PhotonNetwork.Instantiate("RedChecker", new Vector3(-6.5f, 16.27f), Quaternion.Euler(-90, 0, 0));
            PhotonNetwork.Instantiate("RedTrap", new Vector3(-9.5f, 16.27f), Quaternion.Euler(-90, 0, 0));
        }



    }

}
