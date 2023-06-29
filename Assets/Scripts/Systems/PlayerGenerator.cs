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
            //playerDatas.Add(GameManager.Resource.Load<PlayerData>("Datas/Players/Chie"));

            var temp = GameManager.Pool.Get(true, playerDatas[0].player.Prefab, GenratePos[0].position, Quaternion.identity);


            cam.Follow = temp.transform.Find("CamPos");

            var player = temp.GetComponent<Player>();
            player.data = playerDatas[0].player;

            player.MaxHp = playerDatas[0].player.Hp;
            player.MaxSp = playerDatas[0].player.Sp;
            player.curHp = playerDatas[0].player.Hp;
            player.curSp = playerDatas[0].player.Sp;


            //페르소나 추가
            for (int i = 0; i < playerDatas[0].PersonaNames.Count; i++)
            {
                var data = GameManager.Resource.Load<PersonaData>("Datas/Personas/" + playerDatas[0].PersonaNames[i]);
                player.Personas.Add(data.PData.prefabs.GetComponent<BattlePersona>());
                player.Personas[i].data = data.PData;
            }

            
            //party 추가
            player.AddParty("Chie");
            player.AddParty("Kanji");

            GameManager.Data.Dungeon.InBattlePlayers.Add(player);

            for (int i = 0; i < player.Partys.Count; i++) 
            {
                var newAllyName = player.Partys[i].data.unitName;

                playerDatas.Add(GameManager.Resource.Load<PlayerData>("Datas/Players/" + newAllyName));

                var newAlly = GameManager.Pool.Get(true, playerDatas[i+1].player.Prefab, GenratePos[i+1].position, Quaternion.identity);

                newAlly.GetComponent<AllyMover>().ChacePos = player.GetComponent<PlayerMover>().ChacePoitns[i];

                var Ally = newAlly.GetComponent<Player>();

                Ally.data = playerDatas[i + 1].player;

                Ally.MaxHp = playerDatas[i+1].player.Hp;
                Ally.MaxSp = playerDatas[i+1].player.Sp;
                Ally.curHp = playerDatas[i+1].player.Hp;
                Ally.curSp = playerDatas[i+1].player.Sp;

                //페르소나 추가
                for (int j = 0; j < playerDatas[i+1].PersonaNames.Count; j++)
                {
                    var data = GameManager.Resource.Load<PersonaData>("Datas/Personas/" + playerDatas[i + 1].PersonaNames[j]);
                    Ally.Personas.Add(data.PData.prefabs.GetComponent<BattlePersona>());
                    Ally.Personas[j].data = data.PData;
                }



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
