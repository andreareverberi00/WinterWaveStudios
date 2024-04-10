using UnityEngine;

public class TestMissions : MonoSingleton<TestMissions>
{
    public Waste waste;
    public int Counter;
    public int Counter2;
    //public BinDataHolder binholder;
    //private bool MIssion1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waste.wasteType.GetType() == typeof(Waste.WasteType)/*binholder.binData.acceptsType*/)
        {
            //Debug.Log("PLASTICA");
        }
        int score = PlayerPrefs.GetInt("score", 0);
        score = score + Counter;
        Debug.Log("Counter :" + Counter);
        Debug.Log("Counter2 :" + Counter2);

    }
}
