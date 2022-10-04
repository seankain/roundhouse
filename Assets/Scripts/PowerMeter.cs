using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMeter : MonoBehaviour
{


    public GameObject PowerSlicePrefab;
    public GameObject Indicator;
    public float Radius = 2;
    public int SliceCount = 10;
    private List<GameObject> powerSlices;
    private List<MeshRenderer> powerSliceRenderers;


    void Awake()
    {
        powerSlices = new List<GameObject>();
        powerSliceRenderers = new List<MeshRenderer>();
        for(var i = SliceCount; i >0; i--)
        {
            //powerSlices.Add(Instantiate(PowerSlicePrefab, this.transform));
            var indicator = PlaceIndicator(180 / SliceCount * i);
            var renderer = indicator.GetComponent<MeshRenderer>();
            renderer.enabled = false;
            powerSliceRenderers.Add(renderer);
            powerSlices.Add(indicator);
            
            //powerSliceRenderers[i].enabled = false;
            //powerSlices[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetPowerLevel(float powerLevel)
    {
        var powerPerSlice = 100f / SliceCount;
        var slices = powerPerSlice * powerLevel;
        for(var i = 0 ; i<powerSliceRenderers.Count; i++)
        {
            if(i <= slices)
            {
                powerSliceRenderers[i].enabled = true;
            }
            else
            {
                powerSliceRenderers[i].enabled = false;
            }
        }
    }

    private GameObject PlaceIndicator(float angle)
    {        
        var pos = new Vector3
        {
            x = (Radius * Mathf.Cos(angle * Mathf.Deg2Rad)),
            y = 0,
            z = (Radius * Mathf.Sin(angle * Mathf.Deg2Rad))
        };
        var rotation = new Vector3(0,-angle,0);
        var indicator = Instantiate(PowerSlicePrefab,this.gameObject.transform,false);
        indicator.transform.localPosition = pos;
        indicator.transform.rotation = Quaternion.Euler(rotation);
        return indicator;
    }

    private void PlaceSelectionIndicator(float angle)
    {
        var pos = new Vector3
        {
            x = (Radius * Mathf.Cos(angle * Mathf.Deg2Rad)),
            y = 0,
            z = (Radius * Mathf.Sin(angle * Mathf.Deg2Rad))
        };
        var rotation = new Vector3(0, -angle, 0);
        //var indicator = Instantiate(PowerSlicePrefab, this.gameObject.transform, false);
        Indicator.transform.localPosition = pos;
        Indicator.transform.rotation = Quaternion.Euler(rotation);
        Indicator.SetActive(true);
    }

    private void HideSelectionIndicator()
    {
        Indicator.SetActive(false);
    }


    private Vector3[] PlaceSlices(Vector3 centerLocation, float radius, int resolution)
    {
        if (resolution < 3)
        {
            Debug.LogWarning("Resolution must be 3 or greater");
            resolution = 3;
        }
        List<Vector3> verts = new List<Vector3>();

        int stepSize = (int)Mathf.Floor(180 / resolution);
        for (var i = resolution; i > 0; i--)
        {
            var angle = stepSize * i;
            var v = new Vector3
            {
                x = (radius * Mathf.Cos(angle * Mathf.Deg2Rad)) + centerLocation.x,
                y = centerLocation.y,
                z = (radius * Mathf.Sin(angle * Mathf.Deg2Rad)) + centerLocation.z
            };
            verts.Add(v);

        }
        return verts.ToArray();
    }
}
