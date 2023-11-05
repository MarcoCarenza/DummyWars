using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance { get; private set;} //can only be set by this class, but taken from any

    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitPlaneLayerMask;


    private void Awake()
    {
        if (Instance != null)
        { 
            Debug.LogError("There's multiple UAS! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
         } 
        
        Instance = this;
    }

    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
        }


        if (Input.GetMouseButtonDown(1))
        {
            selectedUnit.GetMoveAction().Move(MouseWorld.GetPosition());
        }

        if (Input.GetKeyDown("tab"))
        {
            
        }

    }
        

    private bool TryHandleUnitSelection()
    {
        //Debug.Log(Input.mousePosition); shows where camera is pointing at
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//this is the mouse pointer, sends ray from camera to object and says the position in world. we are creating a ray and the ray takes the mouse poisition based on where it is on the screen and send it back.
        if  (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitPlaneLayerMask))//this shows the actual value of where the mouse is, as it gives the coordinates.
                                                                                                 //out makes it that the function writes on the variable, and the funciton shows where the mouse is.
                                                                                                 //float max makes it so that the ray can be as long as possible
                                                                                                 // 
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
                                                                                                      
        return false;   
    }


    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty); //this fires off an event telling the unit indicators to turn off
        /* ^ does below.
        if (OnSelectedUnitChanged != null)
        {
            OnSelectedUnitChanged(this, EventArgs.Empty);
        }*/
       
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
