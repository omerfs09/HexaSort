using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public LayerMask deskSlotLayer;
    public LayerMask slotLayer;
    Camera mainCamera;
    Vector3 initialPos;
    Queue<CheckNeighborsCommand> commands = new();
    bool putEnabled = true;

    void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }
    DraggableStack currentDraggable;
    DeskSlot currentDeskSlot;
    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.origin+ray.direction);
            if (Physics.Raycast(ray, out hit, deskSlotLayer))
            {
                currentDeskSlot = hit.collider.GetComponent<DeskSlot>();
                DraggableStack draggable;
                if (currentDeskSlot == null) draggable = null;
                else draggable = currentDeskSlot.stack;
                if(draggable != null)
                {
                    if(currentDraggable == null)
                    {
                        currentDraggable = draggable;
                        initialPos = draggable.transform.position;
                        
                    }
                    
                }
                
            }
            
        }
        else if (Input.GetMouseButtonUp(0) && currentDraggable != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            HexagonSlot slot = null;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, slotLayer))
            {
                slot = hit.collider.GetComponent<HexagonSlot>();

                
            }
            
            if(slot == null || !slot.IsEmpty() || !slot.isAvailable)
            {
                currentDraggable.transform.position = initialPos;
            }
            else
            {

                
                
                currentDraggable.AddToSlot(slot);
                currentDeskSlot.stack = null;
                currentDeskSlot.GetDesk().OnAStackRemoved();
            }
            ReleaseObject();

        }
        if(currentDraggable != null)
        currentDraggable.Drag(mainCamera.ScreenToWorldPoint(Input.mousePosition)+ mainCamera.transform.forward*3);

        void ReleaseObject()
        {
            currentDraggable = null;
        }
        if (commands.Count > 0 && commands.Peek().IsRunnable())
        {
            commands.Dequeue().RunCommand();
        }
    }
    public void AddToQueue(CheckNeighborsCommand command)
    {
        commands.Enqueue(command);
    }
}
