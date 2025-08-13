using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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
    [SerializeField] private GameObject slotsParent;
    private Vector3 cameraStartPoint;
    void Awake()
    {
        Input.multiTouchEnabled = false;
        Instance = this;
        mainCamera = Camera.main;
        cameraR = mainCamera.transform.position.magnitude;
        cameraStartPoint = mainCamera.transform.position;
    }
    DraggableStack currentDraggable;
    DeskSlot currentDeskSlot;
    ControlState controlState = ControlState.RotateCamera;
    // Update is called once per frame
    public void FixedUpdate()
    {
        Rotate(0.6f);
    }
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
            else if(controlState == ControlState.InActive)
            {

            }
        
        previousMousePos = Input.mousePosition;
    }
    public void ResetCameraRotation()
    {
        angleZ = -90;
        mainCamera.transform.position = cameraStartPoint;
        mainCamera.transform.rotation = Quaternion.Euler(new Vector3(60, 270 - angleZ, 0));
        
    }
    float angleZ = -90;
    float defaultCameraSize = 4;
    public void SetDefaultCameraSize(float size)
    {
        defaultCameraSize = size;
    }
    private void Rotate(float lerpSpeed)
    {
        float r = new Vector3(0, -cameraStartPoint.z, 0).magnitude;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,new Vector3(Mathf.Cos(angleZ * Mathf.Deg2Rad) * r, cameraStartPoint.y, Mathf.Sin(angleZ * Mathf.Deg2Rad) * r),Time.deltaTime*60*lerpSpeed);
        mainCamera.transform.rotation = Quaternion.Lerp( mainCamera.transform.rotation ,Quaternion.Euler(new Vector3(60, 270 - angleZ, 0)),Time.deltaTime*60*lerpSpeed);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize , ((((int)angleZ + 90) / 60) % 3 == 0 ? 1f : 1.41f) * defaultCameraSize, Time.deltaTime * 60 * lerpSpeed *0.2f); ;
    }
    
    public void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            controlState = ControlState.RotateCamera;
            Vector3 dif = Input.mousePosition - previousMousePos;
            angleZ -= dif.x*Time.deltaTime*60*0.3f;
            
        }
        else
        {
            controlState = ControlState.DragAndDrop;
            angleZ = Mathf.Round((angleZ + 90) / 60)*60 -90;
            float r = new Vector3(0, -cameraStartPoint.z, 0).magnitude;
            
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
                        SFXManager.Instance.PlayClipOneShot(AudioEnums.Lift);
                        currentDraggable = draggable;
                        initialPos = currentDeskSlot.transform.position;

                    }

                }
                else
                { controlState = ControlState.RotateCamera; 
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
                currentDraggable.MoveTo(initialPos);
            }
            else
            {
                SFXManager.Instance.HapticLow();
                SFXManager.Instance.PlayClipOneShot(AudioEnums.AddToSlot);
                new AddToSlotCommand(currentDraggable, slot, currentDeskSlot).RunCommand();
                currentDraggable.transform.position = slot.transform.position;
                slot.transform.DOScaleX(1.2f, 0.1f);
                slot.transform.DOScaleX(1, 0.1f).SetDelay(0.1f);

            }
            ReleaseObject();

        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
        if (currentDraggable != null)
            currentDraggable.DragAnimated(DragPoint());
        

        void ReleaseObject()
        {
            currentDraggable = null;
        }
    }
    public void ChangeControlState(ControlState state)
    {
        if(state == ControlState.InActive)
        {
            currentDraggable = null;
        }
        controlState = state;
    }
    [SerializeField] private Hammer hammerAnimated;
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
                if(slot != null && !slot.IsEmpty())
                {
                    
                    controlState = ControlState.DragAndDrop;
                    UIManager.HideClearSkillPanel();
                    LevelManager.Instance.ClearSkillCount--;
                    UIManager.UpdateSkills();
                    (UIManager.GetPanel(PanelType.BoostersPanel) as SkillPanel).SetClearSkillButton(false);
                    hammerAnimated.gameObject.SetActive(true);
                    hammerAnimated.transform.parent.rotation = Quaternion.Euler( new Vector3(0, mainCamera.transform.rotation.eulerAngles.y, 0));
                    Vector3 animationPos = mainCamera.ScreenToWorldPoint((Vector2)mainCamera.WorldToScreenPoint(slot.transform.position))  + ray.direction*2;
                    ChangeControlState(ControlState.InActive);
                    hammerAnimated.transform.parent.position = animationPos;
                    hammerAnimated.action = () => slot.ClearSlotSkill();
                    hammerAnimated.GetComponent<Animator>().SetTrigger("Hit");

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

                if (slot != null && draggable1 == null  && !slot.IsEmpty())
                {

                    DraggableStack draggableStack = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
                    draggable1 = draggableStack;
                    draggable1.Drag(DragPoint());
                    List<Hexagon> hexagons = new();
                    while (slot.GetStack().Count > 0)
                    {
                        Hexagon hexagon = slot.PopObject();
                        hexagons.Add(hexagon);
                    }
                    draggable1.PushList(hexagons.Reverse<Hexagon>().ToList());
                    sourceSlot = slot;
                    draggableStack.SetAllHexShadowless();
                    SFXManager.Instance.PlayClipOneShot(AudioEnums.Lift);
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
                LevelManager.Instance.MoveSkillCount--;
                UIManager.UpdateSkills();
                SFXManager.Instance.PlayClipOneShot(AudioEnums.AddToSlot);

            }
            controlState = ControlState.DragAndDrop;
            UIManager.HideMoveSkillPanel();
        }

        if (draggable1 != null)
        {
            draggable1.DragAnimated(DragPoint());
        }
    }
    public Vector3 DragPoint()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition) + mainCamera.transform.forward * 6;
    }
}

public enum ControlState
{
    DragAndDrop,
    RotateCamera,
    ClearSkill,
    MoveSkill,
    DesteKar,
    InActive,
}