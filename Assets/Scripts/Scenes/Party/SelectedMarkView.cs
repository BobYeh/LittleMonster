using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMarkView : MonoBehaviour
{
    [SerializeField]
    GameObject selectedMark;

    public void SetSelected(bool isSelected)
    {
        selectedMark.SetActive(isSelected);
    }
}
