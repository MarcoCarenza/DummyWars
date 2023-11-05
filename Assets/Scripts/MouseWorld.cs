using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{

    [SerializeField] private LayerMask mousePlaneLayerMask; //this field is on the layermask for the code below

    private static MouseWorld instance;

    private void Awake() { instance = this; }

    private void Update()
    {
        transform.position = MouseWorld.GetPosition(); //this is so that we can get the mouse positin if we ever need it.
    }

    public static Vector3 GetPosition()
    {
        //Debug.Log(Input.mousePosition); shows where camera is pointing at
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//this is the mouse pointer, sends ray from camera to object and says the position in world. we are creating a ray and the ray takes the mouse poisition based on where it is on the screen and send it back.
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask); //this shows the actual value of where the mouse is, as it gives the coordinates.
                                                                                                         //out makes it that the function writes on the variable, and the funciton shows where the mouse is.
                                                                                                         //float max makes it so that the ray can be as long as possible
                                                                                                         // 
        return raycastHit.point;
    }
}
