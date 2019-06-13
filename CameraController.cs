using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    //variables for edge panning
    public float pan_speed;
    public float pan_border_thickness;
    public Vector2 minimum_pan_value;
    public Vector2 maximum_pan_value;

    //variables for scroll wheel zoom
    public float scroll_speed;
    public float scroll_min_y;
    public float scroll_max_y;

    // Use this for initialization
    void Start ()
    {

        //determine what is the current scene
        Scene cur_scene = SceneManager.GetActiveScene();

        //set the properties of the camera depending on the scene being loaded.
        if (cur_scene.name == "Game Map - Prototype")
        {
            
            //set boundaries for the camera
            minimum_pan_value = new Vector2(-100, 0);
            maximum_pan_value= new Vector2(500, 375);
            
            //set edge panning values
            pan_speed = 250f;
            pan_border_thickness = 10f;

            //set scroll wheel zoom limits
            scroll_min_y = 100f;
            scroll_max_y = 300f;
            scroll_speed = 4000f;

        }
        else if (cur_scene.name == "Arabian Nights")
        {

            //set boundaries for the camera
            minimum_pan_value = new Vector2(550, 480);
            maximum_pan_value = new Vector2(940, 942);

            //set edge panning values
            pan_speed = 250f;
            pan_border_thickness = 10f;

            //set scroll wheel zoom limits
            scroll_min_y = 100f;
            scroll_max_y = 230f;
            scroll_speed = 4000f;

            transform.eulerAngles = new Vector3(60, 0, 0);
        }
        else if(cur_scene.name == "Wuotan's Divine Abode")
        {
            //set boundaries for the camera
            minimum_pan_value = new Vector2(600, 400);
            maximum_pan_value = new Vector2(900, 850);

            //set edge panning values
            pan_speed = 250f;
            pan_border_thickness = 10f;

            //set scroll wheel zoom limits
            scroll_min_y = 100f;
            scroll_max_y = 180f;
            scroll_speed = 4000f;

            //set rotation of camera
            transform.eulerAngles = new Vector3(40, 0, 0);
        }
    }


    // Update is called once per frame
    void Update ()
    {

        Vector3 pos = transform.position;

        /*
         * check if the user is using either the keyboard
         * or edge panning to move the camera. If so,
         * update the position of the camera.
        */
        if (Input.GetKey("w") || (Input.mousePosition.y >= (Screen.height - pan_border_thickness)))
        {
            pos.z += pan_speed * Time.deltaTime;
        }
        if (Input.GetKey("s") || (Input.mousePosition.y <= pan_border_thickness))
        {
            pos.z -= pan_speed * Time.deltaTime;
        }
        if (Input.GetKey("d") || (Input.mousePosition.x >= (Screen.width - pan_border_thickness)))
        {
            pos.x += pan_speed * Time.deltaTime;
        }
        if (Input.GetKey("a") || (Input.mousePosition.x <= pan_border_thickness))
        {
            pos.x -= pan_speed * Time.deltaTime;
        }

        /*
         * modifies the zoom of the camera based on mouse scrolling
        */
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scroll_speed * Time.deltaTime;

        /*
            Ensures that the position of the camera never exceeds boundaries 
        */ 
        pos.x = Mathf.Clamp(pos.x, minimum_pan_value.x, maximum_pan_value.x);
        pos.y = Mathf.Clamp(pos.y, scroll_min_y, scroll_max_y);
        pos.z = Mathf.Clamp(pos.z, minimum_pan_value.y, maximum_pan_value.y);

        /*
            finally update the position of the camera after all input has been
            handled.
        */
        transform.position = pos;
    }
}
