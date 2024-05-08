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
        transform.GetChild(0).gameObject.SetActive(false);
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

                    if (Scene_Link2.Instance.perkplastic && binHolder.binData.acceptsType == Waste.WasteType.Plastic)
                    {
                        ScoreController.Instance.punteggio = (int)(ScoreController.Instance.punteggio * 1.5f);
                        ScoreController.Instance.AddScore();
                        ScoreController.Instance.punteggio = 10;
                    }
                    else if (Scene_Link2.Instance.perkmetal && binHolder.binData.acceptsType == Waste.WasteType.Metal)
                    {
                        ScoreController.Instance.punteggio = 15;
                        ScoreController.Instance.AddScore();
                        ScoreController.Instance.punteggio = 10;
                    }
                    else if (Scene_Link2.Instance.perkglass && binHolder.binData.acceptsType == Waste.WasteType.Glass)
                    {
                        ScoreController.Instance.punteggio = 20;
                        ScoreController.Instance.AddScore();
                        ScoreController.Instance.punteggio = 10;
                    }
                    else if (Scene_Link2.Instance.perkpaper && binHolder.binData.acceptsType == Waste.WasteType.Paper)
                    {
                        ScoreController.Instance.punteggio = 30;
                        ScoreController.Instance.AddScore();
                        ScoreController.Instance.punteggio = 10;
                    }
                    else if (Scene_Link2.Instance.perkorganic && binHolder.binData.acceptsType == Waste.WasteType.Organic)
                    {
                        ScoreController.Instance.punteggio = 40;
                        ScoreController.Instance.AddScore();
                        ScoreController.Instance.punteggio = 10;
                    }
                    else
                    {
                        ScoreController.Instance.AddScore();
                    }

                    BatteryController.Instance.CollectBattery(3);
                    VFXController.Instance.PlayVFXAtPosition(VFXType.CorrectSorting, transform.position, 1f);

                }

                else
                {
                    Debug.Log("Incorrect sorting.");
                    ScoreController.Instance.RecordMissedThrow();
                    BatteryController.Instance.ConsumeEnergy();
                    VFXController.Instance.PlayVFXAtPosition(VFXType.Explosion, transform.position, 3f);

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
