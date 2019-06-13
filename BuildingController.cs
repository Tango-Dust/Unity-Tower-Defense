using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{

    public int buildingId; // Used to determine building type.
    private GameObject econCont;
    public Material mat0;
    public Material mat1;
    public Material mat2;
    private Camera cam;

    //Building Stats
    public int health;
    private int maxhealth;
    private string buildingName;
    private string description;

    //Variables used for upgrade ui
    static bool isShowing = false;
    static bool lose = false;
    static int buildingCount = 0;
    public GameObject recyclerButton;
    public GameObject powerPlantButton;
    public GameObject cancelButton;
    public GameObject insufficientFunds;
    public GameObject upgradeButtons;
    public GameObject sellButton;
    public GameObject repairButton;
    public GameObject gameOver;

    //Array for costs. money then power
    private int[,] costArray = { { 0, 100, 100 }, { 0, 25, 0 } };

    // Use this for initialization
    void Start()
    {
        econCont = GameObject.FindGameObjectWithTag("Economy");
        SetBuildingType();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        LoseCondition();
    }

    //Sets the type of building based on building Id
    void SetBuildingType()
    {

        switch (buildingId)
        {
            case 0:
                buildingName = "Foundation";
                description = "A basic foundation that can be upgraded to other infrastructure buildings";
                health = maxhealth = 100;
                GetComponent<MeshRenderer>().material = mat0;
                tag = "Untagged";
                break;
            case 1:
                buildingName = "Recycler";
                description = "Recycles enemy units after they have been destroyed giving income equal to 10% of their health";
                health = maxhealth = 150;
                GetComponent<MeshRenderer>().material = mat1;
                tag = "Building";
                econCont.GetComponent<EconomyController>().RecyclerCountUpdate(1);
                buildingCount++;
                break;
            case 2:
                buildingName = "Power Generator";
                description = "Provides power to buildings and turrents";
                health = maxhealth = 100;
                GetComponent<MeshRenderer>().material = mat2;
                tag = "Building";
                //Debug.Log(buildingId);
                econCont.GetComponent<EconomyController>().SetPower();
                buildingCount++;
                break;
        }
    }

    //returns building's Id
    public int GetId()
    {
        return buildingId;
    }

    //returns buildings name
    public string GetName()
    {
        return buildingName;
    }

    //returns buildings description
    public string GetDescription()
    {
        return description;
    }

    //Sets health of building. Used when enemy comes in contact
    public void SetHealth(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (buildingId == 2)
            {
                econCont.GetComponent<EconomyController>().SetPower(-1);
            }

            FreePower();
            buildingId = 0;
            SetBuildingType();
        }
    }

    //Requests to upgrade building. If upgrade is succesful (Player has enough resources) returns true otherwise returns false
    public void UpgradeBuilding(int id)
    {
        if (econCont.GetComponent<EconomyController>().RequestResources(costArray[0, id], costArray[1, id]))
        {
            buildingId = id;
            SetBuildingType();
            CancelUi();
        }
        else
        {
            StartCoroutine(FundsWarning());
        }
    }

    public int getPowerCost()
    {
        return costArray[1, buildingId];
    }

    //Returns the total amount of non foundation building
    public int BuildingCount()
    {
        return buildingCount;
    }

    //Used to check for defeat if the player controls no nonfoundation buildings
    public bool LoseCondition()
    {
        if (BuildingCount() == 0)
        {
            if (!lose)
            {
                lose = true;
                gameOver.SetActive(true);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //Click on a foundation building to open up upgrade menu
    private void OnMouseDown()
    {
        if (!isShowing && buildingId == 0)
        {
            upgradeButtons.GetComponent<UpgradeController>().SetBuilding(gameObject);
            recyclerButton.SetActive(true);
            powerPlantButton.SetActive(true);
            cancelButton.SetActive(true);
            isShowing = true;
        }
        if (!isShowing && buildingId != 0)
        {
            upgradeButtons.GetComponent<UpgradeController>().SetBuilding(gameObject);
            sellButton.SetActive(true);
            cancelButton.SetActive(true);
            repairButton.SetActive(true);
            isShowing = true;
        }
    }

    void OnBecameInvisible()
    {
        sellButton.SetActive(false);
        recyclerButton.SetActive(false);
        powerPlantButton.SetActive(false);
        cancelButton.SetActive(false);
        repairButton.SetActive(false);
        isShowing = false;
    }

    //Hides the ui after use
    public void CancelUi()
    {
        sellButton.SetActive(false);
        recyclerButton.SetActive(false);
        powerPlantButton.SetActive(false);
        cancelButton.SetActive(false);
        repairButton.SetActive(false);
        isShowing = false;
    }

    //Is called when you have inssuficient funds and flashes a warning
    IEnumerator FundsWarning()
    {
        for (int i = 0; i < 5; i++)
        {
            insufficientFunds.SetActive(true);
            yield return new WaitForSeconds(.1f);
            insufficientFunds.SetActive(false);
            yield return new WaitForSeconds(.1f);
        }
    }

    //turns building into foundation and refunds 50% of the money
    public void SellBuilding()
    {
        if (isShowing && BuildingCount() > 1)
        {
            if (buildingId == 2)
            {
                //Debug.Log(buildingId);
                econCont.GetComponent<EconomyController>().SetPower(-1);
            }
            FreePower();
            econCont.GetComponent<EconomyController>().RequestResources(Mathf.RoundToInt(-.5f * costArray[0, buildingId]), 0);
            buildingId = 0;
            SetBuildingType();
            CancelUi();
        }
    }

    //Restores a building to full health and subtracts funds based on percent of health missing * building's cost
    public void RepairBuilding()
    {
        if (isShowing && health < maxhealth)
        {
            if (econCont.GetComponent<EconomyController>().RequestResources(Mathf.RoundToInt((1.0f * maxhealth - 1.0f * health) / (1.0f * maxhealth) * costArray[0, buildingId]), 0))
            {
                health = maxhealth;
                CancelUi();
            }
            else
            {
                StartCoroutine(FundsWarning());
            }
        }
    }

    private void FreePower()
    {
        econCont.GetComponent<EconomyController>().RequestResources(0, -costArray[1, buildingId]);
        buildingCount--;
        if (buildingId == 1)
        {
            econCont.GetComponent<EconomyController>().RecyclerCountUpdate(-1);
        }
    }
}
