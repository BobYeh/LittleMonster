using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Utils;

namespace Assets.Scripts.Monster
{
    public class MonsterDetailView : SingletonMonoBehaviour<MonsterDetailView>
    {
        [SerializeField]
        Image icon;
        [SerializeField]
        Text nickName;
        [SerializeField]
        Text hp;
        [SerializeField]
        Text attack;
        [SerializeField]
        Text defence;
        [SerializeField]
        Text dodge;
        [SerializeField]
        Text critical;
        [SerializeField]
        Animator animator;

        private MonsterEntity currentEntity;

        public void UpdateView(MonsterEntity entity)
        {
            if (entity == null || entity == currentEntity)
                return;

            currentEntity = entity;

            ResourceUtilities.Instance.LoadMonsterIcon(entity.masterId, (sprite)=> {
                icon.sprite = sprite;
                PlayIconAppearAnimation();
            });

            nickName.text = entity.nickname;
            hp.text = entity.hp.ToString();
            attack.text = entity.attack.ToString();
            defence.text = entity.defence.ToString();
            dodge.text = entity.dodge.ToString();
            critical.text = entity.critical.ToString();
        }

        public void PlayIconAppearAnimation()
        {
            animator.Play("IconAppearAnimation", 0, 0);
        }
    }
}
