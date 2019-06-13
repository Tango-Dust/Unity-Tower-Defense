using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTurret : MonoBehaviour
{
    public static BuildTurret instance;
    public NodeUI UI;
    Dropdown mDrop;
    private Nodes selectedNode;

    // Use this for initialization
    void Awake()
    {
        //ensures there is only one buildmanager
        if (instance != null)
        {
            Debug.LogError("More than one BuildTurret in scene!");
        }
        instance = this;
    }

    //gives a public prefab slot to be used in unity inspector
    public GameObject turretPrefab;
    public GameObject turretPrefab2;
    public GameObject turretPrefab3;

    

    public void DropdownValueChanged(Dropdown change)
    {
        Debug.Log("YES");
        Debug.Log(change.value);
        if(change.value==0)
        {
            turretToBuild = turretPrefab;
        }
        else if(change.value==1)
        {
            turretToBuild = turretPrefab2;
        }
        else if(change.value==2)
        {
            turretToBuild = turretPrefab3;
        }
    }


    void Start()
    {
        //sets turret you are building to the turret prefab selected
        turretToBuild = turretPrefab;

        mDrop = GetComponent<Dropdown>();
    }

     void Update()
    {
        
    }

    //private object for the turret that will be built
    private GameObject turretToBuild;

    //gets turret for building
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectNode(Nodes node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        UI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        UI.Hide();
    }

    //sets buildable turret
    public void SetTurretToBuild(GameObject Turret)
    {
        turretToBuild = Turret;
        DeselectNode();

    }
}

