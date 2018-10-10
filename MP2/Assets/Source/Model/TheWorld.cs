using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TheWorld : MonoBehaviour {

    public GameObject Pointer;
    public const float clickSize = 0.5f;
    public GameObject mSelected = null;
    public Text text = null;

    // Use this for initialization
    void Start () {
        Debug.Assert(Pointer!=null);
        text.text = "Selected:None";
	}
	
	// Update is called once per frame
	void Update () {
        if(mSelected != null) {
            Renderer r = mSelected.GetComponent<Renderer>();
            mSelected.GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.1f, 0.5f);
            Color color = r.material.color;
            color.a = 0.1f;
            r.material.color = color;
        }
    }

    public void SelectObjectAt(GameObject obj, Vector3 p)
    {
        if (obj.name == "CreationPlane")
        {
            p.y = clickSize / 2f;
            ResetSelectionRenderer(mSelected);
            mSelected = null;
            SetSelectedText();
        }
        else
        {
            if(mSelected!=null && mSelected == obj) {
                Debug.Log("OBJECT UNSELECTED!");
                ResetSelectionRenderer(mSelected);
                mSelected = null;
                SetSelectedText();
            } else {
                Debug.Log("OBJECT HERE TO BE SELECTED!");
                if(mSelected!=null) {
                    ResetSelectionRenderer(mSelected);
                }
                mSelected = obj;
                SetSelectedText();
            }
        }
    }

    public void ProcessUserSelection(string objType)
    {
        GameObject newObj = Instantiate(Resources.Load(objType)) as GameObject;
        if (mSelected == null) {
            newObj.GetComponent<Renderer>().material.color = Color.black;
            newObj.transform.position = new Vector3(-3, 1, -2);
        } else {
            newObj.transform.SetParent(mSelected.transform);
            Vector3 p = mSelected.transform.localPosition;
            p.y = 0.5f;
            newObj.transform.localPosition = p;
            if(newObj.transform.GetSiblingIndex()>0){
                Debug.Log("OTHER SIBILINGS PRESENT!");
                newObj.GetComponent<Renderer>().material.color = newObj.transform.parent.GetChild(0).GetComponent<Renderer>().material.color;
            } else {
                newObj.GetComponent<Renderer>().material.color = Color.white;
            }
            /*if (mSelected.transform.childCount>0){
                newObj.GetComponent<Renderer>().material.color = mSelected.GetComponent<Renderer>().material.color;
            } else {
                newObj.GetComponent<Renderer>().material.color = Color.white;
            }*/
        }
    }

    public void ResetSelectionRenderer(GameObject gameObject) {


        if(gameObject!=null) {
            if(!IsOriginalGameObject(gameObject)) {
                Renderer siblingRenderer = null;
                if (gameObject.transform.GetSiblingIndex() > 0)
                {
                    siblingRenderer = gameObject.transform.parent.GetChild(0).GetComponent<Renderer>();
                    gameObject.GetComponent<Renderer>().material.color = siblingRenderer.material.color;
                } else {
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
            } else {
                Color originalColor = GetOriginalObjectColor(gameObject);
                if (originalColor != null)
                {
                    gameObject.GetComponent<Renderer>().material.color = originalColor;
                }
            }
        }
    }

    public bool IsOriginalGameObject(GameObject gameObject) {
        if((gameObject.name == "GrandParent" )|| (gameObject.name == "Parent") || (gameObject.name == "Child")) {
            return true;
        }
        return false;
    }

    public Material GetOriginalObjectMaterial(GameObject gameObject) {
        Material m;
        if(gameObject!=null) {
            switch (gameObject.name)
            {
                case "GrandParent":
                    m = (Material)Resources.Load("GrandParentMaterial", typeof(Material));
                    
                    break;
                case "Parent":
                    m = (Material)Resources.Load("ParentMaterial", typeof(Material));
                    break;
                case "Child":
                    m = (Material)Resources.Load("ChildMaterial", typeof(Material));
                    break;
                default:
                    break;
            }
        }
        return null;
    }

    public Color GetOriginalObjectColor(GameObject gameObject)
    {
        Color c = new Color();
        if (gameObject != null)
        {
            switch (gameObject.name)
            {
                case "GrandParent":
                    c = new Color(0.18f, 0.49f, 0.72f);
                    break;
                case "Parent":
                    c = new Color(0.14f, 0.79f, 0.16f);
                    break;
                case "Child":
                    c = new Color(0.91f, 0.25f, 0.16f);
                    break;
                default:
                    break;
            }
        }
        return c;
    }

    public void SetSelectedText() {
        if(mSelected !=null) {
            text.text = "Selected:" + mSelected.name;
        } else {
            text.text = "Selected:None";
        }

    }

    public float GetSelectedPosition()
    {
        return mSelected.transform.position.x;
    }

    public void SetSelelectedPosition(Vector3 p)
    {
        if (mSelected != null)
            mSelected.transform.position = p;
    }
}
