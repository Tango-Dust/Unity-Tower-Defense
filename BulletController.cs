using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    private Vector3 adjustment = new Vector3(.5f,0,0);

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void fireRound(GameObject launcher, GameObject target)
    {
        if (launcher.GetComponent<TurretController>().getTurretType() == 1)
        {
            //creates a bullet at the position of the turret that fired it
            Instantiate(bullet1, launcher.transform.position, launcher.transform.rotation);
        }

        else if (launcher.GetComponent<TurretController>().getTurretType() == 2)
        {
            //creates a bullet at the position of the turret that fired it
            Instantiate(bullet2, launcher.transform.position, launcher.transform.rotation);
        }

        else if (launcher.GetComponent<TurretController>().getTurretType() == 3)
        {
            //creates a bullet at the position of the turret that fired it
            Instantiate(bullet3, launcher.transform.position, launcher.transform.rotation);
        }

    }


    // instantiate/ create bullet(from fake thing in front of turrets)
    // Set position in front of player/ set rotation
    // get the rigid body that is attached to the instanctiated bullet
    // shoot the bullet with turret.main.transform.position.forward + speed



}
