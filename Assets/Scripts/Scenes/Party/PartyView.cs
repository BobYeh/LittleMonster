using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Monster;

public class PartyView : MonoBehaviour
{
    private PartyEntity partyEntity;

    Dictionary<int, MonsterListItemView> items = new Dictionary<int, MonsterListItemView>();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        for(int i=0; i<GameDefineData.NUMBER_OF_PARTY_MEMBER; i++)
        {
            items.Add(i , GenerateMonsterListItemView());
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
}
