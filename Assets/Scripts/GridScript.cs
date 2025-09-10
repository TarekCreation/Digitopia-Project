using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    private List<BlockPuzzleCell> cells = new List<BlockPuzzleCell>();
    public GameObject DestroyRowPrefab;
    public bool WentToCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            BlockPuzzleCell cell = child.GetComponent<BlockPuzzleCell>();
            if (cell != null)
            {
                cells.Add(cell);
            }
        }
    }
    void Update()
    {
        bool AtLeastOneOption = false;
        foreach (Movable blocks in FindObjectsOfType<Movable>())
        {
            if (blocks.enabled == true)
            {
                if (!blocks.isLocked)
                {
                    AtLeastOneOption = true;
                    break;
                }
            }
            
        }
        if (!AtLeastOneOption && !WentToCheck)
        {
            WentToCheck = true;
            StartCoroutine(CheckAfterWaiting());
        }
    }
    IEnumerator CheckAfterWaiting()
    {
        yield return new WaitForSeconds(1f);
        bool AtLeastOneOption = false;
        foreach (Movable blocks in FindObjectsOfType<Movable>())
        {
            if (blocks.enabled == true)
            {
                if (!blocks.isLocked)
                {
                    AtLeastOneOption = true;
                    break;
                }
            }
        }
        if (!AtLeastOneOption)
        {
            Debug.Log("Game Over");
        }else
        {
            WentToCheck = false;
        }
    }

    public void CheckIfComplete()
    {
        int[] rowCount = new int[9];
        int[] colCount = new int[9];

        foreach (BlockPuzzleCell cell in cells)
        {
            if (cell.IsOccupied)
            {
                rowCount[cell.Row - 1]++;
                colCount[cell.Column - 1]++;
            }
        }
        List<int> listOfCompletedRows = new List<int>();
        List<int> listOfCompletedColumns = new List<int>();
        for (int r = 0; r < rowCount.Length; r++)
        {
            if (rowCount[r] == 9)
            {
                listOfCompletedRows.Add(r + 1);
            }
        }
        for (int c = 0; c < colCount.Length; c++)
        {
            if (colCount[c] == 9)
            {
                listOfCompletedColumns.Add(c + 1);
            }
        }

        foreach (int r in listOfCompletedRows)
        {
            float y = 0f;
            string password = "";
            foreach (BlockPuzzleCell cell in cells)
            {
                if (cell.Row == r && cell.OccupyingBlock != null)
                {
                    y = cell.transform.position.y;
                    string Char = cell.OccupyingBlock.GetComponent<CellPrefab>().value;
                    password += Char;
                    cell.ClearContent();
                }
            }
            Debug.Log("Completed Row Password: " + password);
            PasswordScore passwordScore = FindObjectOfType<PasswordStrengthChecker>().CheckStrength(password);
            if (passwordScore == PasswordScore.VeryWeak)
            {
                FindObjectOfType<Score>().UpdateScore(-150);
            }
            else if (passwordScore == PasswordScore.Weak)
            {
                FindObjectOfType<Score>().UpdateScore(-100);
            }
            else if (passwordScore == PasswordScore.Medium)
            {
                FindObjectOfType<Score>().UpdateScore(150);
            }
            else if (passwordScore == PasswordScore.Strong)
            {
                FindObjectOfType<Score>().UpdateScore(200);
            }
            else if (passwordScore == PasswordScore.VeryStrong)
            {
                FindObjectOfType<Score>().UpdateScore(300);
            }
            Quaternion rotation = new Quaternion();
            int rnd = UnityEngine.Random.Range(0, 2);
            if (rnd == 0)
            {
                rotation = Quaternion.Euler(DestroyRowPrefab.transform.rotation.x, DestroyRowPrefab.transform.rotation.y, 0);
            }
            else
            {
                rotation = Quaternion.Euler(DestroyRowPrefab.transform.rotation.x, DestroyRowPrefab.transform.rotation.y, 180);
            }

            Instantiate(DestroyRowPrefab, new Vector3(0, y, 0), rotation);
        }

        foreach (int c in listOfCompletedColumns)
        {
            float x = 0f;
            string password = "";
            foreach (BlockPuzzleCell cell in cells)
            {
                if (cell.Column == c && cell.OccupyingBlock != null)
                {
                    x = cell.transform.position.x;
                    string Char = cell.OccupyingBlock.GetComponent<CellPrefab>().value;
                    password += Char;
                    cell.ClearContent();
                }
            }
            Debug.Log("Completed Column Password: " + password);
            PasswordScore passwordScore = FindObjectOfType<PasswordStrengthChecker>().CheckStrength(password);
            if (passwordScore == PasswordScore.VeryWeak)
            {
                FindObjectOfType<Score>().UpdateScore(-150);
            }
            else if (passwordScore == PasswordScore.Weak)
            {
                FindObjectOfType<Score>().UpdateScore(-100);
            }
            else if (passwordScore == PasswordScore.Medium)
            {
                FindObjectOfType<Score>().UpdateScore(150);
            }
            else if (passwordScore == PasswordScore.Strong)
            {
                FindObjectOfType<Score>().UpdateScore(200);
            }
            else if (passwordScore == PasswordScore.VeryStrong)
            {
                FindObjectOfType<Score>().UpdateScore(300);
            }
            Quaternion rotation = new Quaternion();
            int rnd = UnityEngine.Random.Range(0, 2);
            if (rnd == 0)
            {
                rotation = Quaternion.Euler(DestroyRowPrefab.transform.rotation.x, DestroyRowPrefab.transform.rotation.y, 90);
            }
            else
            {
                rotation = Quaternion.Euler(DestroyRowPrefab.transform.rotation.x, DestroyRowPrefab.transform.rotation.y, 270);
            }

            Instantiate(DestroyRowPrefab, new Vector3(x, 0, 0), rotation);
        }
        foreach (Movable blocks in FindObjectsOfType<Movable>())
        {
            FindObjectOfType<GridScript>().CheckIfCanPlace(blocks);
        }
    }
    public List<BlockPuzzleCell> CheckIfCanPlaceAtThisCell(BlockPuzzleCell cell, Movable blocks)
    {
        bool CanPlaceTheseBlocks = true;
        List<BlockPuzzleCell> Cells = new List<BlockPuzzleCell>();
        foreach (List<int> index in blocks.Indexes)
        {

            int targetCol = cell.Column + index[0];
            int targetRow = cell.Row - index[1];

            if (targetCol < 1 || targetCol > 9 || targetRow < 1 || targetRow > 9)
            {
                CanPlaceTheseBlocks = false;
                break;
            }
            else
            {
                BlockPuzzleCell targetCell = cells.Find(c => c.Row == targetRow && c.Column == targetCol);
                if (targetCell.IsOccupied)
                {
                    CanPlaceTheseBlocks = false;
                    break;
                }else
                {
                    Cells.Add(targetCell);
                }
            }
        }
        if (CanPlaceTheseBlocks)
        {
            return Cells;
        }
        else
        {
            return null;
        }
    }
    // public void PlaceBlocks(List<CellPrefab> cellPrefabs, Movable blocks)
    // {
        
    //     foreach (List<int> index in blocks.Indexes)
    //     {

    //         int targetCol = cell.Column + index[0];
    //         int targetRow = cell.Row + index[1];

    //         if (targetCol < 1 || targetCol > 9 || targetRow < 1 || targetRow > 9)
    //         {
    //             CanPlaceTheseBlocks = false;
    //             break;
    //         }
    //         else
    //         {

    //             BlockPuzzleCell targetCell = cells.Find(c => c.Row == targetRow && c.Column == targetCol);


    //             if (targetCell.IsOccupied)
    //             {
    //                 CanPlaceTheseBlocks = false;
    //                 break;
    //             }else
    //             {
                    
    //             }
    //         }
    //     }
        
    // }
    public void CheckIfCanPlace(Movable blocks)
    {
        bool ThereIsAtLeastOnePossiblePlace = false;
        foreach (BlockPuzzleCell cell in cells)
        {
            bool CanPlaceTheseBlocks = true;

            foreach (List<int> index in blocks.Indexes)
            {

                int targetCol = cell.Column + index[0];
                int targetRow = cell.Row - index[1];

                if (targetCol < 1 || targetCol > 9 || targetRow < 1 || targetRow > 9)
                {
                    CanPlaceTheseBlocks = false;
                    break;
                }
                else
                {

                    BlockPuzzleCell targetCell = cells.Find(c => c.Row == targetRow && c.Column == targetCol);

                    if (targetCell != null)
                    {
                        if (targetCell.IsOccupied)
                        {
                            CanPlaceTheseBlocks = false;
                            break;
                        }
                    }
                    
                }
            }
            if (CanPlaceTheseBlocks)
            {
                ThereIsAtLeastOnePossiblePlace = true;

                break;
            }

        }
        if (!ThereIsAtLeastOnePossiblePlace)
        {
            blocks.breakBool = true;
            blocks.isLocked = true;
        }
        else
        {
            blocks.breakBool = false;
            blocks.isLocked = false;
        }
    }
}
