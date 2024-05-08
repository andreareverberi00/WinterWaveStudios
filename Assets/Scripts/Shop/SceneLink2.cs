using UnityEngine;

public class Scene_Link2 : MonoSingleton<Scene_Link2>
{

    public bool baseantenna;
    public bool tankantenna;
    public bool proantenna;
    public bool perkpaper;
    public bool perkglass;
    public bool perkplastic;
    public bool perkorganic;
    public bool perkmetal;

    void Start()
    {
        baseantenna = PlayerPrefs.GetString("SelectedAntenna") == "AntennaBase";
        tankantenna = PlayerPrefs.GetString("SelectedAntenna") == "AntennaTank";
        proantenna = PlayerPrefs.GetString("SelectedAntenna") == "AntennaPro";

        perkpaper = PlayerPrefs.GetString("SelectedPerk") == "PaperPerk";
        perkglass = PlayerPrefs.GetString("SelectedPerk") == "GlassPerk";
        perkplastic = PlayerPrefs.GetString("SelectedPerk") == "PlasticPerk";
        perkorganic = PlayerPrefs.GetString("SelectedPerk") == "OrganicPerk";
        perkmetal = PlayerPrefs.GetString("SelectedPerk") == "MetalPerk";
    }
    private void Update()
    {
        baseantenna = PlayerPrefs.GetString("SelectedAntenna") == "AntennaBase";
        tankantenna = PlayerPrefs.GetString("SelectedAntenna") == "AntennaTank";
        proantenna = PlayerPrefs.GetString("SelectedAntenna") == "AntennaPro";

        perkpaper = PlayerPrefs.GetString("SelectedPerk") == "PaperPerk";
        perkglass = PlayerPrefs.GetString("SelectedPerk") == "GlassPerk";
        perkplastic = PlayerPrefs.GetString("SelectedPerk") == "PlasticPerk";
        perkorganic = PlayerPrefs.GetString("SelectedPerk") == "OrganicPerk";
        perkmetal = PlayerPrefs.GetString("SelectedPerk") == "MetalPerk";
    }

}
