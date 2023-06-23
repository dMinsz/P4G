using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public Transform[] GenratePos;
    public PlayerData playerData;
    public CinemachineVirtualCamera cam;

    private void Awake()
    {
    }

    private void Start()
    {

        playerData = GameManager.Resource.Load<PlayerData>("Datas/Players/Yu");

        for (int i = 0; i < GenratePos.Length; i++)
        {
            var temp = GameManager.Pool.Get(false,playerData.player.Prefab, GenratePos[i].position, Quaternion.identity);

            if (i == 0)
            {
                cam.Follow = temp.transform.Find("CamPos");
            }

            var player = temp.GetComponent<Player>();
            player.data = playerData.player;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
