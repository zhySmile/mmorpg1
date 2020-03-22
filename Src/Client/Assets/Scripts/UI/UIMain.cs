using System;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoSingleton<UIMain>
{
    public Text avatarName;
    public Text avatarLevel;
    public Button BackToSelectCharButton;

    // Use this for initialization
    protected override void OnStart()
    {
        this.UpdateAvatar();
    }

    private void OnEnable()
    {
        BackToSelectCharButton.onClick.AddListener(BackToChaSelect);
    }

    private void OnDisable()
    {
        BackToSelectCharButton.onClick.RemoveListener(BackToChaSelect);
    }

    void UpdateAvatar()
    {
        this.avatarName.text = string.Format("{0}[{1}]", User.Instance.CurrentCharacter.Name,
            User.Instance.CurrentCharacter.Id);
        this.avatarLevel.text = User.Instance.CurrentCharacter.Level.ToString();
    }

    public void BackToChaSelect()
    {
        SceneManager.Instance.LoadScene("CharSelect");
        Services.UserService.Instance.SendGameLeave();
    }
}