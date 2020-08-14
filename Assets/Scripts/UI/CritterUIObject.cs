using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CritterUIObject : MonoBehaviour
{
    public CritterUIManager critterUIManager;
    public Image critterSprite;
    public Text critterName;
    public Text hpText;
    public int index;

    public void SelectThisCritter()
    {
        critterUIManager.SetSelectedCritter(index);
    }
}
