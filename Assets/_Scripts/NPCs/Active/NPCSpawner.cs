using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Enemies;
using Unity.Mathematics;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private Transform[] coreWaypoints;
    


    private void Start()
    {
        SpawnNPC();
    }
    
   
    public void SpawnNPC()
    {
        GameObject instance = Instantiate(npc, transform.position, quaternion.identity);
        instance.GetComponent<Enemies.ActiveNPC>().waypoints = coreWaypoints;
    }
}
