using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SubstanceController : MonoBehaviour, ITrackableEventHandler
{
    public string substanceName;
    public GameObject[] substance;
    public GameObject osad;
    private GameObject explosionPrefab, carbonDioxideCloudPrefab, hydrogenCloudPrefab;
    private Material substanceMaterial;
    private TrackableBehaviour mTrackableBehaviour;

    private void ChangeSubstance(string substanceNameChange)
    {
        substanceName = substanceNameChange;
        gameObject.name = substanceName + "_ImageTarget";
        substanceMaterial = Resources.Load(substanceName, typeof(Material)) as Material;
        foreach (GameObject i in substance)
        {
            i.GetComponent<Renderer>().material = substanceMaterial;
        }
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1.5f);
    }

    void HydrogenGas()
    {
        GameObject hydrogenGas = Instantiate(hydrogenCloudPrefab, transform.position, Quaternion.identity);
        hydrogenGas.transform.parent = gameObject.transform;
        hydrogenGas.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (substanceName == "H2" && collider.gameObject.name == "O2_ImageTarget")
        {
            ChangeSubstance("H2O");
            Explode();
        }
        else if (substanceName == "CO2" && collider.gameObject.name == "H2O_ImageTarget")
        {
            // Change into CO2 gas cloud
            foreach (GameObject i in substance)
                Destroy(i, 0.0f);
            GameObject co2_cloud = Instantiate(carbonDioxideCloudPrefab, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
            co2_cloud.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        else if (substanceName == "Fe" && collider.gameObject.name == "HCl_ImageTarget")
        {
            // Change into hydrogen gas and change osad into FeCl2
            substanceName = "H2(gas)";
            gameObject.name = "H2(gas)_ImageTarget";
            foreach (GameObject i in substance)
                Destroy(i, 0.0f);
            HydrogenGas();
            collider.gameObject.GetComponent<SubstanceController>().osad.GetComponent<Renderer>().material = Resources.Load("FeCl2", typeof(Material)) as Material;
        }
        else if (substanceName == "Fe" && collider.gameObject.name == "H2SO4_ImageTarget")
        {
            // Change into hydrogen gas and change osad into FeSO4
            substanceName = "H2(gas)";
            gameObject.name = "H2(gas)_ImageTarget";
            foreach (GameObject i in substance)
                Destroy(i, 0.0f);
            HydrogenGas();
            collider.gameObject.GetComponent<SubstanceController>().osad.GetComponent<Renderer>().material = Resources.Load("FeSO4", typeof(Material)) as Material;
        }
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
            explosionPrefab = Resources.Load("SmallExplosion", typeof(GameObject)) as GameObject;
            carbonDioxideCloudPrefab = Resources.Load("GroundFog", typeof(GameObject)) as GameObject;
            if (substanceName == "HCl" || substanceName == "H2SO4")
                osad = transform.Find("colbapalkazosadom/osad").gameObject;
            hydrogenCloudPrefab = Resources.Load("Steam", typeof(GameObject)) as GameObject;
            ChangeSubstance(substanceName);
        }
        else
        {
            // Lost
        }
    }
}
