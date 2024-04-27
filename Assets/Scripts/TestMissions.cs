using UnityEngine;

public class TestMissions : MonoSingleton<TestMissions>
{
    public int Counter;
    public int Counter2;
    public int AntiCounter2;
    //public BinDataHolder binholder;
    //private bool MIssion1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        int score = PlayerPrefs.GetInt("score", 0);
        score = score + Counter;
        //Debug.Log("Counter :" + Counter);
        //Debug.Log("Counter2 :" + Counter2);
        //Mission();
        Mission2();


    }
    void Mission()
    {
        if(Counter>49)
        {
            Debug.Log("Mission 50 plastic complete");
        }
    }
    void Mission2()
    {
        if (AntiCounter2 <= 0 && Counter2 != 0 && UIController.Instance.IsGameOver==true)
        {
            Debug.Log("Mission firstrow complete");
        }
    }

}
