using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    
    public const float STACK_SPACE = 0.15f;
    public const  float HEXAGON_R = 0.42f;
    public const  int MOVESKILL_PRICE = 100;
    public const  int CLEARSKILL_PRICE = 100;
    public const  int REFRESHDESK_PRICE = 100;
    public void Awake()
    {
        
    }
    public static Vector3 HexPosition(int col,int row,float r = HEXAGON_R)
    {
        
        if(col % 2 == 0)
        {
            return Vector3.right*col* r * 1.5f + Vector3.forward* -1.73f*row*r;
        }
        else
        {
            return Vector3.right * col * r * 1.5f + Vector3.forward * -1.73f * row*r - Vector3.forward * r * 0.866f;
        }
    } 
}
