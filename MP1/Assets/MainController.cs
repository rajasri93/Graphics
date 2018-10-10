using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        //left mouse click
        if (Input.GetMouseButton(0))
        {
            GameObject myPointerSphere = GameObject.Find("PointerSphere");
            if (myPointerSphere != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    myPointerSphere.transform.position = hit.point;
                }
                foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (obj.name == "cubeCreated" || obj.name == "sphereCreated" || obj.name == "cylinderCreated")
                    {
                        if(IsInsideTheVolume(obj.transform.position, hit.point)) {
                            Destroy(obj);
                        }
                    }

                }
            }
        }
    }

    bool IsInsideTheVolume(Vector3 goPos, Vector3 targetPos) {
        if((goPos.x >= targetPos.x-0.5f && goPos.x <= targetPos.x+0.5f) &&
           (goPos.y >= targetPos.y - 0.5f && goPos.y <= targetPos.y + 0.5f) &&
          (goPos.z >= targetPos.z - 0.5f && goPos.z <= targetPos.z + 0.5f)) {
            return true;
        }
        return false;
    }

}
