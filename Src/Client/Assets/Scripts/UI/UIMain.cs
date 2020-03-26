using System;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class UIMain : MonoSingleton<UIMain>
{
    public Text avatarName;
    public Text avatarLevel;
    public Button BackToSelectCharButton;
    public Button BagButton;
    public Button CharEquipButton;
    public Button QuestButton;
    
    // Use this for initialization
    protected override void OnStart()
    {
        this.UpdateAvatar();
    }

    private void OnEnable()
    {
        BackToSelectCharButton.onClick.AddListener(BackToChaSelect);
        BagButton.onClick.AddListener(OnClickBag);
        CharEquipButton.onClick.AddListener(OnClickCharEquip);
        QuestButton.onClick.AddListener(OnClickQuest);
    }

    private void OnDisable()
    {
        BackToSelectCharButton.onClick.RemoveListener(BackToChaSelect);
        BagButton.onClick.RemoveListener(OnClickBag);
        CharEquipButton.onClick.RemoveListener(OnClickCharEquip);
        QuestButton.onClick.RemoveListener(OnClickQuest);
    }

    void UpdateAvatar()
    {
        this.avatarName.text = string.Format("{0}[{1}]", User.Instance.CurrentCharacterInfo.Name,
            User.Instance.CurrentCharacterInfo.Id);
        this.avatarLevel.text = User.Instance.CurrentCharacterInfo.Level.ToString();
    }

    public void BackToChaSelect()
    {
        SceneManager.Instance.LoadScene("CharSelect");
        Services.UserService.Instance.SendGameLeave();
    }

    private void OnClickBag()
    {
        UIManager.Instance.Show<UIBag>();
    }
    
    private void OnClickCharEquip()
    {
        UIManager.Instance.Show<UICharEquip>();
    }
    
    private void OnClickQuest()
    {
        UIManager.Instance.Show<UIQuestSystem>();
    }
}