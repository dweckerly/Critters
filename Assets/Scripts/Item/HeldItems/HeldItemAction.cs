﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItemAction : MonoBehaviour
{
    public PlayerInterface player;
    public HeldItemsUIManager heldItemsUIManager;
    public List<Item> heldItems;
    int currentIndex;

    public float clickCoolDown;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        currentIndex = 0;
        clickCoolDown = 0.2f;
        for (int i = 0; i < heldItems.Count; i++)
        {
            heldItems[i].hia = this;
            if (i == currentIndex)
            {
                heldItems[i].gameObject.SetActive(true);
            }
            else
            {
                heldItems[i].gameObject.SetActive(false);
            }
        }
        heldItemsUIManager.PopulateHeldItemsPanel(heldItems);
        heldItemsUIManager.SetActiveItemDisplay(currentIndex);
    }

    void Update()
    {
        if (player.playerInput.mouse.LeftClick())
        {
            heldItems[currentIndex].LeftClickAction();
            player.playerInput.detectNumericInput = false;
            player.playerInput.mouse.detectLeftClick = false;
            player.playerInput.mouse.detectRightClick = false;
            StartCoroutine(ClickCoolDown());
        }

        if (player.playerInput.mouse.RightClick())
        {
            heldItems[currentIndex].RightClickAction();
        }

        if (player.playerInput.mouse.LeftClickRelease())
        {
            heldItems[currentIndex].UnClickAction();
        }

        if (heldItems.Count > 1)
        {
            float sw = player.playerInput.mouse.ScrollWheelInput();
            if (sw != 0)
            {
                SwapItem(sw);
            }
        }
        int numKey = player.playerInput.HeldItemKeyPressed();
        if(numKey != -1 && numKey < heldItems.Count && numKey != currentIndex)
        {
            SwapItem(0, numKey);
        }
    }

    public void SwapItem(float sw, int index = -1)
    {
        player.playerInput.detectNumericInput = false;
        player.playerInput.mouse.DisableMouseClickAndScroll();
        StartCoroutine(SwapOutHeldItem(sw, index));
    }

    IEnumerator SwapOutHeldItem(float sw, int index)
    {
        yield return new WaitForSeconds(heldItems[currentIndex].SwapOut());
        heldItems[currentIndex].gameObject.SetActive(false);
        if (index != -1)
        {
            currentIndex = index;  
        }
        else
        {
            if (sw > 0)
            {
                currentIndex++;
                currentIndex %= heldItems.Count;
            }
            else if (sw < 0)
            {
                currentIndex--;
                if (currentIndex == -1)
                {
                    currentIndex = heldItems.Count - 1;
                }
                else
                {
                    currentIndex %= heldItems.Count;
                }
            }
            if (currentIndex >= heldItems.Count)
            {
                currentIndex = heldItems.Count - 1;
            }
        }
        heldItems[currentIndex].gameObject.SetActive(true);
        heldItemsUIManager.SetActiveItemDisplay(currentIndex);
        StartCoroutine(SwapInHeldItem());
    }

    IEnumerator SwapInHeldItem()
    {
        player.playerInput.mouse.EnableMouseClickAndScroll();
        player.playerInput.detectNumericInput = true;
        yield return new WaitForSeconds(heldItems[currentIndex].SwapIn());
    }

    IEnumerator ClickCoolDown()
    {
        yield return new WaitForSeconds(clickCoolDown);
        player.playerInput.detectNumericInput = true;
        player.playerInput.mouse.detectLeftClick = true;
        player.playerInput.mouse.detectRightClick = true;
    }
}
