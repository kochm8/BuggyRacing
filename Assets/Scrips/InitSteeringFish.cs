using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class InitSteeringFish : MonoBehaviour {

    private int underwaterLevel = 4;
    private GameObject buggy;

    void Start () {
        buggy = GameObject.Find("buggy");
    }
	
	void Update () {

        //spawn of buggy under water
        if (transform.position.y < underwaterLevel)
        {
            FishCount fc = FishCount.Instance;

            if (fc.canSpawn(1))
            {
                spawnTuna();
            }

            if (fc.canSpawn(2))
            {
                spawnSockeye();
            }

            if (fc.canSpawn(3))
            {
                spawnShark();
            }
        }
    }


    private void spawnTuna()
    {
        //increase counter
        FishCount fc = FishCount.Instance;

        //spwan tuna
        GameObject tuna = (GameObject)Instantiate(Resources.Load("SteeringFish1"));
        tuna.GetComponent<SteerForEvasion>().Menace = buggy.GetComponent<AutonomousVehicle>();
        tuna.GetComponent<SteerToFollow>().Target = buggy.transform;
        tuna.transform.position = getFishPosition();
        tuna.name = "tuna";
        fc.increaseCounter(1);

    }

    private void spawnSockeye()
    {
        FishCount fc = FishCount.Instance;

        //spwan sockeye
        GameObject sockeye = (GameObject)Instantiate(Resources.Load("SteeringFish2"));
        sockeye.GetComponent<SteerForEvasion>().Menace = buggy.GetComponent<AutonomousVehicle>();
        sockeye.GetComponent<SteerToFollow>().Target = buggy.transform;
        sockeye.GetComponent<SteerForWallAvoidance>().Menace = GameObject.Find("WaterDown").GetComponent<AutonomousVehicle>();
        sockeye.GetComponent<SteerForTerrainAvoidance>().Menace = GameObject.Find("Terrain").GetComponent<AutonomousVehicle>();
        sockeye.transform.position = getFishPosition();
        sockeye.name = "sockeye";
        fc.increaseCounter(2);
    }

    private void spawnShark()
    {
        //increase counter
        FishCount fc = FishCount.Instance;

        //spwan shark
        GameObject shark = (GameObject)Instantiate(Resources.Load("SteeringFish3"));
        shark.GetComponent<SteerForEvasion>().Menace = buggy.GetComponent<AutonomousVehicle>();
        shark.GetComponent<SteerToFollow>().Target = buggy.transform;
        shark.transform.position = getFishPosition();
        shark.name = "shark";
        fc.increaseCounter(3);
    }

    private Vector3 getFishPosition()
    {
        //calc fish Position
        float offset = 0.2f;
        float distance = 30.0f;
        Transform buggyTransform = buggy.GetComponent<Transform>();
        Vector3 fishPosition = buggyTransform.position + buggyTransform.forward * distance;
        fishPosition.y = Random.Range(offset, underwaterLevel - offset);
        fishPosition.x += Random.Range(-10, 10);
        fishPosition.z += Random.Range(0, 10);
        return fishPosition;
    }
}
