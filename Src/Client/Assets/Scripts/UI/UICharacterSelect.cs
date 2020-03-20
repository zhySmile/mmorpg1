using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using Services;
using SkillBridge.Message;

public class UICharacterSelect : MonoBehaviour
{
    public GameObject panelCreate;
    public GameObject panelSelect;

    public GameObject btnCreateCancel;

    public InputField charName;
    CharacterClass charClass;

    public Transform uiCharList;
    public GameObject uiCharInfo;

    public List<GameObject> uiChars = new List<GameObject>();

    public Image[] titles;

    public Text descs;


    public Text[] names;

    private int selectCharacterIdx = -1;

    public UICharacterView characterView;
    public Button createButton;
    public Button cancelButton;
    public Button startCreateButton;
    public Button enterGameButton;

    // Use this for initialization
    void Start()
    {
        InitCharacterSelect(true);
        //InitCharacterCreate();
        UserService.Instance.OnCharacterCreate = OnCharacterCreate;
    }

    private void OnEnable()
    {
        createButton.onClick.AddListener(OnClickCreate);
        cancelButton.onClick.AddListener(OnClickCancel);
        startCreateButton.onClick.AddListener(OnClickStartCreate);
        enterGameButton.onClick.AddListener(OnClickPlay);
    }

    private void OnDisable()
    {
        createButton.onClick.RemoveListener(OnClickCreate);
        cancelButton.onClick.RemoveListener(OnClickCancel);
        startCreateButton.onClick.RemoveListener(OnClickStartCreate);
        enterGameButton.onClick.RemoveListener(OnClickPlay);
    }

    public void InitCharacterCreate()
    {
        ActivePanelSelect(false);
        OnSelectClass(1);
    }

    public void OnClickStartCreate()
    {
        InitCharacterCreate();
    }

    public void OnClickCreate()
    {
        if (string.IsNullOrEmpty(this.charName.text))
        {
            MessageBox.Show("请输入角色名称");
            return;
        }

        UserService.Instance.SendCharacterCreate(this.charName.text, this.charClass);
    }

    public void OnClickCancel()
    {
        InitCharacterSelect(true);
    }

    private void ActivePanelSelect(bool isActive)
    {
        panelSelect.SetActive(isActive);
        panelCreate.SetActive(!isActive);
    }

    /// <summary>
    /// 选择职业
    /// </summary>
    /// <param name="charClass"></param>
    public void OnSelectClass(int charClass)
    {
        this.charClass = (CharacterClass) charClass;

        characterView.CurrectCharacter = charClass - 1;

        for (int i = 0; i < 3; i++)
        {
            titles[i].gameObject.SetActive(i == charClass - 1);
            names[i].text = DataManager.Instance.Characters[i + 1].Name;
        }

        descs.text = DataManager.Instance.Characters[charClass].Description;
    }


    void OnCharacterCreate(Result result, string message)
    {
        if (result == Result.Success)
        {
            InitCharacterSelect(true);
        }
        else
            MessageBox.Show(message, "错误", MessageBoxType.Error);
    }


    public void InitCharacterSelect(bool init)
    {
        ActivePanelSelect(true);

        if (init)
        {
            foreach (var old in uiChars)
            {
                Destroy(old);
            }

            uiChars.Clear();
            Debug.Log(User.Instance.Info.Player.Characters.Count + " " +
                      Models.User.Instance.Info.Player.Characters.Count);
            for (int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
            {
                GameObject go = Instantiate(uiCharInfo, this.uiCharList);
                UICharInfo chrInfo = go.GetComponent<UICharInfo>();
                chrInfo.info = User.Instance.Info.Player.Characters[i];

                Button button = go.GetComponent<Button>();
                int idx = i;
                button.onClick.AddListener(() => { OnSelectCharacter(idx); });

                uiChars.Add(go);
                go.SetActive(true);
            }
        }
    }


    public void OnSelectCharacter(int idx)
    {
        this.selectCharacterIdx = idx;
        var cha = User.Instance.Info.Player.Characters[idx];
        Debug.LogFormat("Select Char:[{0}]{1}[{2}]", cha.Id, cha.Name, cha.Class);
        characterView.CurrectCharacter = ((int) cha.Class - 1);

        for (int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
        {
            UICharInfo ci = this.uiChars[i].GetComponent<UICharInfo>();
            ci.Selected = idx == i;
        }
    }

    public void OnClickPlay()
    {
        if (selectCharacterIdx >= 0)
        {
            MessageBox.Show("进入游戏", "确定");
            UserService.Instance.SendGameEnter(selectCharacterIdx);
        }
    }
}