using System.Collections;
using UnityEngine;

public class WasteDataHolder : MonoBehaviour
{
    public Waste wasteData;
    public GameObject Portal;
    public int Counter = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bin"))
        {
            BinDataHolder binHolder = other.GetComponent<BinDataHolder>();
            if (binHolder != null && wasteData != null)
            {
                bool isCorrectSorting = wasteData.wasteType == binHolder.binData.acceptsType;
                WastePool.Instance.ReturnWaste(gameObject);

                if (isCorrectSorting)
                {
                    Debug.Log("Correct sorting!");
                    ScoreController.Instance.RecordCorrectThrow();
                    ScoreController.Instance.AddScore();
                    BatteryController.Instance.CollectBattery(10);
                    FirstRow();

                }

                else
                {
                    Debug.Log("Incorrect sorting.");
                    ScoreController.Instance.RecordMissedThrow();
                    BatteryController.Instance.ConsumeEnergy();
                }
                PlaySoundBasedOnWasteType();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            WastePool.Instance.ReturnWaste(gameObject);
            ScoreController.Instance.RecordMissedThrow();
            PlaySoundBasedOnWasteType();
        }
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {

            transform.position = new Vector3(Portal.transform.position.x + 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }

    }
    private void Update()
    {
        if (transform.position.x >= CameraView.Instance.maxcamera && transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x - 1, 0.5f, transform.position.z);
        }
        if (transform.position.x >= CameraView.Instance.maxcamera && transform.position.y <= 0)
        {
            WastePool.Instance.ReturnWaste(gameObject);
            //BatteryController.Instance.ConsumeEnergy();
        }
    }
    public IEnumerator ReturnWaste()
    {
        yield return new WaitForSeconds(4f);
        WastePool.Instance.ReturnWaste(gameObject);
        ScoreController.Instance.RecordMissedThrow();
        BatteryController.Instance.ConsumeEnergy();
    }

    void FirstRow()
    {
        if (wasteData.wasteType == Waste.WasteType.Plastic)
        {
            TestMissions.Instance.Counter++;
            TestMissions.Instance.Counter2++;
        }
        if (wasteData.wasteType == Waste.WasteType.Metal)
        {
            TestMissions.Instance.Counter2++;
        }
        if (wasteData.wasteType == Waste.WasteType.Glass)
        {
            TestMissions.Instance.Counter2++;
        }
        else if(wasteData.wasteType == Waste.WasteType.Organic || wasteData.wasteType == Waste.WasteType.Paper)
        {
            TestMissions.Instance.AntiCounter2++;
        }
    }
    private void PlaySoundBasedOnWasteType()
    {
        switch (wasteData.wasteType)
        {
            case Waste.WasteType.Plastic:
                AudioController.Instance.PlaySound("Plastic_bottle_squeezing");
                break;
            case Waste.WasteType.Metal:
                AudioController.Instance.PlaySound("Metal_hit");
                break;
            case Waste.WasteType.Glass:
                AudioController.Instance.PlaySound("Glass_bottles_fall");
                break;
            case Waste.WasteType.Paper:
                AudioController.Instance.PlaySound("Cardboard_box_drop");
                break;
            case Waste.WasteType.Organic:
                AudioController.Instance.PlaySound("Banana_splat");
                break;
            default:
                Debug.LogError("Unsupported waste type for sound.");
                break;
        }
    }
}