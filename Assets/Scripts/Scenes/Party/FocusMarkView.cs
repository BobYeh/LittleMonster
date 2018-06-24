using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusMarkView : MonoBehaviour
{
    [SerializeField]
    GameObject focusMark;

    public void SetFocus(bool isFocus)
    {
        focusMark.SetActive(isFocus);
    }
}