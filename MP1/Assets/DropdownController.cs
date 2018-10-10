using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour {

    public Dropdown myDropdown;

    public bool cubeMovingUp = true;
    public bool sphereMovingRight = true;
    public bool cylinderMovingForward = true;


    // Use this for initialization
    void Start () {
        myDropdown = GetComponent<Dropdown>();
        myDropdown.onValueChanged.AddListener(delegate {
            OnDDValueChange(myDropdown);
        });
    }
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindObjectsOfType<GameObject>().Length > 0)
        {
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
                switch (obj.name)
                {
                    case "cubeCreated":
                        CubeBehaviour(obj);
                        break;
                    case "sphereCreated":
                        SphereBehaviour(obj);
                        break;
                    case "cylinderCreated":
                        CylinderBehaviour(obj);
                        break;
                    default:
                        break;
                }
            }
        }

    }

    void OnDDValueChange (Dropdown selected) {
        switch (selected.value)
        {
            case 1:
                GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myCube.name = "cubeCreated";
                myCube.transform.position = GetTargetLocation();
                myCube.transform.localScale = new Vector3(1, 1, 1);
                myCube.transform.rotation = Quaternion.Euler(0, 90, 0);
                myCube.transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
                Renderer cubeRenderer = myCube.GetComponent<Renderer>();
                cubeRenderer.material.color = new Color(1, 1, 1);
                break;
            case 2:
                GameObject mySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                mySphere.name = "sphereCreated";
                mySphere.transform.position = new Vector3(GetTargetLocation().x, GetTargetLocation().y + 0.1f, GetTargetLocation().z);
                mySphere.transform.localScale = new Vector3(1, 1, 1);
                mySphere.transform.Translate(Vector3.forward * Time.deltaTime);
                Renderer sphereRenderer = mySphere.GetComponent<Renderer>();
                sphereRenderer.material.color = new Color(1, 1, 1);
                break;
            case 3:
                GameObject myCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                myCylinder.name = "cylinderCreated";
                myCylinder.transform.position = new Vector3(GetTargetLocation().x, GetTargetLocation().y+1f, GetTargetLocation().z);
                myCylinder.transform.localScale = new Vector3(1, 2, 1);
                myCylinder.transform.Translate(Vector3.left * Time.deltaTime);
                Renderer cylinderRenderer = myCylinder.GetComponent<Renderer>();
                cylinderRenderer.material.color = new Color(1, 1, 1);
                break;
            default:
                break;
        }
    }

    public void CubeBehaviour(GameObject gameObject) {

        gameObject.transform.rotation *= Quaternion.Euler(0, 90f * Time.deltaTime, 0);

        Renderer r = gameObject.GetComponent<Renderer>();

        if (gameObject.transform.position.y >= 5)
        {
            cubeMovingUp = false;

        }
        else if (gameObject.transform.position.y <= 0)
        {
            cubeMovingUp = true;
        }
        if(cubeMovingUp){
            gameObject.transform.Translate(Vector3.up * Time.deltaTime);
            r.material.color = new Color(1, 1, 1);
        }
        else{
            gameObject.transform.Translate(Vector3.down * Time.deltaTime);
            r.material.color = new Color(1, 0, 1);
        }
    }

    public void SphereBehaviour(GameObject gameObject)
    {
        Renderer r = gameObject.GetComponent<Renderer>();

        if (gameObject.transform.position.x >= 5)
        {
            sphereMovingRight = false;
        }
        else if (gameObject.transform.position.x <= 0)
        {
            sphereMovingRight = true;
        }

        if (sphereMovingRight)
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime);
            r.material.color = new Color(1, 1, 1);
        }
        else
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime);
            r.material.color = new Color(0, 1, 1);
        }
    }

    public void CylinderBehaviour(GameObject gameObject)
    {
        Renderer r = gameObject.GetComponent<Renderer>();

        if (gameObject.transform.position.z >= 5)
        {
            cylinderMovingForward = false;
        }
        else if (gameObject.transform.position.z <= 0)
        {
            cylinderMovingForward = true;
        }
        if (cylinderMovingForward)
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
            r.material.color = new Color(1, 1, 1);
        }
        else
        {
            gameObject.transform.Translate(Vector3.back * Time.deltaTime);
            r.material.color = new Color(1, 1, 0);
        }
    }

    public Vector3 GetTargetLocation() {
        GameObject sphereObject = GameObject.Find("PointerSphere");
        return sphereObject != null ? sphereObject.transform.position : new Vector3(0, 0, 0);
    }

}
