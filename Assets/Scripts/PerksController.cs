using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksController : MonoSingleton<PerksController>
{
    public bool plastic;
    public bool metal;
    public bool glass;
    public bool paper;
    public bool organic;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
