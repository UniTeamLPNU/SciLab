using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ElectricController : MonoBehaviour, ITrackableEventHandler
{
    public string deviceName;
    public GameObject cupola, light;
    public Material glass, lightGlass, halfLitGlass;
    private TrackableBehaviour mTrackableBehaviour;
    public bool resistorCurrent = false;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (deviceName == "Lamp" && collider.gameObject.name == "Battery_ImageTarget")
        {
            cupola.GetComponent<Renderer>().material = lightGlass;
            light.GetComponent<Light>().intensity = 100;
        }
        else if (deviceName == "Resistor" && collider.gameObject.name == "Battery_ImageTarget")
            resistorCurrent = true;
        else if (deviceName == "Lamp" && collider.gameObject.name == "Resistor_ImageTarget" && collider.GetComponent<ElectricController>().resistorCurrent)
        {
            cupola.GetComponent<Renderer>().material = halfLitGlass;
            light.GetComponent<Light>().intensity = 30;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (deviceName == "Lamp")
        {
            cupola.GetComponent<Renderer>().material = glass;
            light.GetComponent<Light>().intensity = 0;
        }
        else if (deviceName == "Resistor" && collider.gameObject.name == "Battery_ImageTarget")
            resistorCurrent = false;
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Detected
        }
        else
        {
            // Lost
        }
    }
}
