using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EndOfChoiceStage : MonoBehaviour
{
    [SerializeField] private GameObject clickProtect;

    [SerializeField] private GameObject BlueRelay;
    [SerializeField] private GameObject BlueChecker;
    [SerializeField] private GameObject BlueTrap;
    [SerializeField] private GameObject BlueManaBoost;

    [SerializeField] private GameObject RedRelay;
    [SerializeField] private GameObject RedChecker;
    [SerializeField] private GameObject RedTrap;
    [SerializeField] private GameObject RedManaBoost;

    private void Start()
    {
        LocalGameManager.gameStarted = true;
        LocalGameManager.Board.SetActive(true);
        Destroy(clickProtect);

        if (LocalGameManager.isBlue)
        {
            GameManeger.myAbilityCards.Add(Instantiate(BlueRelay, new Vector3(3.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.myAbilityCards.Add(Instantiate(BlueChecker, new Vector3(6.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.myAbilityCards.Add(Instantiate(BlueTrap, new Vector3(9.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.myAbilityCards.Add(Instantiate(BlueManaBoost, new Vector3(12.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));

            GameManeger.enemyAbilityCards.Add(Instantiate(RedRelay, new Vector3(-3.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.enemyAbilityCards.Add(Instantiate(RedChecker, new Vector3(-6.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.enemyAbilityCards.Add(Instantiate(RedTrap, new Vector3(-9.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.enemyAbilityCards.Add(Instantiate(RedManaBoost, new Vector3(-12.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));

            TextManager.activateThisText(TextManager.yourTurn);
        }
        else
        {
            GameManeger.enemyAbilityCards.Add(Instantiate(BlueRelay, new Vector3(3.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.enemyAbilityCards.Add(Instantiate(BlueChecker, new Vector3(6.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.enemyAbilityCards.Add(Instantiate(BlueTrap, new Vector3(9.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.enemyAbilityCards.Add(Instantiate(BlueManaBoost, new Vector3(12.5f, -16.27f), Quaternion.Euler(-90, 0, 0)));

            GameManeger.myAbilityCards.Add(Instantiate(RedRelay, new Vector3(-3.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.myAbilityCards.Add(Instantiate(RedChecker, new Vector3(-6.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.myAbilityCards.Add(Instantiate(RedTrap, new Vector3(-9.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));
            GameManeger.myAbilityCards.Add(Instantiate(RedManaBoost, new Vector3(-12.5f, 16.27f), Quaternion.Euler(-90, 0, 0)));

            TextManager.activateThisText(TextManager.waitFor);
        }



    }

}
