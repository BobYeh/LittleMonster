using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Monster;

public class EditPartyViewManager : MonoBehaviour
{
    [SerializeField]
    PartyView partyView;
    [SerializeField]
    MonsterListView monsterListView;

    MonsterListItemView currentSelectedItemView;

    // Use this for initialization
    void Start()
    {
        monsterListView.ItemSelectedHandler += OnClickedMonsterListItem;
        partyView.ItemSelectedHandler += OnClickedPartyItem;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickedPartyItem(MonsterListItemView itemView)
    {
        if (currentSelectedItemView != itemView)
        {
            currentSelectedItemView = itemView;
        }
        else
        {
            currentSelectedItemView = null;
        }
    }

    public void OnClickedMonsterListItem(MonsterListItemView itemView)
    {
        if (currentSelectedItemView != itemView)
        {
            currentSelectedItemView = itemView;
            partyView.TryAddPartyMember(itemView);
        }
        else
        {
            currentSelectedItemView = null;
        }
    }

    public void OnClickedRemoveButton()
    {
        partyView.TryRemovePartyMember();
    }
}
