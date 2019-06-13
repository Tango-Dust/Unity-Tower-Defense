using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{

    public Color hoverColor;
    

    private Color startColor;
    private Renderer rend;
    private GameObject turret;

    BuildManager buildManager;

    void Start()
    {

        //records starting color and renders object
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        
        
    }

    //checks if there is a turret in a node
    private void OnMouseDown()
    {

        if(turret !=null)
        {
            Debug.Log("CANNOT BUILD");
            return;
        }
        
        //builds turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
    }

    void OnMouseEnter()
    {
       

        //checks if there is a turret in a node
        if (turret != null)
        {
            //if so creates a log saying cant build
            Debug.Log("CANNOT BUILD");

        }
        else
        {
            //renders object again
            rend = GetComponent<Renderer>();

            //on hover changes color and enables sight of object
            rend.material.color = hoverColor;
            GetComponent<MeshRenderer>().enabled = true;

        }

    }

    void OnMouseExit()
    {
        //returns object to start color and makes it invisible again
        rend.material.color = startColor;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
