using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class AgentBehaviour : MonoBehaviour {
    
    public int removeDistance = 40;
    private float underwaterLevel;
    private GameObject buggy;
    private GameObject terrain;

    void Start () {
        buggy = GameObject.Find("buggy");
        terrain = GameObject.Find("Terrain");
        underwaterLevel = Underwater.underwaterLevel;
        underwaterLevel -= 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //delete fish if out of range
        float distanceToBuggy = Vector3.Distance(buggy.transform.position, transform.position);
        if (distanceToBuggy > removeDistance)
        {
            destroyFish();
        }

        //delete fish if under terrain
        if (transform.position.y < Terrain.activeTerrain.SampleHeight(transform.position))
        {
            destroyFish();
            Vector3 position = transform.position;
            position.y = Terrain.activeTerrain.SampleHeight(transform.position);
            position.y += 0.2f;
            //transform.position = position;
        }

        //delete fish if above water
        if (transform.position.y > Underwater.underwaterLevel + 1)
        {
            destroyFish();
        }

        //fix fish underwater Y-Position
        if (transform.position.y > underwaterLevel)
        {
            //transform.position = new Vector3(transform.position.x, underwaterLevel, transform.position.z);
        }
    }

    private void destroyFish()
    {
        //decrease Counter
        FishCount fc = FishCount.Instance;
        switch (gameObject.transform.name)
        {
            case "tuna":
                fc.decreaseCounter(1);
                break;
            case "sockeye":
                fc.decreaseCounter(2);
                break;
            case "shark":
                fc.decreaseCounter(3);
                break;
        }

        //delete fish
        Destroy(gameObject);
    }
}
