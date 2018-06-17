using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyItemView : MonoBehaviour 
{
    private int position;

    public int Position
    {
        set
        {
            if (value >= 0)
            {
                position = value;
            }
        }

        get
        {
            return position;
        }
    }
}
