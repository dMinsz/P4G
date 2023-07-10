using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public Transform[] GenratePos;
    public List<PlayerData> playerDatas;
    public CinemachineVirtualCamera cam;

    public void Init()
    {
        if (GameManager.Data.Dungeon.IsInit)
        {
            playerDatas.Add(GameManager.Resource.Load<PlayerData>("Datas/Players/Yu"));

            var temp = GameManager.Pool.Get(true, playerDatas[0].player.Prefab, GenratePos[0].position, Quaternion.identity);


            cam.Follow = temp.transform.Find("CamPos");

            temp.GetComponent<PlayerHiter>().Cam = cam; // 배틀씬 이동할때 카메라 조정을 위하여넣어준다.

            var player = temp.GetComponent<Player>();

            player.GetComponent<CharacterController>().enabled = false; // 캐릭터 위치에 이동

            player.transform.position = GenratePos[0].position;

            player.GetComponent<CharacterController>().enabled = true;

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

            GameManager.Data.Dungeon.InBattlePlayers.Add(player);

            for (int i = 0; i < player.Partys.Count; i++) 
            {
                var newAllyName = player.Partys[i].data.unitName;

                playerDatas.Add(GameManager.Resource.Load<PlayerData>("Datas/Players/" + newAllyName));

                var newAlly = GameManager.Pool.Get(true, playerDatas[i+1].player.Prefab, GenratePos[i+1].position, Quaternion.identity);


                newAlly.GetComponent<CharacterController>().enabled = false;

                newAlly.transform.position = GenratePos[i + 1].position;

                newAlly.GetComponent<CharacterController>().enabled = true;



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

                    player.GetComponent<PlayerHiter>().Cam = cam; // 배틀씬 이동할때 카메라 조정을 위하여넣어준다.

                }
                player.GetComponent<CharacterController>().enabled = false;

                if (GameManager.Data.Dungeon.didBattle) // 배틀을 하고왔으면
                {
                    player.transform.position = Players[i].transform.position;
                    player.transform.rotation = Players[i].transform.rotation; //포지션 유지
                }
                else 
                {
                    player.transform.position = GenratePos[i].transform.position;
                    player.transform.rotation = GenratePos[i].transform.rotation; //생성위치에 생성
                }

                player.GetComponent<CharacterController>().enabled = true;


            }

        }

    }
}
