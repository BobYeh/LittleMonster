using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Monster;
using System;

public class PartyGroupManager : MonoBehaviour
{
    [SerializeField]
    PartyView currentPartyView;
    [SerializeField]
    PartyView placeHolderPartyView;
    [SerializeField]
    Text partyName;

    Animator animator;

    private int currentPartyIndex = 0;

	void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey(ConstValue.PlayerPrefsKey.LAST_VIEWED_PARTYNUMBER))
            currentPartyIndex = PlayerPrefs.GetInt(ConstValue.PlayerPrefsKey.LAST_VIEWED_PARTYNUMBER);
        currentPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[currentPartyIndex]);
        UpdatePartyName();
    }

    public void OnClickedNextParty()
    {
        PartyDataManager.Instance.UpdateParty(currentPartyIndex, currentPartyView.GetCurrentPartyMembers());

        if(currentPartyIndex < GameDefineData.NUMBER_OF_PARTY - 1)
        {
            currentPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[currentPartyIndex + 1]);
            placeHolderPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[currentPartyIndex]);
            currentPartyIndex = currentPartyIndex + 1;
        }
        else if(currentPartyIndex == GameDefineData.NUMBER_OF_PARTY - 1)
        {
            currentPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[0]);
            placeHolderPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[currentPartyIndex]);
            currentPartyIndex = 0;
        }

        UpdatePartyName();

        PlayerPrefs.SetInt(ConstValue.PlayerPrefsKey.LAST_VIEWED_PARTYNUMBER, currentPartyIndex);

        animator.Play("MoveToNextParty", 0, 0);
    }

    public void OnClickedPreParty()
    {
        PartyDataManager.Instance.UpdateParty(currentPartyIndex, currentPartyView.GetCurrentPartyMembers());

        if (currentPartyIndex > 0)
        {
            currentPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[currentPartyIndex - 1]);
            placeHolderPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[currentPartyIndex]);
            currentPartyIndex = currentPartyIndex - 1;
        }
        else if (currentPartyIndex == 0)
        {
            currentPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[GameDefineData.NUMBER_OF_PARTY - 1]);
            placeHolderPartyView.UpdatePartyView(PartyDataManager.Instance.AllPartyInfo[currentPartyIndex]);
            currentPartyIndex = GameDefineData.NUMBER_OF_PARTY - 1;
        }

        UpdatePartyName();

        PlayerPrefs.SetInt(ConstValue.PlayerPrefsKey.LAST_VIEWED_PARTYNUMBER, currentPartyIndex);

        animator.Play("MoveToPreviousParty", 0, 0);
    }

    void UpdatePartyName()
    {
        partyName.text = string.Format("Party{0}", currentPartyIndex + 1);
    }

    public void AddClickedPartyItemHandler(PartyView.OnPartyViewItemSelected handler)
    {
        currentPartyView.ItemSelectedHandler += handler;
    }
}
