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

    // Use this for initialization
    protected override void OnStart()
    {
        this.UpdateAvatar();
    }

    private void OnEnable()
    {
        BackToSelectCharButton.onClick.AddListener(BackToChaSelect);
        BagButton.onClick.AddListener(OnClickBag);
    }

    private void OnDisable()
    {
        BackToSelectCharButton.onClick.RemoveListener(BackToChaSelect);
        BagButton.onClick.AddListener(OnClickBag);
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

    private void OnClickBag()
    {
        UIManager.Instance.Show<UIBag>();
    }
}