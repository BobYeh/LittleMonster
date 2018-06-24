using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Monster;
using System;

public class PartyView : MonoBehaviour
{
    public delegate void OnPartyViewItemSelected(PartyItemView view);
    public OnPartyViewItemSelected ItemSelectedHandler;

    public Action ResetItemHandler;
    public Action UpdatePartyMemberHandler;

    private PartyEntity partyEntity;

    Dictionary<int, MonsterListItemView> items = new Dictionary<int, MonsterListItemView>();

    //-1 as nothing is selected
    int currentSelectedPartyItemPosition = -1;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < GameDefineData.NUMBER_OF_PARTY_MEMBER; i++)
        {
            var itemView = GenerateMonsterListItemView();
            itemView.gameObject.AddComponent<PartyItemView>().Position = i;
            itemView.ClickedHandler += OnClickedPartyItem;
            items.Add(i , itemView);
        }
    }

    public MonsterListItemView GenerateMonsterListItemView()
    {
        var prefab = Instantiate(Resources.Load<GameObject>(PrefabPath.MONSTER_LIST_ITEM_VIEW));
        prefab.transform.SetParent(transform, false);
        return prefab.GetComponent<MonsterListItemView>();
    }

    public void UpdatePartyView(PartyEntity entity)
    {
        if (entity == null)
            return;

        for(int i=0; i<entity.MemberMonsterIds.Count; i++)
        {
            items[i].UpdateItem(MonsterDataManager.Instance.GetMonsterData(entity.MemberMonsterIds[i]));
        }

        partyEntity = entity;

        ResetSelectedItem();

        if (UpdatePartyMemberHandler != null)
            UpdatePartyMemberHandler();
    }

    public void UpdatePartyView(int index, int monsterId)
    {
        if (items.ContainsKey(index))
        {
            items[index].UpdateItem(MonsterDataManager.Instance.GetMonsterData(monsterId));
        }
    }

    public void OnClickedPartyItem(MonsterListItemView itemView)
    {
        var partyItem = itemView.GetComponent<PartyItemView>();
        var itemPosition = partyItem.Position;

        if(currentSelectedPartyItemPosition != itemPosition)
        {
            currentSelectedPartyItemPosition = itemPosition;
        }

        if (ItemSelectedHandler != null)
        {
            ItemSelectedHandler(partyItem);
        }
    }

    public void TryAddPartyMember(MonsterListItemView itemView)
    {
        if (currentSelectedPartyItemPosition != -1)
        {
            var sameItem = items.Where(item => item.Value.Entity == itemView.Entity).FirstOrDefault().Value;

            if(sameItem != null && sameItem.GetComponent<PartyItemView>().Position != currentSelectedPartyItemPosition)
            {
                var currentSelectedItemEntity = items[currentSelectedPartyItemPosition].Entity;
                sameItem.UpdateItem(currentSelectedItemEntity);
            }

            items[currentSelectedPartyItemPosition].UpdateItem(itemView.Entity);
            ResetSelectedItem();
        }
        else if (currentSelectedPartyItemPosition == -1)
        {
            var emptyItem = items.Values.Where(item => item.Entity == null).FirstOrDefault();
            var sameItem = items.Where(item => item.Value.Entity == itemView.Entity).FirstOrDefault().Value;

            if (emptyItem != null && sameItem == null)
            {
                emptyItem.UpdateItem(itemView.Entity);
                ResetSelectedItem();
            }
        }

        if (UpdatePartyMemberHandler != null)
            UpdatePartyMemberHandler();
    }

    public bool IsEmpty
    {
        get
        {
            var emptyItem = items.Values.Where(item => item.Entity == null).FirstOrDefault();
            return emptyItem != null;
        }
    }

    public void ResetSelectedItem()
    {
        //reset currentSelectedItem position
        currentSelectedPartyItemPosition = -1;

        if (ResetItemHandler != null)
            ResetItemHandler();
    }

    public void TryRemovePartyMember()
    {
        if (currentSelectedPartyItemPosition != -1)
        {
            items[currentSelectedPartyItemPosition].UpdateItem(null);
        }
        else if (currentSelectedPartyItemPosition == -1)
        {
            var noneEmptyItem = items.Values.Where(item => item.Entity != null).LastOrDefault();
            if(noneEmptyItem != null)
                noneEmptyItem.UpdateItem(null);
        }

        ResetSelectedItem();

        if (UpdatePartyMemberHandler != null)
            UpdatePartyMemberHandler();
    }

    public List<int> GetCurrentPartyMembers()
    {
        List<int> partyMembers = new List<int>();

        for (int i = 0; i < GameDefineData.NUMBER_OF_PARTY_MEMBER; i++)
        {
            partyMembers.Add(items[i].Entity == null ? 0 : items[i].Entity.monsterId);
        }

        return partyMembers;
    }
}
