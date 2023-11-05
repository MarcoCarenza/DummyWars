using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{

    [SerializeField] private Animator unitAnimator;
    [SerializeField] float rotateSpeed = 10f; //speed of unit rotation
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }

    private void Update()
    {

        float stoppingDistance = .1f;//so the model doesnt bounce back and forth

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed); //makes the model rotate to where its walking, rotates model from forward to clicked point

            unitAnimator.SetBool("IsWalking", true); //for animator to switch between walking and idle animation
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }

    }


    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
    
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) 
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }

}
