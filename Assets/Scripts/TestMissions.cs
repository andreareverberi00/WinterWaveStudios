using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMissions : MonoBehaviour
{
    Waste waste;
    BinDataHolder binholder;
    //private bool MIssion1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Waste.WasteType.Plastic == binholder.binData.acceptsType)
        {
            
        }
    }
}
