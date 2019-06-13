using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyController : MonoBehaviour {

    public static int money = 0; //can be used to set starting money
    public int startingMoney = 0;

    public static int power = 0;
    public static int usedPower;

    public static int recyclerCount = 0;

    // Use this for initialization
    void Start ()
    {
        money = startingMoney;
        SetUsedPower();
        SetStartingPower();
    }
	
	// Update is called once per frame
	void Update ()
    {
        startingMoney = money;
        //SetUsedPower();
    }

    //returns player's money
    public int getMoney()
    {
        return money;
    }

    //How player gets more money. Called by enemies when they are die
    public void Income(int bounty, int health)
    {
        if (recyclerCount != 0)
        {
            money += (int)(bounty + (Mathf.Log(2 * recyclerCount) * health * .05));
        }
        else
        {
            money += bounty;
        }
    }

    //requests resources to use. If power is available uses it and returns true, otherwise returns false.
    public bool RequestResources(int requestedMoney, int requestedPower)
    {
        if (money >= requestedMoney)
        {
            if (power >= usedPower + requestedPower || requestedPower == 0)
            {
                money -= requestedMoney;
                usedPower += requestedPower;
                return true;
            }
            return false;
        }
        return false;
    }

    //returns total power available
    public int GetPower()
    {
        return power;
    }

    //returns power currently in use
    public int GetUsedPower()
    {
        return usedPower;
    }

    //requests power to use. If power is available uses it and returns true, otherwise returns false.
    public bool UsePower(int requestedPower)
    {
        if (power <= usedPower + requestedPower)
        {
            usedPower += requestedPower;
            return true;
        }
        return false;
    }

    //Sets power based on amount of power plants
    public void SetStartingPower(bool add = true)
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        int count = 0;

        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].GetComponent<BuildingController>().GetId() == 2)
            {
                count++;
            }
        }
        power = count*100;
    }

    public void SetPower(int i = 1)
    {
        power += 100 * i;
    }

    //sets used power for game start
    public void SetUsedPower()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
       int count = 0;
    
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].GetComponent<BuildingController>().GetId() != 0 && buildings[i].GetComponent<BuildingController>().GetId() != 2)
            {
                count += buildings[i].GetComponent<BuildingController>().getPowerCost();
            }
        }
        usedPower = count;
    }

    public void RecyclerCountUpdate(int i)
    {
        recyclerCount += i;
    }
}
