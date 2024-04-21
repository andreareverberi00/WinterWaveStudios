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
        // Leggi le preferenze salvate per selezionare la corretta antenna e i perk
        string selectedAntenna = PlayerPrefs.GetString("SelectedAntenna", "baseAntenna");
        string selectedPerk = PlayerPrefs.GetString("SelectedPerk", "");

        Debug.Log("Antenna selezionata: " + selectedAntenna);
        Debug.Log("Perk selezionato: " + selectedPerk);
        // Disattiva tutti gli elementi e attiva solo quelli selezionati
        foreach (var antenna in antennas)
        {
            antenna.SetActive(antenna.name == selectedAntenna);
        }

        foreach (var perk in perks)
        {
            perk.SetActive(perk.name == selectedPerk);
        }
    }
    public void SelectAntenna(string antennaName)
    {
        PlayerPrefs.SetString("SelectedAntenna", antennaName);
        UpdateVisuals();
    }

    public void SelectPerk(string perkName)
    {
        PlayerPrefs.SetString("SelectedPerk", perkName);
        UpdateVisuals();
    }

}
