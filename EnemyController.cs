using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject spawner;
    public int enemyId; // Used to determine enemy type.
    private GameObject econCont;
    public bool seekAndDestroy = false; //Determines if enemy will seak out buildings (if true) or destination (if false)

    //Difficulty Modifiers
    static float healthModifier = 1.0f;
    static float damageModifier = 1.0f;
    static float bountyModifier = 1.0f;
    static float speedModifier = 1.0f;

    //Unit stats
    private float speed;
    private int maxHealth;
    public int health = 1;
    private int bounty;
    private int damage;
    public int armor = 0; //Reduces incoming damage to a unit by the armor value.

    /* Abilities Implemented:
     * regenerate: enemy will heal 1 hp per frame if it is under maxhealth.
     * shell: units armor is reduced to 0 after taking 10 damage and their speed is increased.
     * warcry: After unit is at half health increases the movespeed of nearby units by .5
     * 
     * Armor types:
     * light: weak to "piercing", resistant to "crushing"
     * heavy: weak to "crushing", resistant to "piercing"
     * divine: resistant to everything except "chaos"
     */
    public List<string> abilities; 

    //Used for collision and attack
    private Transform target;

    // Use this for initialization
    void Start ()
    {
        SetEnemyType();
        target = FindTarget();
        econCont = GameObject.FindGameObjectWithTag("Economy");
        spawner = GameObject.FindGameObjectWithTag("Spawn");

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (seekAndDestroy)
        {
            AttackTarget();
        }

        if (GetComponent<EnemyController>().abilities.Contains("regenerate"))
        {
            if (health < maxHealth)
            {
                health += 1;
            }
        }

        if (health <= 0)
        {
            econCont.GetComponent<EconomyController>().Income(GetBounty(), GetMaxHealth());
            Destroy(this.gameObject);
        }
    }

    //sets enemy type based on Id
    void SetEnemyType()
    {
        switch(enemyId)
        {
            //Easy Enemies
            case 0:
                speed = gameObject.GetComponent<NavMeshAgent>().speed = speedModifier;
                maxHealth = health = Mathf.RoundToInt(healthModifier * 100);
                bounty = Mathf.RoundToInt(bountyModifier * 10);
                damage = Mathf.RoundToInt(damageModifier * 10);
                armor = 2;
                break;
            case 1:
                speed = gameObject.GetComponent<NavMeshAgent>().speed = 1.5f * speedModifier;
                maxHealth = health = Mathf.RoundToInt(healthModifier * 75);
                bounty = Mathf.RoundToInt(bountyModifier * 7);
                damage = Mathf.RoundToInt(damageModifier * 10);
                abilities.Add("light");
                break;
            case 2:
                speed = gameObject.GetComponent<NavMeshAgent>().speed = .5f * speedModifier;
                maxHealth = health = Mathf.RoundToInt(healthModifier * 200);
                bounty = Mathf.RoundToInt(bountyModifier * 13);
                damage = Mathf.RoundToInt(damageModifier * 10);
                armor = 4;
                abilities.Add("heavy");
                break;
            //Regenerator
            case 3:
                speed = gameObject.GetComponent<NavMeshAgent>().speed = speedModifier;
                maxHealth = health = Mathf.RoundToInt(healthModifier * 100);
                bounty = Mathf.RoundToInt(bountyModifier * 10);
                damage = Mathf.RoundToInt(damageModifier * 10);
                armor = 2;
                abilities.Add("regenerate");
                abilities.Add("light");
                break;
            //Turtle
            case 4:
                speed = gameObject.GetComponent<NavMeshAgent>().speed = .5f * speedModifier;
                maxHealth = health = Mathf.RoundToInt(healthModifier * 100);
                bounty = Mathf.RoundToInt(bountyModifier * 10);
                damage = Mathf.RoundToInt(damageModifier * 10);
                armor = 100;
                abilities.Add("shell");
                abilities.Add("heavy");
                break;
            //Warleader
            case 5:
                speed = gameObject.GetComponent<NavMeshAgent>().speed = .7f * speedModifier;
                maxHealth = health = Mathf.RoundToInt(healthModifier * 150);
                bounty = Mathf.RoundToInt(bountyModifier * 20);
                damage = Mathf.RoundToInt(damageModifier * 20);
                armor = 3;
                abilities.Add("divine");
                abilities.Add("warcry");
                break;
        } 
    }

    public void OnDestroy()
    {
        spawner.GetComponent<SpawnTester>().EnemyCountDecrement();
    }

    //returns enemy damage
    public int GetDamage()
    {
        return damage;
    }

    //returns how much health enemy has left
    public int GetHealth()
    {
        return health;
    }

    //returns max health enemy could have
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    //Deals damage to enemy, if health is reduceded to 0 enemy is destroyed and player recieves income
    public void SetHealth(int damage, string type = "normal")
    {

        //adjust damage based off of enemies armor and possible defence type
        damage = Mathf.RoundToInt(damage * DefenceDamageMultiplier(type));
        damage -= armor;
        if(damage < 1)
        {
            damage = 1;
        }

        health -= damage;

        if (health <= 0)
        {
            econCont.GetComponent<EconomyController>().Income(GetBounty(), GetMaxHealth());
            Destroy(this.gameObject);   
        }

        else if (health <= maxHealth - 10 && GetComponent<EnemyController>().abilities.Contains("shell"))
        {
            GetComponent<EnemyController>().abilities.Remove("shell");
            speed = gameObject.GetComponent<NavMeshAgent>().speed = 1.5f * speedModifier;
            armor = 0;
        }

        else if (health <= maxHealth / 2 && GetComponent<EnemyController>().abilities.Contains("warcry"))
        {
            GetComponent<EnemyController>().abilities.Remove("warcry");
            Warcry();
        }
    }

    //returns how much you get for destroying enemy without any recyclers
    public int GetBounty()
    {
        return bounty;
    }

    //Used to find player buildings to attack
    public Transform FindTarget()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Building");
        float minDistance = Mathf.Infinity;
        Transform closest;

        if (candidates.Length == 0) { return null; }

        closest = candidates[0].transform;
        for (int i = 1; i < candidates.Length; ++i)
        {
            float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                closest = candidates[i].transform;
                minDistance = distance;
            }
        }
        return closest;
    }

    //used to move towards found player buildings to attack
    void AttackTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.deltaTime);
            target = FindTarget();
        }
    }

    //Destoys enemy and deals damage to building
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            other.gameObject.GetComponent<BuildingController>().SetHealth(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Destination"))
        {
            seekAndDestroy = true;
        }
    }

    //Used to determine which "ai" is used
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Destination") && seekAndDestroy == false)
        {
            seekAndDestroy = true;
        }
    }

    //Finds nearby enemies and gives then a speed boost
    private void Warcry()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 15);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                hitColliders[i].gameObject.GetComponent<EnemyController>().speed = hitColliders[i].gameObject.GetComponent<EnemyController>().speed * 1.25f;
                hitColliders[i].gameObject.GetComponent<NavMeshAgent>().speed = hitColliders[i].gameObject.GetComponent<NavMeshAgent>().speed * 1.25f;
                if (hitColliders[i].gameObject.GetComponent<EnemyController>().speed >= 4.0f * speedModifier)
                {
                    hitColliders[i].gameObject.GetComponent<EnemyController>().speed = 4.0f * speedModifier;
                    hitColliders[i].gameObject.GetComponent<NavMeshAgent>().speed = 4.0f * speedModifier;
                }
            }
            i++;
        }
    }

    //Calculates multiplier for damage based on attack and defence type
    private float DefenceDamageMultiplier(string attack)
    {
        if (attack == "piercing" && GetComponent<EnemyController>().abilities.Contains("light"))
        {
            return 1.5f;
        }
        else if (attack == "piercing" && GetComponent<EnemyController>().abilities.Contains("heavy"))
        {
            return 0.75f;
        }
        else if (attack == "crushing" && GetComponent<EnemyController>().abilities.Contains("light"))
        {
            return 0.75f;
        }
        else if (attack == "crushing" && GetComponent<EnemyController>().abilities.Contains("heavy"))
        {
            return 1.5f;
        }
        else if (attack != "chaos" && GetComponent<EnemyController>().abilities.Contains("divine"))
        {
            return 0.5f;
        }
        else
        {
            return 1.0f;
        }
    }

    //Modifies enemy stats based on difficulty
    public void SetDifficulty(float h, float d, float b, float s)
    {
        healthModifier = h;
        damageModifier = d;
        //bountyModifier = b;
        speedModifier = s;
    }

    public void ModifySpeed(float modifier)
    {
        speed = gameObject.GetComponent<NavMeshAgent>().speed * modifier;
        gameObject.GetComponent<NavMeshAgent>().speed = gameObject.GetComponent<NavMeshAgent>().speed * modifier;
    }

}


