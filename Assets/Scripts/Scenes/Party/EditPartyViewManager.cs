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

    MonsterListItemView currentSelectedMonsterListItemView;
    PartyItemView currentSelectePartyItemView;

    // Use this for initialization
    void Start()
    {
        monsterListView.ItemSelectedHandler += OnClickedMonsterListItem;
        partyView.ItemSelectedHandler += OnClickedPartyItem;
        partyView.ResetItemHandler += OnResetItem;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickedPartyItem(PartyItemView itemView)
    {
        if (currentSelectedMonsterListItemView != null)
        {
            partyView.TryAddPartyMember(currentSelectedMonsterListItemView);
        }
        else if (currentSelectePartyItemView != itemView)
        {
            if(currentSelectePartyItemView != null)
                currentSelectePartyItemView.GetComponent<SelectedItemView>().SetSelected(false);

            currentSelectePartyItemView = itemView;
            currentSelectePartyItemView.GetComponent<SelectedItemView>().SetSelected(true);
        }
        else
        {
            currentSelectePartyItemView.GetComponent<SelectedItemView>().SetSelected(false);
            currentSelectePartyItemView = null;
        }
    }

    public void OnClickedMonsterListItem(MonsterListItemView itemView)
    {
        if (currentSelectedMonsterListItemView != itemView)
        {
            if(currentSelectedMonsterListItemView != null)
                currentSelectedMonsterListItemView.GetComponent<SelectedItemView>().SetSelected(false);

            currentSelectedMonsterListItemView = itemView;
            currentSelectedMonsterListItemView.GetComponent<SelectedItemView>().SetSelected(true);
            partyView.TryAddPartyMember(itemView);
        }
        else
        {
            currentSelectedMonsterListItemView.GetComponent<SelectedItemView>().SetSelected(false);
            currentSelectedMonsterListItemView = null;
        }
    }

    public void OnResetItem()
    {
        if (currentSelectedMonsterListItemView != null)
        {
            currentSelectedMonsterListItemView.GetComponent<SelectedItemView>().SetSelected(false);
            currentSelectedMonsterListItemView = null;
        }

        if (currentSelectePartyItemView != null)
        {
            currentSelectePartyItemView.GetComponent<SelectedItemView>().SetSelected(false);
            currentSelectePartyItemView = null;
        }
    }

    public void OnClickedRemoveButton()
    {
        partyView.TryRemovePartyMember();
    }
}
