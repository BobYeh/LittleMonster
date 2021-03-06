﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Monster
{
    public class MonsterListItemView : MonoBehaviour
    {
        public delegate void OnClickedMonsterListItemView(MonsterListItemView itemView);
        public OnClickedMonsterListItemView ClickedHandler;

        [SerializeField]
        MonsterIconView IconView;

        private MonsterEntity entity;

        [HideInInspector]
        public MonsterEntity Entity
        {
            get
            {
                return entity;
            }
        }
        
       public void UpdateItem(MonsterEntity entity)
        {
            this.entity = entity;

            if (entity != null)
            {
                IconView.UpdateView(entity.masterId);
            }
            else
            {
                IconView.gameObject.SetActive(false);
            }
        }

        public void OnClickedButton()
        {
            if(entity != null)
                MonsterDetailView.Instance.UpdateView(entity);

            if (ClickedHandler != null)
                ClickedHandler(this);
        }
    }
}
