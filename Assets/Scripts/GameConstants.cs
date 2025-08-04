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
    public static Vector3 HexPosition(int col,int row)
    {
        if(col % 2 == 0)
        {
            return Vector3.right*col* HEXAGON_R * 1.5f + Vector3.forward* -1.73f*row*HEXAGON_R;
        }
        else
        {
            return Vector3.right * col * HEXAGON_R * 1.5f + Vector3.forward * -1.73f * row*HEXAGON_R - Vector3.forward * HEXAGON_R * 0.866f;
        }
    } 
}
