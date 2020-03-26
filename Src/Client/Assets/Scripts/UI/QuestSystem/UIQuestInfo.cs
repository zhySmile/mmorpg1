﻿using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;
public class UIQuestInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public Text title;
    public Text[] targets;
    public Text description;

    public Text overview;

    public UIIconItem rewardItems;

    public Text rewardMoney;
    public Text rewardExp;

    public Button navButton;
    private int npc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetQuestInfo(Quest quest)
    {
        this.title.text = string.Format("[{0}] {1}",quest.Define.Type,quest.Define.Name);
        if (this.overview != null) this.overview.text = quest.Define.Overview;


        if (this.description!=null)
        {
            if (quest.Info == null)
            {
                this.description.text = quest.Define.Dialog;
            }
            else
            {
                if (quest.Info.Status == SkillBridge.Message.QuestStatus.Complated)
                {
                    this.description.text = quest.Define.DialogFinish;
                }
            }
        }
       
        this.rewardMoney.text = quest.Define.RewardGold.ToString();
        this.rewardExp.text = quest.Define.RewardExp.ToString();

        if (quest.Info==null)
        {
            this.npc = quest.Define.AcceptNPC;
        }
        else if(quest.Info.Status==SkillBridge.Message.QuestStatus.Complated)
        {
            this.npc = quest.Define.SubmitNPC;
        }
        if(navButton!=null) this.navButton.gameObject.SetActive(this.npc>0);
        foreach (var fitter in this.GetComponentsInChildren<ContentSizeFitter>())
        {
            fitter.SetLayoutVertical();
        }
    }

    public void OnClickAbandon()
    {

    }

    public void OnClickNav()
    {
        Vector3 pos = NpcManager.Instance.GetNpcPosition(this.npc);
//        User.Instance.CurrentCharacterObject.StartNav(pos);
        UIManager.Instance.Close<UIQuestSystem>();
    }
}
