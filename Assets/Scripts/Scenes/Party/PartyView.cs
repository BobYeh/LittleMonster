using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Monster;

public class PartyView : MonoBehaviour
{
    public delegate void OnPartyViewItemSelected(MonsterListItemView view);
    public OnPartyViewItemSelected ItemSelectedHandler;

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
        var itemPosition = itemView.GetComponent<PartyItemView>().Position;

        if(currentSelectedPartyItemPosition != itemPosition)
        {
            currentSelectedPartyItemPosition = itemPosition;
        }
        else
        {
            currentSelectedPartyItemPosition = -1;
        }

        if (ItemSelectedHandler != null)
        {
            ItemSelectedHandler(itemView);
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
        }
        else if (currentSelectedPartyItemPosition == -1)
        {
            var emptyItem = items.Values.Where(item => item.Entity == null).FirstOrDefault();
            var sameItem = items.Where(item => item.Value.Entity == itemView.Entity).FirstOrDefault().Value;

            if (emptyItem != null && sameItem == null)
            {
                emptyItem.UpdateItem(itemView.Entity);
            }
        }

        //reset currentSelectedItem position
        currentSelectedPartyItemPosition = -1;
    }
}
