using UnityEngine;

public class RobotVisualController : MonoBehaviour
{
    [SerializeField]
    private GameObject baseAntenna;
    [SerializeField]
    private GameObject tankAntenna;
    [SerializeField]
    private GameObject proAntenna;

    [SerializeField]
    private GameObject[] perkObjects; // Array di oggetti perk che possono essere attivati/disattivati

    [SerializeField]
    private float rotationSpeed = 1f;

    [SerializeField]
    private bool rotateModel = true;

    private void Start()
    {
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
        transform.Rotate(Vector3.forward, rotationSpeed*Time.deltaTime);
    }
    public void UpdateVisuals()
    {
        // Ottieni la personalizzazione selezionata dall'utente
        string selectedAntenna = PlayerPrefs.GetString("SelectedAntenna", "base");
        string selectedPerk = PlayerPrefs.GetString("SelectedPerk", "");

        // Aggiorna l'antenna
        baseAntenna.SetActive(selectedAntenna == "base");
        tankAntenna.SetActive(selectedAntenna == "tank");
        proAntenna.SetActive(selectedAntenna == "pro");

        // Aggiorna i perk
        foreach (var perkObject in perkObjects)
        {
            // Assumi che ogni perkObject abbia un nome che corrisponde al nome del perk
            perkObject.SetActive(perkObject.name == selectedPerk);
        }

        // Potrebbe essere necessario aggiungere logica aggiuntiva se le combinazioni di perk e antenne sono più complesse.
    }

    // Altri metodi per attivare/disattivare specifiche parti visive del robot...
}
