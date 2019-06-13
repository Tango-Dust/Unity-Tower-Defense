using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private bool canFire = true;
    private int damage = 25;
    public GameObject self;
    public GameObject BulletController;
    public int turretType;

    //For building the turrets, first line is gold cost second line is power cost
    private int[,] costArray = { { 75, 150, 200 },
        { 5, 15, 25 } };

    // Use this for initialization
    void Start ()
    {
        self = gameObject;
        Debug.Log(gameObject.name);
        gameObject.layer = 2;

        if (Debug.isDebugBuild == true)
        {
            Debug.Log("");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public int[,] getCost()
    {
        return costArray;
    }


    //This will detect the enemy and call the bullet controller to instatiate a bullet to fire at the enemy, the only things needed from this to shoot, should be its position
    //For the starting point for the bullet and the what enemy is being shot at.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 ePosition = other.gameObject.GetComponent<Transform>().position;
            
            if(canFire == true)
            {
                canFire = false;
                //Gets the bullet controller object in the world and calls its script, to use the fucntion fire round
                BulletController.GetComponent<BulletController>().fireRound(self, other.gameObject);
                if (turretType == 1)
                {
                    Invoke("Reloading", .2f);
                }

                else if (turretType == 2)
                {
                    Invoke("Reloading", 1.5f);
                }

                else if (turretType == 3)
                {
                    Invoke("Reloading", .5f);
                }


            }
        }
    }

    public int getTurretType()
    {
        return turretType;
    }

    private void Reloading()
    {
        canFire = true;
        
    }
}
