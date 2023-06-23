using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerGenerator : MonoBehaviour
{
    public Transform[] GenratePos;
    public List<PlayerData> playerDatas;
    public CinemachineVirtualCamera cam;

    private void Start()
    {
        if (GameManager.Data.Dungeon.IsInit)
        {
            playerDatas.Add(GameManager.Resource.Load<PlayerData>("Datas/Players/Yu"));


            var temp = GameManager.Pool.Get(true, playerDatas[0].player.Prefab, GenratePos[0].position, Quaternion.identity);


            cam.Follow = temp.transform.Find("CamPos");

            var player = temp.GetComponent<Player>();
            player.data = playerDatas[0].player;

            player.MaxHp = playerDatas[0].player.Hp;
            player.MaxSp = playerDatas[0].player.Sp;
            player.curHp = playerDatas[0].player.Hp;
            player.curSp = playerDatas[0].player.Sp;

            GameManager.Data.Dungeon.InBattlePlayers.Add(player);

            for (int i = 0; i < player.Partys.Count; i++)
            {
                var newAllyName = player.Partys[i].data.unitName;

                playerDatas.Add(GameManager.Resource.Load<PlayerData>("Datas/Players/" + newAllyName));

                var newAlly = GameManager.Pool.Get(true, playerDatas[++i].player.Prefab, GenratePos[++i].position, Quaternion.identity);
                var Ally = newAlly.GetComponent<Player>();

                Ally.data = playerDatas[++i].player;

                Ally.MaxHp = playerDatas[++i].player.Hp;
                Ally.MaxSp = playerDatas[++i].player.Sp;
                Ally.curHp = playerDatas[++i].player.Hp;
                Ally.curSp = playerDatas[++i].player.Sp;

                GameManager.Data.Dungeon.InBattlePlayers.Add(Ally);

            }
        }
        else 
        {
            var Players = GameManager.Data.Dungeon.InBattlePlayers;

            for (int i = 0; i < Players.Count; i++)
            {
                var player = GameManager.Pool.Get(true, Players[i], Players[i].transform.position,
                                                    Players[i].transform.rotation);
                if (i == 0)
                {
                    cam.Follow = player.transform.Find("CamPos");
                }
              
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
