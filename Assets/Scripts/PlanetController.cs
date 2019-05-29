using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour
{
    public float speed = 10.0f;
    private bool rotate = false;

    void OnTriggerEnter(Collider collider)
    {
        if ((gameObject.name == "Sun_ImageTarget" && collider.gameObject.name == "Mercury_ImageTarget") ||
            (gameObject.name == "Mercury_ImageTarget" && collider.gameObject.name == "Sun_ImageTarget") ||
            (gameObject.name == "Mercury_ImageTarget" && collider.gameObject.name == "Venus_ImageTarget") ||
            (gameObject.name == "Venus_ImageTarget" && collider.gameObject.name == "Mercury_ImageTarget") ||
            (gameObject.name == "Venus_ImageTarget" && collider.gameObject.name == "Earth_ImageTarget") ||
            (gameObject.name == "Earth_ImageTarget" && collider.gameObject.name == "Venus_ImageTarget") ||
            (gameObject.name == "Earth_ImageTarget" && collider.gameObject.name == "Mars_ImageTarget") ||
            (gameObject.name == "Mars_ImageTarget" && collider.gameObject.name == "Earth_ImageTarget") ||
            (gameObject.name == "Mars_ImageTarget" && collider.gameObject.name == "Jupiter_ImageTarget") ||
            (gameObject.name == "Jupiter_ImageTarget" && collider.gameObject.name == "Mars_ImageTarget") ||
            (gameObject.name == "Jupiter_ImageTarget" && collider.gameObject.name == "Saturn_ImageTarget") ||
            (gameObject.name == "Saturn_ImageTarget" && collider.gameObject.name == "Jupiter_ImageTarget") ||
            (gameObject.name == "Saturn_ImageTarget" && collider.gameObject.name == "Neptune_ImageTarget") ||
            (gameObject.name == "Neptune_ImageTarget" && collider.gameObject.name == "Saturn_ImageTarget") ||
            (gameObject.name == "Neptune_ImageTarget" && collider.gameObject.name == "Uranus_ImageTarget") ||
            (gameObject.name == "Uranus_ImageTarget" && collider.gameObject.name == "Neptune_ImageTarget") ||
            (gameObject.name == "Uranus_ImageTarget" && collider.gameObject.name == "Pluto_ImageTarget") ||
            (gameObject.name == "Pluto_ImageTarget" && collider.gameObject.name == "Uranus_ImageTarget"))
            rotate = true;
    }

    void OnTriggerExit(Collider collider)
    {
        rotate = false;
    }

    void Update()
    {
        if (rotate)
            transform.GetChild(0).gameObject.transform.Rotate(Vector3.up, speed * 3 * Time.deltaTime);
    }

}