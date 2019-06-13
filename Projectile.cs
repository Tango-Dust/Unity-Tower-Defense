using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    private int damage;
    Rigidbody self;
    public int bulletID;

    // instantiate/ create bullet(from fake thing in front of turrets)
    // Set position in front of player/ set rotation
    // get the rigid body that is attached to the instanctiated bullet
    // shoot the bullet with turret.main.transform.position.forward + speed


    // Use this for initialization
    void Start ()
    {
        if(bulletID == 1)
        {
            damage = 10;
            speed = 300f;
        }

        else if (bulletID == 2)
        {
            damage = 55;
            speed = 500f;
        }

        else if (bulletID == 3)
        {
            damage = 5;
            speed = 300f;
        }
        self = GetComponent<Rigidbody>();
        self.velocity = transform.forward * speed;
    }


    // Update is called once per frame
    void Update ()
    {	}

    private void OnTriggerEnter(Collider other)
    {
        //If what the bullet has collided with is an enemy, it will do damage to the enemy and then
        //destroy iteslf
        //piercing
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (bulletID == 1)
            {
                other.gameObject.GetComponent<EnemyController>().SetHealth(damage);
                Destroy(gameObject);
            }

            else if (bulletID == 2)
            {
                other.gameObject.GetComponent<EnemyController>().SetHealth(damage, "piercing");
                Destroy(gameObject);
            }

            else if (bulletID == 3)
            {
                other.gameObject.GetComponent<EnemyController>().SetHealth(damage);
                Destroy(gameObject);
            }

        }
        //If the bullet hits some terrain this should stop it and make it destroy itself
        if (other.gameObject.CompareTag("Impassible Object"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //If the bullet hits some terrain this should stop it and make it destroy itself
        if (other.gameObject.CompareTag("Impassible Object"))
        {
            Destroy(gameObject);
        }
    }
}
