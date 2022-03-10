using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int GridX;
    public int GridY;

    public bool IsWall;
    public Vector3 Position;

    public Node Parent;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    public Node(bool a_bIsWall, Vector3 a_vPos, int a_igridX, int a_igridY)
    {
        IsWall = a_bIsWall;
        Position = a_vPos;
        GridX = a_igridX;
        GridY = a_igridY;
    }
}
