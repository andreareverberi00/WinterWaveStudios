using System.Collections;
using TMPro;
using UnityEngine;

public class WasteDataHolder : MonoBehaviour
{
    public Waste wasteData;
    public GameObject Portal;
    public int Counter = 0;
    public PerkController pk;
    private bool isTouchingConveyor = false;
    private Rigidbody rb;
    private Quaternion startRotation;

    private void OnEnable()
    {
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
    // set start rotation
    private void OnDisable()
    {
        transform.rotation = startRotation;
    }
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
                    //BatteryController.Instance.CollectBattery(10);
                    VFXController.Instance.PlayVFXAtPosition(VFXType.CorrectSorting, transform.position, 1f);

                    //int compliments;
                    //compliments = Random.Range(1, 6);
                    //Compliment(compliments);
                    //FirstRow();

                }

                else
                {
                    Debug.Log("Incorrect sorting.");
                    ScoreController.Instance.RecordMissedThrow();
                    BatteryController.Instance.ConsumeEnergy();
                    VFXController.Instance.PlayVFXAtPosition(VFXType.Explosion, transform.position, 3f);

                    //int insults;
                    //insults = Random.Range(1,7);
                    //Insult(insults);

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
            BatteryController.Instance.ConsumeEnergy();
            PlaySoundBasedOnWasteType();
            VFXController.Instance.PlayVFXAtPosition(VFXType.Explosion, transform.position,3f);
        }
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            transform.position = new Vector3(Portal.transform.position.x + 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }
    }

    private void Update()
    {    
        Camera();
        if(pk.nocustom==false)
        {
            PerkVerification();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Conveyor"))
        {
            isTouchingConveyor = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Conveyor"))
        {
            isTouchingConveyor = false;
        }
    }
    public IEnumerator ReturnWaste()
    {
        yield return new WaitForSeconds(2.5f);
        if(!isTouchingConveyor)
        {
            WastePool.Instance.ReturnWaste(gameObject);
            ScoreController.Instance.RecordMissedThrow();
            BatteryController.Instance.ConsumeEnergy();
        }
    }

    void PerkPlastic()
    {
        if (wasteData.wasteType == Waste.WasteType.Plastic)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.Double;
        }
        if (wasteData.wasteType == Waste.WasteType.Paper)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.half;
        }
        else
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.initial;
        }
    }

    void PerkGlass()
    {
        if (wasteData.wasteType == Waste.WasteType.Glass)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.Double;
        }
        if (wasteData.wasteType == Waste.WasteType.Plastic)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.half;
        }
        else
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.initial;
        }
    }
    void PerkPaper()
    {
        if (wasteData.wasteType == Waste.WasteType.Paper)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.Double;
        }
        if (wasteData.wasteType == Waste.WasteType.Organic)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.half;
        }
        else
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.initial;
        }
    }
    void PerkMetal()
    {
        if (wasteData.wasteType == Waste.WasteType.Metal)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.Double;
        }
        if (wasteData.wasteType == Waste.WasteType.Organic)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.half;
        }
        else
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.initial;
        }
    }
    void PerkOrganic()
    {
        if (wasteData.wasteType == Waste.WasteType.Organic)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.Double;
        }
        if (wasteData.wasteType == Waste.WasteType.Metal)
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.half;
        }
        else
        {
            ScoreController.Instance.scoreAmount = ScoreController.Instance.initial;
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
   public void PerkVerification()
    {
        if (Scene_Link2.Instance.perkglass == true)
        {
            PerkPaper();
        }
        if (Scene_Link2.Instance.perkpaper == true)
        {
            PerkGlass();
        }
        if (Scene_Link2.Instance.perkplastic == true)
        {
            PerkPlastic();
        }
        if (Scene_Link2.Instance.perkorganic == true)
        {
            PerkMetal();
        }
        if (Scene_Link2.Instance.perkmetal == true)
        {
            PerkOrganic();
        }

    }
    void Camera()
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
}
