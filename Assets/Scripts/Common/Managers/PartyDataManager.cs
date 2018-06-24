using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Utils;
using System.Linq;

/// <summary>
/// Party member data from server (temp)
/// </summary>
public class PartyMemberData
{
    public int monsterId { get; set; }
    public int masterId { get; set; }
    public int position { get; set; }
}


/// <summary>
/// Party data from server (temp)
/// </summary>
public class PartyData
{
    public int partyNumber;
    public List<PartyMemberData> partyMemberData;

    public PartyData(int partyNumber, List<object> memberDatas)
    {
        this.partyNumber = partyNumber;
        partyMemberData = new List<PartyMemberData>();

        if (memberDatas != null)
        {
            foreach (var memberData in memberDatas)
            {
                partyMemberData.Add(JsonHelpers.DictionaryToObject<PartyMemberData>(memberData as Dictionary<string, object>));
            }
        }
    }
}

public class PartyDataManager : SingletonMonoBehaviour<PartyDataManager>
{
    Dictionary<int, PartyEntity> allPartyInfo = new Dictionary<int, PartyEntity>();

    [SerializeField]
    HttpPostTest http;

    protected override void Awake()
    {
      
    }

    public void UpdateDataFromServer(List<PartyData> partyData)
    {
        allPartyInfo = new Dictionary<int, PartyEntity>();

        for (int i = 0; i < partyData.Count; i++)
        {
            PartyEntity entity = new PartyEntity();
            entity.PartyNumber = partyData[i].partyNumber;
            entity.MemberMonsterIds = new List<int>();

            for(int j=0; j<GameDefineData.NUMBER_OF_PARTY_MEMBER; j++)
            {
                entity.MemberMonsterIds.Add(0);
            }

            foreach (var partyMemberData in partyData[i].partyMemberData)
            {
                entity.MemberMonsterIds[partyMemberData.position] = partyMemberData.monsterId;
            }

            allPartyInfo.Add(i, entity);
        }
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
        StartCoroutine(UpdateServerPartyData(index));
    }

    IEnumerator UpdateServerPartyData(int index)
    {
        yield return http.Post(API.UpdatePlayerParty(allPartyInfo[index]), (request) =>
        {
            Debug.Log("UpdateServerPartyData: " + request.downloadHandler.text);
        });
    }


}
