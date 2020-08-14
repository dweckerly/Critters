using UnityEngine;
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
        critterDetails.critterATK.text = "";
        critterDetails.critterDEF.text = "";
        critterDetails.critterSATK.text = "";
        critterDetails.critterSDEF.text = "";
        critterDetails.critterSPD.text = "";
        critterDetails.critterHP.text = "";
        critterDetails.critterDescription.text = "";
        critterDetails.critterName.text = "";
        critterDetails.critterImage.sprite = null;
        critterDetails.critterLevel.text = "";
        critterDetails.critterXP.text = "";
    }

    void ShowSelectedCritterDetails()
    {
        critterDetails.critterATK.text = selectedCritter.ATK.value.ToString();
        critterDetails.critterDEF.text = selectedCritter.DEF.value.ToString();
        critterDetails.critterSATK.text = selectedCritter.SATK.value.ToString();
        critterDetails.critterSDEF.text = selectedCritter.SDEF.value.ToString();
        critterDetails.critterSPD.text = selectedCritter.SPD.value.ToString();
        critterDetails.critterHP.text = selectedCritter.HP.value.ToString();
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
