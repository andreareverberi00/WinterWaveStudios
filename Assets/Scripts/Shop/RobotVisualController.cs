using UnityEngine;

public class RobotVisualController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] antennas; // Array che contiene tutti i modelli di antenna

    [SerializeField]
    private GameObject[] perks; // Array che contiene tutti i modelli di perk

    [SerializeField]
    private float rotationSpeed = 1f; // Velocità di rotazione del modello

    [SerializeField]
    private bool rotateModel = true; // Controllo per abilitare/disabilitare la rotazione

    public static RobotVisualController Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateVisuals();
    }

    private void Update()
    {
        if (rotateModel)
        {
            RotateModel();
        }
    }

    private void RotateModel()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void UpdateVisuals()
    {
        string selectedAntenna = PlayerPrefs.GetString("SelectedAntenna", "");
        string selectedPerk = PlayerPrefs.GetString("SelectedPerk", "");

        foreach (var antenna in antennas)
        {
            antenna.SetActive(selectedAntenna != "" && antenna.name == selectedAntenna);
        }

        foreach (var perk in perks)
        {
            perk.SetActive(selectedPerk != "" && perk.name == selectedPerk);
        }
    }
    private void SelectAntenna(string antennaName)
    {
        PlayerPrefs.SetString("SelectedAntenna", antennaName);
        UpdateVisuals();
    }

    private void SelectPerk(string perkName)
    {
        PlayerPrefs.SetString("SelectedPerk", perkName);
        UpdateVisuals();
    }

    public void SelectItemOfType(ShopItemType itemType, string itemName)
    {
        if (itemType == ShopItemType.Antenna)
        {
            SelectAntenna(itemName);
        }
        else if (itemType == ShopItemType.Perk)
        {
            SelectPerk(itemName);
        }
    }

}
