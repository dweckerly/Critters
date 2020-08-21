using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBillboard : MonoBehaviour
{
    public Camera playerCamera;
    public float rotateSpeed = 0.1f;
    private List<Transform> envTrans = new List<Transform>();
    private List<Renderer> envRenderers = new List<Renderer>();

    private void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Billboard");
        GameObject[] goc = GameObject.FindGameObjectsWithTag("Critter");
        for (int i = 0; i < gos.Length; i++)
        { 
            envTrans.Add(gos[i].transform);
        }
        for (int i = 0; i < goc.Length; i++)
        {
            envTrans.Add(goc[i].transform);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < envTrans.Count; i++)
        {
            if(envTrans[i] != null)
            {
                Vector3 v = playerCamera.transform.position - envTrans[i].position;
                v.x = v.z = 0.0f;
                envTrans[i].LookAt(Vector3.Lerp(envTrans[i].position, 2 * envTrans[i].position - (playerCamera.transform.position - v), rotateSpeed * Time.deltaTime));
            }
            else
            {
                envTrans.Remove(envTrans[i]);
            }
        }
    }
}
