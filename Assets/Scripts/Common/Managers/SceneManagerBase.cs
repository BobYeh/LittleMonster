using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneManagerBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject topCanvas;

    public virtual void OnOpen()
    {
        topCanvas.SetActive(true);
    }

    public virtual void OnClose()
    {
        topCanvas.SetActive(false);
    }
}
