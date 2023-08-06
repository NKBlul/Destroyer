using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform prefabLoc;

    private GameObject spawnEnemy;

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            SpawnEnemy();
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        spawnEnemy = PhotonNetwork.Instantiate(enemyPrefab.name, prefabLoc.position, Quaternion.identity);
    }
}
