using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public Text debugText;
    public GameObject marker;

    Ray lastRay;
    PhantomMotor phantom;

	// Use this for initialization
	void Start () {
        phantom = GameObject.Find("ShadowMaster").GetComponent<PhantomMotor>();
	}
	
	// Update is called once per frame
	void Update () {
	    //if (Input.GetButtonUp("LeftMouse"))
     //   {
     //       OnLeftClick();
     //   }

     //   OnLeftClick();

        if (debugText)
        {
            Vector3 cursorPos = Input.mousePosition;
            debugText.text = cursorPos.ToString();
        }

        Debug.DrawLine(lastRay.origin, lastRay.origin + lastRay.direction * 100, Color.red);
    }

    void MyDebugLine(Ray testRay)
    {
        for (int i = 0; i < 100; i ++)
        {
            Vector3 pos = testRay.origin + i * testRay.direction * 0.03f;
            CreateSphereAt(pos, 0.005f);
        }
    }

    void CreateSphereAt(Vector3 pos, float size = 0.03f)
    {
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(size, size, size);
        sphere.transform.position = pos;
        sphere.GetComponent<Collider>().isTrigger = true;
    }

    void OnLeftClick()
    {
        Vector3 cursorPos = Input.mousePosition;
        //cursorPos.y = Screen.height - cursorPos.y;
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        lastRay = ray;
        RaycastHit hitInfo;
        //MyDebugLine(ray);
        if (Physics.Raycast(ray, out hitInfo))
        {
            //CreateSphereAt(hitInfo.point);
            if (marker)
            {
                marker.transform.position = hitInfo.point;
            }
        }
    }

    public void OnTriggerPressed()
    {
        AdoptPhantomsView();
    }

    public void OnPadPressed()
    {
        phantom.activated = !phantom.activated;
    }

    void AdoptPhantomsView()
    {
        //if (phantom.activated)
        {


            var cameraRig = Camera.main.transform.parent;
            var phantomTrans = phantom.transform;
            var camera = Camera.main.transform;

            var rigPos = phantomTrans.position - camera.localPosition;
            rigPos.y = 0;
            cameraRig.position = rigPos;


            cameraRig.rotation = phantomTrans.rotation * Quaternion.Inverse(camera.localRotation);
            phantom.activated = false;
        }
    }
}
