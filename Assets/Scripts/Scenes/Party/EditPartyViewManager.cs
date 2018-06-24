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
        partyView.UpdatePartyMemberHandler += OnUpdatePartyMember;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnOpen()
    {
        OnUpdatePartyMember();
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
                currentSelectePartyItemView.GetComponent<FocusMarkView>().SetFocus(false);

            currentSelectePartyItemView = itemView;
            currentSelectePartyItemView.GetComponent<FocusMarkView>().SetFocus(true);
        }
        else
        {
            currentSelectePartyItemView.GetComponent<FocusMarkView>().SetFocus(false);
            currentSelectePartyItemView = null;
        }
    }

    public void OnClickedMonsterListItem(MonsterListItemView itemView)
    {
        if (currentSelectedMonsterListItemView != itemView)
        {
            if(currentSelectedMonsterListItemView != null)
                currentSelectedMonsterListItemView.GetComponent<FocusMarkView>().SetFocus(false);

            currentSelectedMonsterListItemView = itemView;
            currentSelectedMonsterListItemView.GetComponent<FocusMarkView>().SetFocus(true);
            partyView.TryAddPartyMember(itemView);
        }
        else
        {
            currentSelectedMonsterListItemView.GetComponent<FocusMarkView>().SetFocus(false);
            currentSelectedMonsterListItemView = null;
        }
    }

    public void OnResetItem()
    {
        if (currentSelectedMonsterListItemView != null)
        {
            currentSelectedMonsterListItemView.GetComponent<FocusMarkView>().SetFocus(false);
            currentSelectedMonsterListItemView = null;
        }

        if (currentSelectePartyItemView != null)
        {
            currentSelectePartyItemView.GetComponent<FocusMarkView>().SetFocus(false);
            currentSelectePartyItemView = null;
        }
    }

    public void OnClickedRemoveButton()
    {
        partyView.TryRemovePartyMember();
    }

    public void OnUpdatePartyMember()
    {
        monsterListView.UpdateSelectedMember(partyView.GetCurrentPartyMembers());
    }
}
