using UnityEngine;
using System.Collections;

public class CameraMouseController : MonoBehaviour {

    bool isDraggingCamera = false;
    Vector3 lastMousePosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
/*
        //Click and Drag
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayLength = (mouseRay.origin.y / mouseRay.direction.y);
        Vector3 hitPos = mouseRay.origin - (mouseRay.direction * rayLength);

        if (Input.GetMouseButtonDown(0))
        {
            isDraggingCamera = true;
            lastMousePosition = hitPos;
        }
        else if (Input.GetMouseButtonUp(0)) {
            isDraggingCamera = false;
        }

        if (isDraggingCamera) {
            Vector3 diff = lastMousePosition - hitPos;
            Camera.main.transform.Translate(diff, Space.World);
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            rayLength = (mouseRay.origin.y / mouseRay.direction.y);
            lastMousePosition = hitPos = mouseRay.origin - (mouseRay.direction * rayLength);
        }

        //ScollWheel zoom
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollAmount) > 0.01f) {
            int minHeight = 2;
            int maxHeight = 20;

            Vector3 dir = hitPos - Camera.main.transform.position ;
            Vector3 p = Camera.main.transform.position;
            if (scrollAmount > 0 || p.y < maxHeight)
            {
                Camera.main.transform.Translate(dir * scrollAmount, Space.World);
            }
            p = Camera.main.transform.position;
            if (p.y < minHeight) {
                p.y = minHeight;
            }
            if (p.y > maxHeight)
            {
                p.y = maxHeight;
            }
            Camera.main.transform.position = p;

            //change camera angle
            Camera.main.transform.rotation = Quaternion.Euler(Mathf.Lerp(30, 80, p.y/ maxHeight),
            Camera.main.transform.rotation.eulerAngles.y,
            Camera.main.transform.rotation.eulerAngles.z);
        }*/
	}
}
