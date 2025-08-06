using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class HexagonSlot : MonoBehaviour, IPoolable
{
    private Stack<Hexagon> stack = new Stack<Hexagon>();
    static float STACK_SPACE = GameConstants.STACK_SPACE;
    private float stackHeight = GameConstants.STACK_SPACE;
    public List<HexagonSlot> connectedSlots;
    public bool isAvailable = true;
    public static bool addToSlotEnabled = true;
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Stack<Hexagon> GetStack()
    {
        return stack;
    }
    
    public void OnDrop()
    {

        Colors topColor;
        if (stack.Count > 0)
            topColor = stack.Peek().color;
        else topColor = Colors.Null;

        if (topColor != Colors.Null)
        {
            CheckNeighbors(topColor, () => {
                if(CommandController.Instance.clearQue.Count > 0)
                {
                    CommandController.Instance.RunClearQueue();
                }
                else
                {
                    GameStats.Instance.CheckGameOver();
                }
                
            });
            GameStats.Instance.moves++;
        }


    }
    public void CheckNeighbors(Colors color1, Action onComplete)
    {

        if (color1 == Colors.Null)
        {
            color1 = GetTopColor();
        }
        Colors color = color1;
        isAvailable = false;

        List<HexagonSlot> available = new();
        foreach (HexagonSlot slot in connectedSlots)
        {

            if (slot.GetTopColor() == color && slot.GetTopColor() != Colors.Null && slot.isAvailable)
            {
                available.Add(slot);
            }
        }
        if (available.Count > 0)
        {
            if (available.Count < 2)
            {
                if (available[0].IsSingleColor() && !IsSingleColor())
                {
                    Debug.Log("SingleColorMatch");
                    isAvailable = true;
                    available[0].CheckNeighbors(color, onComplete);
                }
                else
                {
                    Debug.Log("Tekli");
                    available[0].isAvailable = false;
                    available[0].PourToSlot(this, color, () => available[0].CheckNeighbors(Colors.Null, () => end()));

                }
            }
            else
            {
                Debug.Log("Ciftli");
                available[0].isAvailable = false;
                available[0].PourToSlot(this, color, () => CheckNeighbors(GetTopColor(), () => available[0].CheckNeighbors(Colors.Null, () => onComplete?.Invoke())));
            }


        }
        else
        {
            Debug.Log("No Avaliable");
            //CheckMatch(null);
            isAvailable = true;
            onComplete?.Invoke();
        }
        void end()
        {
            //CheckMatch(()=> { isAvailable = true;onComplete?.Invoke(); });
            //addToSlotEnabled = true;
            CheckMatch(null);
            isAvailable = true;
            onComplete?.Invoke();

        }


    }


    public void PourToSlot(HexagonSlot other, Colors color, Action onComplete)
    {
        addToSlotEnabled = false;
        isAvailable = false;
        other.isAvailable = false;
        int i = 0;
        float totalTime = 0.5f;
        int colorSeries = GetColorSeries();
        SFXManager.Instance.PlayClipOneShot(AudioEnums.Pour);
        while (stack.Count > 0 && color == stack.Peek().color)
        {
            Hexagon hexagon = stack.Pop();
            other.PushObject(hexagon, false);
            stackHeight -= GameConstants.STACK_SPACE;
            
            hexagon.transform.DOJump(other.transform.position + (other.stackHeight - STACK_SPACE) * Vector3.up, 0.1f, 1, 0.15f).SetDelay(i * totalTime / colorSeries);
            Vector3 rotateDirection;
            rotateDirection = other.transform.position - hexagon.transform.position;
            rotateDirection =  Vector3.Cross(rotateDirection.normalized,Vector3.up).normalized;
            Quaternion quaternion = hexagon.transform.rotation * Quaternion.AngleAxis(180f, rotateDirection);
            hexagon.transform.DORotate(quaternion.eulerAngles, 0.15f).SetDelay(i * totalTime / colorSeries);
            i++;
        }
        SFXManager.Instance.HapticLow();
        float wait = totalTime + 0.1f;
        StartCoroutine(cor());
        IEnumerator cor()
        {
            yield return new WaitForSeconds(wait);
            isAvailable = true;
            other.isAvailable = true;
            //stackHeight = GameConstants.STACK_SPACE;
            addToSlotEnabled = true;
            onComplete?.Invoke();
        
        }
        
    }

    public void CheckMatch(Action action)
    {

        if (TopThreeAreEqual(stack))
        {
            Debug.Log("Check", this);
            clearedSlots++;
            CommandController.Instance.clearQue.Enqueue(new ClearSlotCommand(this, action));
        }
        else
        {
            Debug.Log("Doesnt Match!");
        }
    }

    public bool IsEmpty()
    {
        if (stack.Count <= 0)
        {
            return true;
        }
        return false;
    }
    public bool TopThreeAreEqual(Stack<Hexagon> stack)
    {
        return 10 <= GetColorSeries();

    }
    private static int clearedSlots = 0;
    public void ClearSlot(Colors color = Colors.Null, Action onComplete = null)
    {
        isAvailable = false;
        float totalTime = 1;
        int colorSeries = GetColorSeries();
        int i = 0;
        string clearString = "";
        while (stack.Count > 0 && stack.Peek().color == color)
        {
            clearString += stack.Peek().color.ToString() + ",";
            stack.Pop().transform.DOScale(Vector3.zero, 0.15f).SetDelay(i * totalTime / colorSeries).SetEase(Ease.InOutBack);
            stackHeight += -GameConstants.STACK_SPACE;
            i++;
            
        }
        GameStats.Instance.AddColor(color, -i);
        GameStats.Instance.ChangeProggress(i);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(totalTime);
            ParticleSystem p = VFXManager.Instance.GetParticle(VFXEnums.ClearSlotVFX2, transform.position);
            SFXManager.Instance.PlayClipOneShot(AudioEnums.ClearSlot);
            yield return new WaitForSeconds(0.5f);
            //CheckNeighbors(GetTopColor(),null);
            Debug.Log(clearString + i.ToString(), this);
            isAvailable = true;
            CheckNeighbors(GetTopColor(), () => { onComplete?.Invoke(); CommandController.Instance.RunClearQueue(); });
            clearedSlots--;
            if (clearedSlots <= 0)
            {
                OnAllAnimationsEnded();
                clearedSlots = 0;
            }
            SFXManager.Instance.HapticMedium();
        }
    }
    public void ClearSlotSkill()
    {
        isAvailable = false;
        float totalTime = 0.5f;
        int colorSeries = GetColorSeries();
        int i = 0;
        string clearString = "";
        while (stack.Count > 0)
        {
            GameStats.Instance.AddColor(stack.Peek().color, -1);
            clearString += stack.Peek().color.ToString() + ",";
            Hexagon hexagon = stack.Pop();
            hexagon.transform.DOScale(Vector3.zero, 0.15f).SetDelay(i * totalTime / colorSeries).SetEase(Ease.InOutBack);
            hexagon.transform.DOMove(transform.position, 0.15f).SetDelay(i * totalTime / colorSeries);
            stackHeight += -GameConstants.STACK_SPACE;
            i++;
        }

        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(totalTime + 0.5f);
            //CheckNeighbors(GetTopColor(),null);
            Debug.Log(clearString + i.ToString(), this);
            isAvailable = true;
            ParticleSystem particleSystem = VFXManager.Instance.GetParticle(VFXEnums.ClearSlotVFX,gameObject.transform.position);
            particleSystem.Play();
        }
    }
    public void OnAllAnimationsEnded()
    {
        if (GameStats.Instance.CheckLevelComplete())
        {

        }
        else
        GameStats.Instance.CheckGameOver();
    }
    public void Test()
    {
        foreach (HexagonSlot slot in connectedSlots)
        {
            Hexagon hexagon = PoolManager.Instance.GetItem(ItemType.Hexagon) as Hexagon;
            hexagon.MoveTo(slot.transform.position);
        }

    }
    public int GetColorSeries()
    {

        int num = 0;
        Colors firstColor = GetTopColor();
        Stack<Hexagon> tempList = new();
        if (firstColor == Colors.Null) return 0;
        while (stack.Count > 0 && firstColor == stack.Peek().color)
        {
            Hexagon poped = stack.Pop();
            if (poped.color == firstColor)
            {
                tempList.Push(poped);
                num++;
            }
            else
            {
                break;
            }
        }
        foreach (Hexagon item in tempList)
        {
            stack.Push(item);
        }
        return num;

    }
    public bool IsSingleColor()
    {
        if (GetColorSeries() == stack.Count) return true;
        else return false;
    }
    public void PushObject(Hexagon hexa, bool move = true)
    {
        stack.Push(hexa);
        if (move)
            hexa.MoveTo(transform.position + Vector3.up * stackHeight, false);
        stackHeight += STACK_SPACE;
    }
    public Hexagon PopObject()
    {
        Hexagon temp = null;
        if (stack.Count > 0)
        {
            temp = stack.Peek();
            stack.Pop();
            stackHeight += -STACK_SPACE;
        }
        else
        {
            OnPopDone();
        }
        return temp;
    }
    public void OnPopDone()
    {
        Debug.Log("Pop Is Done!");
    }

    public Colors GetTopColor()
    {
        if (stack.Count > 0) return stack.Peek().color;
        return Colors.Null;
    }

    public void OnSpawn()
    {

    }

    public void OnDespawn()
    {
        stack.Clear();
        connectedSlots.Clear();
        stackHeight = STACK_SPACE;
        isAvailable = true;
        addToSlotEnabled = true;
    }
}