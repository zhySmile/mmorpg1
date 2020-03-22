﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Data;
using Managers;
using System;
using Random = UnityEngine.Random;
using Models;

public class NpcController : MonoBehaviour {

    // Use this for initialization
    public int NpcID;
    SkinnedMeshRenderer renderer;
    Animator anim;
    Color orignColor;

    private bool inInteractive = false;
    NpcDefine npc;
    //NpcQuestStatus questStatus;
    void Start() {
        renderer = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        anim = this.gameObject.GetComponent<Animator>();
        orignColor = renderer.sharedMaterial.color;
        npc = NpcManager.Instance.GetNpcDefine(NpcID);
        NpcManager.Instance.UpdateNpcPosition(this.NpcID,this.transform.position);
        this.StartCoroutine(Actions());
        //RefreshNpcStatus();
        //QuestManager.Instance.onQuestStatusChanged += OnQuestStatusChanged;

    }
    //void OnQuestStatusChanged(Quest quest)
    //{
    //    this.RefreshNpcStatus();
    //}
    //void RefreshNpcStatus()
    //{
    //    questStatus = QuestManager.Instance.GetQuestStatusByNpc(this.NpcID);
    //    UIWorldElementManager.Instance.AddNpcQuestStatus(this.transform,questStatus);
    //}
    //private void OnDestroy()
    //{
    //    QuestManager.Instance.onQuestStatusChanged -= OnQuestStatusChanged;
    //    if (UIWorldElementManager.Instance!=null)
    //    {
    //        UIWorldElementManager.Instance.RemoveNpcQuestStatus(this.transform);
    //    }
    //}
    IEnumerator Actions()
    {
        while (true)
        {
            if (inInteractive)
            {
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(5f,10f));
            }
            this.Relax();
        }
    }

    private void Relax()
    {
        anim.SetTrigger("Relax");
    }
    void Interactive()
    {
        if (!inInteractive)
        {
            inInteractive = true;
            StartCoroutine(DoInteractive());
        }
    }
    IEnumerator DoInteractive()
    {
        yield return FaceToPlayer();
        if (NpcManager.Instance.Interactive(npc))
        {
            anim.SetTrigger("Talk");
        }
        yield return new WaitForSeconds(3f);
        inInteractive = false;
    }
    IEnumerator FaceToPlayer()
    {
        Vector3 faceTo = (User.Instance.CurrentCharacterObject.transform.position - this.transform.position).normalized;
        while (Mathf.Abs(Vector3.Angle(this.transform.forward,faceTo))>5)
        {
            this.transform.forward = Vector3.Lerp(this.transform.forward,faceTo,Time.deltaTime*5f);
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(this.transform.position,User.Instance.CurrentCharacterObject.transform.position)>2f)
        {
            //User.Instance.CurrentCharacterObject.StartNav(this.transform.position);
        }
        Interactive();
    }
    private void OnMouseOver()
    {
        Highlight(true);
    }
    private void OnMouseEnter()
    {
        Highlight(true);
    }
    private void OnMouseExit()
    {
        Highlight(false);
    }
    void Highlight(bool highlight)
    {
        if (highlight)
        {
            if (renderer.sharedMaterial.color!=Color.white)
            {
                renderer.sharedMaterial.color = Color.white;
            }
        }
        else
        {
            if (renderer.sharedMaterial.color != orignColor)
            {
                renderer.sharedMaterial.color = orignColor;
            }
        }
    }
}
