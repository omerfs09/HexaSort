using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public LayerMask deskSlotLayer;
    public LayerMask slotLayer;
    public Vector3 cameraFocusPoint = Vector3.zero;
    Camera mainCamera;
    Vector3 initialPos;
    Vector3 previousMousePos;
    float cameraR;
    void Awake()
    {
        
        Instance = this;
        mainCamera = Camera.main;
        cameraR = mainCamera.transform.position.magnitude;
        startCameraSize = mainCamera.orthographicSize;
    }
    DraggableStack currentDraggable;
    DeskSlot currentDeskSlot;
    ControlState controlState = ControlState.RotateCamera;
    float startCameraSize;
    bool rotate = false;
    // Update is called once per frame
    void Update()
    {
            if (controlState == ControlState.RotateCamera)
            {
                RotateCamera();              
            }
            else if (controlState == ControlState.DragAndDrop)
            {
                DragAndDrop();
            }
            else if(controlState == ControlState.ClearSkill)
            {
                ClearSkill();

            }
            else if (controlState == ControlState.MoveSkill)
            {
                MoveSkill();
            }
        
        previousMousePos = Input.mousePosition;
    }
    public void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            controlState = ControlState.RotateCamera;
            Vector3 dif = Input.mousePosition - previousMousePos;
           
            mainCamera.transform.position = Quaternion.AngleAxis(dif.x, Vector3.up) * mainCamera.transform.position;
            mainCamera.transform.forward = - mainCamera.transform.position;
        }
        else
        {

            controlState = ControlState.DragAndDrop;
        }
    }
    public void DragAndDrop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.origin + ray.direction);
            if (Physics.Raycast(ray, out hit, deskSlotLayer))
            {
                currentDeskSlot = hit.collider.GetComponent<DeskSlot>();
                DraggableStack draggable;
                if (currentDeskSlot == null) draggable = null;
                else draggable = currentDeskSlot.stack;
                if (draggable != null)
                {
                    if (currentDraggable == null)
                    {
                        currentDraggable = draggable;
                        initialPos = draggable.transform.position;

                    }

                }

            }
            else
            {
                controlState = ControlState.RotateCamera;

            }

        }
        else if (Input.GetMouseButtonUp(0) && currentDraggable != null)
        {
            controlState = ControlState.DragAndDrop;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            HexagonSlot slot = null;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, slotLayer))
            {
                slot = hit.collider.GetComponent<HexagonSlot>();


            }

            if (slot == null || !slot.IsEmpty() || !slot.isAvailable)
            {
                currentDraggable.transform.position = initialPos;
            }
            else
            {
                SFXManager.Instance.PlayClipOneShot(AudioEnums.AddToSlot);
                CommandController.Instance.EnqueAddToSlotCommand(new AddToSlotCommand(currentDraggable, slot, currentDeskSlot));
                currentDraggable.transform.position = slot.transform.position;


            }
            ReleaseObject();

        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
        if (currentDraggable != null)
            currentDraggable.Drag(DragPoint());
        

        void ReleaseObject()
        {
            currentDraggable = null;
        }
    }
    public void ChangeControlState(ControlState state)
    {
        controlState = state;
    }
    public void ClearSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.origin + ray.direction);
            if (Physics.Raycast(ray, out hit, slotLayer))
            {
                HexagonSlot slot = hit.collider.GetComponent<HexagonSlot>();
                if(slot != null)
                {
                    Debug.LogWarning("Clearing");
                    slot.ClearSlot();
                    controlState = ControlState.DragAndDrop;
                    UIManager.HidePanel(PanelType.ClearSkillPanel);
                }
            }
        }
    }
    private DraggableStack draggable1 = null;
    private HexagonSlot sourceSlot = null;
    public void MoveSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.origin + ray.direction);
            if (Physics.Raycast(ray, out hit, slotLayer))
            {
                HexagonSlot slot = hit.collider.GetComponent<HexagonSlot>();

                if (slot != null && draggable1 == null )
                {

                    DraggableStack draggableStack = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
                    draggable1 = draggableStack;
                    List<Hexagon> hexagons = new();
                    while (slot.stack.Count > 0)
                    {
                        Hexagon hexagon = slot.PopObject();
                        hexagons.Add(hexagon);
                    }
                    draggable1.PushList(hexagons.Reverse<Hexagon>().ToList());
                    sourceSlot = slot;
                }

            }
        }
        else if (Input.GetMouseButtonUp(0) && draggable1 != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            HexagonSlot slot = null;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, slotLayer))
            {
                slot = hit.collider.GetComponent<HexagonSlot>();


            }

            if (slot == null || !slot.IsEmpty() || !slot.isAvailable )
            {
                //cancel 
                controlState = ControlState.DragAndDrop;
                draggable1.AddToSlot(sourceSlot);
                draggable1 = null;
            }
            else
            {
                controlState = ControlState.DragAndDrop;
                draggable1.AddToSlot(slot);
                draggable1 = null;

            }
            controlState = ControlState.DragAndDrop;
            UIManager.HidePanel(PanelType.MoveSkillPanel);
        }

        if (draggable1 != null)
        {
            draggable1.Drag(DragPoint());
        }
    }
    public Vector3 DragPoint()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition) + mainCamera.transform.forward * 3;
    }
}

public enum ControlState
{
    DragAndDrop,
    RotateCamera,
    ClearSkill,
    MoveSkill,
    DesteKar
}