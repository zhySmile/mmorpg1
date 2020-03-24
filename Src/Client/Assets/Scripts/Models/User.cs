﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using UnityEngine;

namespace Models
{
    class User : Singleton<User>
    {
        public Action OnMoneyChanged;

        SkillBridge.Message.NUserInfo userInfo;


        public SkillBridge.Message.NUserInfo Info
        {
            get { return userInfo; }
        }


        public void SetupUserInfo(SkillBridge.Message.NUserInfo info)
        {
            this.userInfo = info;
        }


        public MapDefine CurrentMapData { get; set; }

        public SkillBridge.Message.NCharacterInfo CurrentCharacter { get; set; }

        public GameObject CurrentCharacterObject { get; set; }

        internal void AddGold(int gold)
        {
            this.CurrentCharacter.Gold += gold;
            if (OnMoneyChanged != null)
            {
                OnMoneyChanged();
            }
        }
    }
}