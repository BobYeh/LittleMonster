using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeRadioButton : RadioButton
{
    [SerializeField]
    public GameObject selectedMark;

    protected override void OnSelected()
    {
        base.OnSelected();
        selectedMark.SetActive(true);
    }

    protected override void OnUnSelected()
    {
        base.OnUnSelected();
        selectedMark.SetActive(false);
    }
}
