﻿using UnityEngine;
using UnityEngine.UI;

public class CritterUIManager : MonoBehaviour
{
    public PlayerInterface player;
    public PlayerTeam playerTeam;
    public GameObject critterPanel;

    public Transform critterViewParent;
    public CritterUIObject critterUIObject;
    public CritterDetailsUIObject critterDetails;

    public Button accompanyButton;
    public Button giveItemButton;
    public Button releaseButton;

    public CritterStats selectedCritter;

    Color mutationUIColor = new Color(255, 254, 141, 100);

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
        PopulatePlayerTeamView();
        critterPanel.SetActive(true);
        player.playerInput.mouse.DisableMouse();
        player.playerInput.DisablePlayerMovement();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseCritterPanel()
    {
        player.playerInput.mouse.EnableMouse();
        player.playerInput.EnablePlayerMovement();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        critterPanel.SetActive(false);
    }

    void PopulatePlayerTeamView()
    {
        ClearCritterView();
        for (int i = 0; i < playerTeam.critters.Count; i++)
        {
            critterUIObject.critterUIManager = this;
            critterUIObject.index = i;
            critterUIObject.critterSprite.sprite = playerTeam.critters[i].data.critterSprite;
            critterUIObject.critterName.text = playerTeam.critters[i].data.critterName;
            critterUIObject.hpText.text = playerTeam.critters[i].currentHP + " / " + playerTeam.critters[i].HP.value.ToString();
            Instantiate(critterUIObject, critterViewParent);
        }
        if (playerTeam.critters.Count > 0) 
        {
            if(selectedCritter == null)
            {
                SetSelectedCritter(0);
            }
            else
            {
                ShowSelectedCritterDetails();
            }
        } 
    }

    void ClearCritterView()
    {
        foreach (Transform child in critterViewParent)
        {
            Destroy(child.gameObject);
        }
        ClearCritterDetails();
    }

    void ClearCritterDetails()
    {
        critterDetails.critterATK.text = "";
        critterDetails.critterATKImages[0].color = Color.white;
        critterDetails.critterATKImages[1].color = Color.white;
        critterDetails.critterDEF.text = "";
        critterDetails.critterDEFImages[0].color = Color.white;
        critterDetails.critterDEFImages[1].color = Color.white;
        critterDetails.critterSATK.text = "";
        critterDetails.critterSATKImages[0].color = Color.white;
        critterDetails.critterSATKImages[1].color = Color.white;
        critterDetails.critterSDEF.text = "";
        critterDetails.critterSDEFImages[0].color = Color.white;
        critterDetails.critterSDEFImages[1].color = Color.white;
        critterDetails.critterSPD.text = "";
        critterDetails.critterSPDImages[0].color = Color.white;
        critterDetails.critterSPDImages[1].color = Color.white;
        critterDetails.critterHP.text = "";
        critterDetails.critterHPImages[0].color = Color.white;
        critterDetails.critterHPImages[1].color = Color.white;
        critterDetails.critterDescription.text = "";
        critterDetails.critterName.text = "";
        critterDetails.critterImage.sprite = null;
        critterDetails.critterLevel.text = "";
        critterDetails.critterXP.text = "";
    }

    void ShowSelectedCritterDetails()
    {
        ClearCritterDetails();
        critterDetails.critterATK.text = selectedCritter.ATK.value.ToString();
        if(selectedCritter.ATK.mutation > 1)
        {
            critterDetails.critterATKImages[0].color = mutationUIColor;
            critterDetails.critterATKImages[1].color = mutationUIColor;
        }
        critterDetails.critterDEF.text = selectedCritter.DEF.value.ToString();
        if (selectedCritter.DEF.mutation > 1)
        {
            critterDetails.critterDEFImages[0].color = mutationUIColor;
            critterDetails.critterDEFImages[1].color = mutationUIColor;
        }
        critterDetails.critterSATK.text = selectedCritter.SATK.value.ToString();
        if (selectedCritter.SATK.mutation > 1)
        {
            critterDetails.critterSATKImages[0].color = mutationUIColor;
            critterDetails.critterSATKImages[1].color = mutationUIColor;
        }
        critterDetails.critterSDEF.text = selectedCritter.SDEF.value.ToString();
        if (selectedCritter.SDEF.mutation > 1)
        {
            critterDetails.critterSDEFImages[0].color = mutationUIColor;
            critterDetails.critterSDEFImages[1].color = mutationUIColor;
        }
        critterDetails.critterSPD.text = selectedCritter.SPD.value.ToString();
        if (selectedCritter.SPD.mutation > 1)
        {
            critterDetails.critterSPDImages[0].color = mutationUIColor;
            critterDetails.critterSPDImages[1].color = mutationUIColor;
        }
        critterDetails.critterHP.text = selectedCritter.HP.value.ToString();
        if (selectedCritter.HP.mutation > 1)
        {
            critterDetails.critterHPImages[0].color = mutationUIColor;
            critterDetails.critterHPImages[1].color = mutationUIColor;
        }
        critterDetails.critterDescription.text = selectedCritter.data.description;
        critterDetails.critterName.text = selectedCritter.data.critterName;
        critterDetails.critterImage.sprite = selectedCritter.data.critterSprite;
        critterDetails.critterLevel.text = "Level : " + selectedCritter.level;
        critterDetails.critterXP.text = "XP : " + selectedCritter.XP.ToString() + " / " + selectedCritter.nextLevelXP.ToString();
    }

    public void SetSelectedCritter(int index)
    {
        selectedCritter = playerTeam.critters[index];
        ShowSelectedCritterDetails();
    }
}
