using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public int NestID = -1;
    static public List<PlayerCharacterScript> Players = new List<PlayerCharacterScript>();
    public PlayerCharacterScript Myborb = null;
    public float Dunkrange = 1.0f;

    public void Awake()
    {
        if (NestID < 0)
        {
            Debug.Log("biep boep motherfucker! set my nest");
        }
        GameObject[] Pla = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in Pla)
        {
            Players.Add(player.GetComponent<PlayerCharacterScript>());
            if (player.ID == NestID)
            {
                Myborb = player.GetComponent<PlayerCharacterScript>();
            }
        }
    }

    public void CheckIfPlayerNear()
    {
        if (Vector3Util.RangeCheckFlat(Myborb.transform.position , transform.position , Dunkrange))
        {
            DropEgg();
        }
    }

    public void DropEgg()
    {
        Debug.Log("Chicken dinner");
    }
}
