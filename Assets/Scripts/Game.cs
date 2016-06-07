using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public Text debugText;
    public GameObject marker;
    public TweenAlpha tweener;

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
        if (tweener.GetState() == TweenAlpha.State.In || tweener.GetState() == TweenAlpha.State.Out)
        {
            phantom.StopImmediately();
            phantom.SetMovable(false);
            tweener.StartFadeIn(0.5f, AdoptPhantomsView);
        }
    }

    public void OnPadPressed()
    {
        phantom.activated = !phantom.activated;
    }

    void AdoptPhantomsView()
    {
        var cameraRig = Camera.main.transform.parent;
        var phantomTrans = phantom.transform;
        var camera = Camera.main.transform;


        cameraRig.rotation = phantomTrans.rotation * Quaternion.Inverse(camera.localRotation);
        phantom.activated = false;

        var relativePos = cameraRig.TransformPoint(camera.localPosition) - cameraRig.position;

        var targetPos = phantomTrans.position;
        targetPos.y = 2.25f;
        var rigPos = targetPos + phantomTrans.forward*0.1f - relativePos;
        cameraRig.position = rigPos;

        tweener.StartFadeOut(0.5f, () => phantom.SetMovable(true));
    }
}
