using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static PersonaData;

public class BattleUIHandler : MonoBehaviour
{

    public BattleUI BattleUI;
    public Transform partyUI;
    public Transform MenuUI;
    public Transform MenuContentUI;
    [HideInInspector] public BattleSystem battleSystem;

    private GameObject skillObj;
    private List<GameObject> NowObejcts = new List<GameObject>();
    public void SetUpUI(BattleSystem _battlesystem)
    {
        _battlesystem.uiHandler = this;
        this.battleSystem = _battlesystem;
        MenuUI.gameObject.SetActive(false);

        skillObj = GameManager.Resource.Load<GameObject>("UI/SkillItem");

        foreach (var player in battleSystem.InBattlePlayers)
        {

            //PartyUI Set
            var obj = GameManager.Resource.Load<GameObject>("UI/BattlePartyElement");
            var newUI = GameManager.Pool.GetUI(obj, partyUI);

            newUI.transform.Find("Character").GetComponent<Image>().sprite = player.data.battleImage;
            newUI.transform.Find("Status").Find("SPSlider").GetComponent<SliderBar>().player = player;
            newUI.transform.Find("Status").Find("HPSlider").GetComponent<SliderBar>().player = player;

            newUI.transform.Find("SPText").GetComponent<BarText>().player = player;
            newUI.transform.Find("HPText").GetComponent<BarText>().player = player;


            //Skill Set

            //Player Skill
            foreach (var skill in player.data.skills)
            {
                //var skillobj = GameManager.Resource.Load<GameObject>("UI/SkillItem");
                var newSkillUI = GameManager.Pool.GetUI(skillObj, MenuContentUI, skill.skill.skillName + player.data.unitName);

                var item = newSkillUI.GetComponent<SkillItem>();
                item.Icon.sprite = skill.skill.skillImage;
                item.skillName.text = skill.skill.skillName;
                item.Cost.text = skill.skill.cost.ToString();
                item.CostType.text = skill.skill.cType.ToString();


                item.OnClick.AddListener(this.battleSystem.OnPlayerAttack);

                GameManager.Pool.ReleaseUI(newSkillUI);
            }

            //Persona skill
            foreach (var persona in player.Personas)
            {

                foreach (var pskill in persona.data.skills)
                {
                    //var skillobj = GameManager.Resource.Load<GameObject>("UI/SkillItem");
                    var newPersonaSkillUI = GameManager.Pool.GetUI(skillObj, MenuContentUI, pskill.skill.skillName + persona.data.personaName);

                    var item = newPersonaSkillUI.GetComponent<SkillItem>();
                    item.Icon.sprite = pskill.skill.skillImage;
                    item.skillName.text = pskill.skill.skillName;
                    item.Cost.text = pskill.skill.cost.ToString();
                    item.CostType.text = pskill.skill.cType.ToString();

                    //persona.OnAttack = new UnityEvent();

                    //공격과 스킬사용 애니메이션 바꿈
                    if (pskill.skill.SkillType == ResType.Physic)
                    {

                        item.attackType = BattleSystem.PersonaAttackType.Attack;
                    }
                    else
                    {
                        item.attackType = BattleSystem.PersonaAttackType.Skill;
                    }


                    //Battle Content Item Add Func
                    item.OnClick.AddListener(this.battleSystem.OnPlayerUsePersonaAttack);

                    GameManager.Pool.ReleaseUI(newPersonaSkillUI);
                }
            }
        }
        //GameManager.Resource.Destroy(skillObj);
    }


    public bool IsOpenPlayerMenu = false;
    public bool IsOpenPersonaMenu = false;
    public void OpenPlayerSkill()
    {
        if (MenuUI.gameObject.activeSelf == false)
        {
            IsOpenPlayerMenu = true;

            if (NowObejcts.Count <= 0)
            {
                foreach (var skill in battleSystem.nowPlayer.data.skills)
                {
                    var obj = GameManager.Pool.GetUI(skillObj, MenuContentUI, skill.skill.skillName + battleSystem.nowPlayer.data.unitName);
                    NowObejcts.Add(obj);
                }

                MenuUI.gameObject.SetActive(true);
            }
            else
            {
                foreach (var obj in NowObejcts)
                {
                    if (GameManager.Pool.IsContain(obj))
                    {
                        GameManager.Pool.Release(obj);
                    }
                }
                NowObejcts.Clear();

                foreach (var skill in battleSystem.nowPlayer.data.skills)
                {
                    var obj = GameManager.Pool.GetUI(skillObj, MenuContentUI, skill.skill.skillName + battleSystem.nowPlayer.data.unitName);
                    NowObejcts.Add(obj);
                }

                MenuUI.gameObject.SetActive(true);
            }
        }
        else if (MenuUI.gameObject.activeSelf == true && IsOpenPersonaMenu == true) 
        {
            IsOpenPersonaMenu = false;

            foreach (var obj in NowObejcts)
            {
                if (GameManager.Pool.IsContain(obj))
                {
                    GameManager.Pool.Release(obj);
                }
            }
            NowObejcts.Clear();

            foreach (var skill in battleSystem.nowPlayer.data.skills)
            {
                var obj = GameManager.Pool.GetUI(skillObj, MenuContentUI, skill.skill.skillName + battleSystem.nowPlayer.data.unitName);
                NowObejcts.Add(obj);
            }

            MenuUI.gameObject.SetActive(true);
        }
        else
        {
            IsOpenPlayerMenu = false;
            CloseMenu();
        }

       

    }

    public void OpenPersonaSkill()
    {
        if (MenuUI.gameObject.activeSelf == false)
        {
            IsOpenPersonaMenu = true;
            if (NowObejcts.Count <= 0)
            {
                var nowPersona = battleSystem.nowPlayer.Personas[battleSystem.nowPlayer.nowPersonaIndex];

                foreach (var skill in nowPersona.data.skills)
                {
                    var obj = GameManager.Pool.GetUI(skillObj, MenuContentUI, skill.skill.skillName +
                                                    nowPersona.data.personaName);
                    NowObejcts.Add(obj);
                }

                MenuUI.gameObject.SetActive(true);
            }
            else
            {
                foreach (var obj in NowObejcts)
                {
                    if (GameManager.Pool.IsContain(obj))
                    {
                        GameManager.Pool.Release(obj);
                    }
                }
                NowObejcts.Clear();

                var nowPersona = battleSystem.nowPlayer.Personas[battleSystem.nowPlayer.nowPersonaIndex];

                foreach (var skill in nowPersona.data.skills)
                {
                    var obj = GameManager.Pool.GetUI(skillObj, MenuContentUI, skill.skill.skillName +
                                                        nowPersona.data.personaName);
                    NowObejcts.Add(obj);
                }

                MenuUI.gameObject.SetActive(true);
            }
        }
        else if (MenuUI.gameObject.activeSelf == true && IsOpenPlayerMenu == true)
        {
            IsOpenPlayerMenu = false;
            foreach (var obj in NowObejcts)
            {
                if (GameManager.Pool.IsContain(obj))
                {
                    GameManager.Pool.Release(obj);
                }
            }
            NowObejcts.Clear();

            var nowPersona = battleSystem.nowPlayer.Personas[battleSystem.nowPlayer.nowPersonaIndex];

            foreach (var skill in nowPersona.data.skills)
            {
                var obj = GameManager.Pool.GetUI(skillObj, MenuContentUI, skill.skill.skillName +
                                                    nowPersona.data.personaName);
                NowObejcts.Add(obj);
            }

            MenuUI.gameObject.SetActive(true);
        }
        else 
        {
            IsOpenPersonaMenu = false;
            CloseMenu();
        }
        
    }


    public void CloseMenu()
    {
        MenuUI.gameObject.SetActive(false);
    }

}
