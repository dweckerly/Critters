using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotes
{
    Exclamation,
    Angry,
    Happy
}

public class EmoteController : MonoBehaviour
{
    float emoteActiveTime = 0.75f;

    Dictionary<string, int> emotes = new Dictionary<string, int>
    {
        { "exclamation", 0 },
        { "anger", 1 },
        { "happy", 2 }
    };

    public void ShowEmote(Emotes emote)
    {
        //Transform emote = transform.GetChild(emotes[key]);
        Transform emoteT = transform.GetChild((int)emote);
        emoteT.gameObject.SetActive(true);
        StartCoroutine(EmoteReset(emoteT));
    }

    IEnumerator EmoteReset(Transform emoteT)
    {
        yield return new WaitForSeconds(emoteActiveTime);
        emoteT.gameObject.SetActive(false);
    }
}
