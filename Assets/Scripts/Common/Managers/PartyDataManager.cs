using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Utils;

public class PartyDataManager : SingletonMonoBehaviour<PartyDataManager>
{
    Dictionary<int, PartyEntity> allPartyInfo = new Dictionary<int, PartyEntity>();

    protected override void Awake()
    {
        InitializeDummyData();
    }

    public Dictionary<int, PartyEntity> AllPartyInfo
    {
        get
        {
            return allPartyInfo;
        }
    }

    public void UpdateParty(int index, List<int> memberMonsterIds)
    {
        allPartyInfo[index].MemberMonsterIds = memberMonsterIds;
    }

    #region Dummy
    public void InitializeDummyData()
    {
        for(int i = 0; i<3; i++)
        {
            var entity = new PartyEntity();
            entity.PartyNumber = i;
            entity.MemberMonsterIds = new List<int>();

            for (int j = 0; j < GameDefineData.NUMBER_OF_PARTY_MEMBER; j++)
            {
                if(i == j)
                    entity.MemberMonsterIds.Add(1);
                else
                    entity.MemberMonsterIds.Add(0);
            }
            allPartyInfo.Add(i, entity);
        }
    }
    #endregion
}
