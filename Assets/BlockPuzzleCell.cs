using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleCell : MonoBehaviour
{
    public int Row;
    public int Column;
    public bool IsOccupied = false;
    public GameObject OccupyingBlock = null;
    public bool ABlockIsMovingHere = false;
    public CellPrefab activatingCell = null;
    public GameObject border;

    private static List<BlockPuzzleCell> highlightedCells = new List<BlockPuzzleCell>();

    public void Start()
    {
        switch (transform.position.x)
        {
            case -5f: Column = 1; break;
            case -3.75f: Column = 2; break;
            case -2.5f: Column = 3; break;
            case -1.25f: Column = 4; break;
            case 0f: Column = 5; break;
            case 1.25f: Column = 6; break;
            case 2.5f: Column = 7; break;
            case 3.75f: Column = 8; break;
            case 5f: Column = 9; break;
            default: Column = -1; break;
        }

        switch (transform.position.y)
        {
            case 5f: Row = 1; break;
            case 3.75f: Row = 2; break;
            case 2.5f: Row = 3; break;
            case 1.25f: Row = 4; break;
            case 0f: Row = 5; break;
            case -1.25f: Row = 6; break;
            case -2.5f: Row = 7; break;
            case -3.75f: Row = 8; break;
            case -5f: Row = 9; break;
            default: Row = -1; break;
        }
        if (Row > 2 && Row < 10)
        {
            
        }
    }

    void Update()
    {
        if (ABlockIsMovingHere)
        {
            if (!highlightedCells.Contains(this))
            {
                highlightedCells.Add(this);
            }
            border.SetActive(true);
        }
        else
        {
            if (highlightedCells.Contains(this))
            {
                highlightedCells.Remove(this);
            }
            border.SetActive(false);
        }
    }

    public void SetOccupied(GameObject block)
    {
        IsOccupied = true;
        OccupyingBlock = block;
        ABlockIsMovingHere = false;
        if (highlightedCells.Contains(this))
        {
            highlightedCells.Remove(this);
        }
        border.SetActive(false);
    }

    public void ClearContent()
    {
        IsOccupied = false;
        OccupyingBlock = null;
        ABlockIsMovingHere = false;
        if (highlightedCells.Contains(this))
        {
            highlightedCells.Remove(this);
        }
        border.SetActive(false);
    }

    public static void ClearAllHighlights()
    {
        foreach (BlockPuzzleCell cell in highlightedCells)
        {
            if (cell != null)
            {
                cell.ABlockIsMovingHere = false;
                cell.border.SetActive(false);
            }
        }
        highlightedCells.Clear();
    }
}
