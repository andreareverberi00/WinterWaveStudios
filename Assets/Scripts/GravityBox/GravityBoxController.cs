using UnityEngine;

public class GravityBoxController : MonoSingleton<GravityBoxController>
{
    public float AttractionForce = 250f;
    public float Attractionmagnet;
    private void Start()
    {
        Attractionmagnet = AttractionForce;
    }

}
