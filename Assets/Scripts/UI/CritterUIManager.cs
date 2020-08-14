using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CritterUIManager : MonoBehaviour
{
    public PlayerInterface player;
    public PlayerTeam playerTeam;
    public GameObject critterPanel;

    public Transform critterViewParent;

    public Button accompanyButton;
    public Button giveItemButton;
    public Button releaseButton;

    public void Open()
    {
        if (critterPanel.activeSelf)
        {
            CloseCritterPanel();
        }
        else
        {
            OpenCritterPanel();
        }
    }

    void OpenCritterPanel()
    {

    }

    void CloseCritterPanel()
    {
        player.playerInput.mouse.EnableMouse();
        player.playerInput.EnablePlayerMovement();
        Cursor.lockState = CursorLockMode.Locked;
        critterPanel.SetActive(false);
    }

    void PopulatePlayerTeamView()
    {

    }
}
