using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    private List<BlockPuzzleCell> cells = new List<BlockPuzzleCell>();
    public GameObject DestroyRowPrefab;
    public bool WentToCheck = false;
    int numberOf_Weak_Passwords = 0;
    int numberOf_Medium_Passwords = 0;
    int numberOf_Strong_Passwords = 0;
    public int NumberOfPlacedPieces = 0;
    public Animator zero;

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
            FindObjectOfType<GUIscript>().EndGame(NumberOfPlacedPieces, numberOf_Weak_Passwords, numberOf_Medium_Passwords, numberOf_Strong_Passwords);
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
            PasswordScore passwordScore = FindObjectOfType<PasswordStrengthChecker>().CheckStrength(password);
            if (passwordScore == PasswordScore.VeryWeak)
            {
                zero.SetTrigger("Weak");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyWeakPassword, 0.7f);
                numberOf_Weak_Passwords++;
                FindObjectOfType<Score>().UpdateScore(-15);
                
            }
            else if (passwordScore == PasswordScore.Weak)
            {
                zero.SetTrigger("Weak");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyWeakPassword, 0.7f);
                numberOf_Weak_Passwords++;
                FindObjectOfType<Score>().UpdateScore(-10);
            }
            else if (passwordScore == PasswordScore.Medium)
            {
                zero.SetTrigger("Medium");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyMediumPassword, 0.7f);
                numberOf_Medium_Passwords++;
                FindObjectOfType<Score>().UpdateScore(15);
            }
            else if (passwordScore == PasswordScore.Strong)
            {
                zero.SetTrigger("Strong");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyStrongPassword, 0.7f);
                numberOf_Strong_Passwords++;
                FindObjectOfType<Score>().UpdateScore(20);
            }
            else if (passwordScore == PasswordScore.VeryStrong)
            {
                zero.SetTrigger("Strong");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyStrongPassword, 0.7f);
                numberOf_Strong_Passwords++;
                FindObjectOfType<Score>().UpdateScore(30);
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
            PasswordScore passwordScore = FindObjectOfType<PasswordStrengthChecker>().CheckStrength(password);
            if (passwordScore == PasswordScore.VeryWeak)
            {
                zero.SetTrigger("Weak");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyWeakPassword, 0.7f);
                numberOf_Weak_Passwords++;
                FindObjectOfType<Score>().UpdateScore(-15);
            }
            else if (passwordScore == PasswordScore.Weak)
            {
                zero.SetTrigger("Weak");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyWeakPassword, 0.7f);
                numberOf_Weak_Passwords++;
                FindObjectOfType<Score>().UpdateScore(-10);
            }
            else if (passwordScore == PasswordScore.Medium)
            {
                zero.SetTrigger("Medium");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyMediumPassword, 0.7f);
                numberOf_Medium_Passwords++;
                FindObjectOfType<Score>().UpdateScore(15);
            }
            else if (passwordScore == PasswordScore.Strong)
            {
                zero.SetTrigger("Strong");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyStrongPassword, 0.7f);
                numberOf_Strong_Passwords++;
                FindObjectOfType<Score>().UpdateScore(20);
            }
            else if (passwordScore == PasswordScore.VeryStrong)
            {
                zero.SetTrigger("Strong");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destroyStrongPassword, 0.7f);
                numberOf_Strong_Passwords++;
                FindObjectOfType<Score>().UpdateScore(30);
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
