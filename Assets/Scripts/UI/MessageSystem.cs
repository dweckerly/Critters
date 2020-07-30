using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour
{
    public GameObject message;
    Queue<GameObject> messageQueue = new Queue<GameObject>();
    int maxMessages = 5;

    public void AddMessage(string m)
    {
        if (messageQueue.Count >= maxMessages)
        {
            Destroy(messageQueue.Dequeue());
        }
        Text text = message.GetComponentInChildren<Text>();
        text.text = m;
        messageQueue.Enqueue(Instantiate(message, transform));
        messageQueue.LastOrDefault().GetComponent<RectTransform>().SetSiblingIndex(0);
    }
}
