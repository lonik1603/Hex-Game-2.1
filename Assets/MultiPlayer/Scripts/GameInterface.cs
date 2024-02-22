using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameInterface : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject darkBackgroundPref;
    [SerializeField] private GameObject pauseWinPref;

    [SerializeField] private GameObject endPanelPref;

    [SerializeField] private GameObject pauseButtonPpref;

    [SerializeField] private GameObject backToEndPanelButton;

    [SerializeField] private GameObject rulesChosePanel;

    [SerializeField] private GameObject rulesPanel;
    [SerializeField] private GameObject basisPanel;
    [SerializeField] private GameObject classesPanel;
    [SerializeField] private GameObject additinalPanel;

    [SerializeField] private GameObject victoryTXTpref;
    [SerializeField] private GameObject defeatTXTpref;


    private static GameObject victoryTXT;
    private static GameObject defeatTXT;

    private static GameObject endPanel;
    private static GameObject pauseButton;
    private static GameObject pauseWin;
    private static GameObject darkBackground;

    private static GameObject gameInterface;

    private void Start()
    {
        gameInterface = gameObject;
        pauseWin = pauseWinPref;
        pauseButton = pauseButtonPpref;
        darkBackground = darkBackgroundPref;
        endPanel = endPanelPref;

        victoryTXT = victoryTXTpref;
        defeatTXT = defeatTXTpref;

    }

    public void openClosePause()
    {
        if (darkBackground.activeInHierarchy)
        {
            pauseWin.SetActive(false);
            darkBackground.SetActive(false);


            rulesChosePanel.SetActive(false);
            rulesPanel.SetActive(false);

            basisPanel.SetActive(false);
            classesPanel.SetActive(false);
            additinalPanel.SetActive(false);
        }
        else
        {
            pauseWin.SetActive(true);
            darkBackground.SetActive(true);
        }
    }


    public static void Leave()
    {
        
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0); 
    }
    public static void newGame()
    {
        PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.RandomMatching, null, null, null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, null);
    }

    public static void showEndPanel(string status)
    {
        if (status == "victory")
        {
            victoryTXT.SetActive(true);
        }
        else if (status == "defeat")
        {
            defeatTXT.SetActive(true);
        }
        endPanel.SetActive(true);
        darkBackground.SetActive(true);
        LocalGameManager.canClick = false;
        pauseWin.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void showTheBoard()
    {
        darkBackground.SetActive(false);
        endPanel.SetActive(false);
        backToEndPanelButton.SetActive(true);
    }

    public void backToEndGamePanel()
    {
        endPanel.SetActive(true);
        darkBackground.SetActive(false);
        pauseWin.SetActive(false);
        backToEndPanelButton.SetActive(false);
    }


    public void toRulesButton()
    {
        pauseWin.SetActive(false);
        rulesChosePanel.SetActive(true);
    }

    public void backFromRulesToPause()
    {
        pauseWin.SetActive(true);
        rulesChosePanel.SetActive(false);
    }

    public void toBasis()
    {
        rulesChosePanel.SetActive(false);

        rulesPanel.SetActive(true);
        basisPanel.SetActive(true);
    }
    public void toClasses()
    {
        rulesChosePanel.SetActive(false);

        rulesPanel.SetActive(true);
        classesPanel.SetActive(true);
    }
    public void toAdditional()
    {
        rulesChosePanel.SetActive(false);

        rulesPanel.SetActive(true);
        additinalPanel.SetActive(true);
    }
    public void backToRulesChose()
    {
        rulesPanel.SetActive(false);

        basisPanel.SetActive(false);
        classesPanel.SetActive(false);
        additinalPanel.SetActive(false);

        rulesChosePanel.SetActive(true);
    }
}
