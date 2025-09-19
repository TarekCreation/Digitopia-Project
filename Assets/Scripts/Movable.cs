using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 homePosition;
    private bool IsInBounds = false;
    private List<CellPrefab> cellPrefabs = new List<CellPrefab>();
    private BlockPuzzleCell[] allCells;

    public int gridWidth = 5;
    public int gridHeight = 5;
    public List<bool> gridCells = new List<bool>();

    public List<List<int>> Indexes = new List<List<int>>();
    private GameObject[] BlockPrefabs;
    public bool isLocked = false;
    public bool isTheStartingOne = false;
    public bool breakBool = false;
    public CellPrefab CentreBlock;
    List<BlockPuzzleCell> hoveringCells = new List<BlockPuzzleCell>();

    void OnValidate()
    {
        int requiredSize = gridWidth * gridHeight;
        while (gridCells.Count < requiredSize)
        {
            gridCells.Add(false);
        }
        while (gridCells.Count > requiredSize)
        {
            gridCells.RemoveAt(gridCells.Count - 1);
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        homePosition = transform.position;

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            CellPrefab cellPrefab = child.GetComponent<CellPrefab>();
            if (cellPrefab != null)
            {
                cellPrefabs.Add(cellPrefab);
            }
        }
        allCells = FindObjectsOfType<BlockPuzzleCell>();
    }

    public void Start()
    {
        if (gridCells[0])
        {
            Indexes.Add(new List<int> { -2, 2 });
        }
        if (gridCells[1])
        {
            Indexes.Add(new List<int> { -1, 2 });
        }
        if (gridCells[2])
        {
            Indexes.Add(new List<int> { 0, 2 });
        }
        if (gridCells[3])
        {
            Indexes.Add(new List<int> { 1, 2 });
        }
        if (gridCells[4])
        {
            Indexes.Add(new List<int> { 2, 2 });
        }
        if (gridCells[5])
        {
            Indexes.Add(new List<int> { -2, 1 });
        }
        if (gridCells[6])
        {
            Indexes.Add(new List<int> { -1, 1 });
        }
        if (gridCells[7])
        {
            Indexes.Add(new List<int> { 0, 1 });
        }
        if (gridCells[8])
        {
            Indexes.Add(new List<int> { 1, 1 });
        }
        if (gridCells[9])
        {
            Indexes.Add(new List<int> { 2, 1 });
        }
        if (gridCells[10])
        {
            Indexes.Add(new List<int> { -2, 0 });
        }
        if (gridCells[11])
        {
            Indexes.Add(new List<int> { -1, 0 });
        }
        if (gridCells[12])
        {
            Indexes.Add(new List<int> { 0, 0 });
        }
        if (gridCells[13])
        {
            Indexes.Add(new List<int> { 1, 0 });
        }
        if (gridCells[14])
        {
            Indexes.Add(new List<int> { 2, 0 });
        }
        if (gridCells[15])
        {
            Indexes.Add(new List<int> { -2, -1 });
        }
        if (gridCells[16])
        {
            Indexes.Add(new List<int> { -1, -1 });
        }
        if (gridCells[17])
        {
            Indexes.Add(new List<int> { 0, -1 });
        }
        if (gridCells[18])
        {
            Indexes.Add(new List<int> { 1, -1 });
        }
        if (gridCells[19])
        {
            Indexes.Add(new List<int> { 2, -1 });
        }
        if (gridCells[20])
        {
            Indexes.Add(new List<int> { -2, -2 });
        }
        if (gridCells[21])
        {
            Indexes.Add(new List<int> { -1, -2 });
        }
        if (gridCells[22])
        {
            Indexes.Add(new List<int> { 0, -2 });
        }
        if (gridCells[23])
        {
            Indexes.Add(new List<int> { 1, -2 });
        }
        if (gridCells[24])
        {
            Indexes.Add(new List<int> { 2, -2 });
        }
        FindObjectOfType<GridScript>().CheckIfCanPlace(this);
        if (isTheStartingOne)
        {
            isLocked = false;
        }
    }

    void Update()
    {
        if (isLocked)
        {
            breakBool = false;
            foreach (var block in cellPrefabs) block.isDeactivated = true;
        }
        else
        {
            if (!breakBool)
            {
                foreach (var block in cellPrefabs) block.isDeactivated = false;
                breakBool = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        if (isLocked) return;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
        offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
#elif UNITY_IOS || UNITY_ANDROID
        if (isLocked) return;
        if (Input.touchCount > 0)
        {
            Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            offset = transform.position - new Vector3(touchWorldPos.x, touchWorldPos.y, transform.position.z);
        }
#endif
    }

    public void OnDrag(PointerEventData eventData)
    {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        if (isLocked) return;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + offset;
#elif UNITY_IOS || UNITY_ANDROID
        if (isLocked) return;
        if (Input.touchCount > 0)
        {
            Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position = new Vector3(touchWorldPos.x, touchWorldPos.y, transform.position.z) + offset;
        }
#endif
        if (CentreBlock != null && CentreBlock.NearestCollider() != null && IsInBounds)
        {
            hoveringCells = FindObjectOfType<GridScript>().CheckIfCanPlaceAtThisCell(CentreBlock.NearestCollider().GetComponent<BlockPuzzleCell>(), this);
            if (hoveringCells != null)
            {
                foreach (var block in cellPrefabs) block.isDeactivated = false;
            }
            else
            {
                foreach (var block in cellPrefabs) block.isDeactivated = true;
            }
        }
        else
        {
            foreach (var block in cellPrefabs) block.isDeactivated = true;
        }
        foreach (var cell in FindObjectsOfType<BlockPuzzleCell>()) cell.ABlockIsMovingHere = false;
        if (hoveringCells != null)
        {
            foreach (var item in hoveringCells) item.ABlockIsMovingHere = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var cell in FindObjectsOfType<BlockPuzzleCell>()) cell.ABlockIsMovingHere = false;
        foreach (var block in cellPrefabs) block.isDeactivated = false;
        if (isLocked) return;
        if (!IsInBounds)
        {
            transform.position = homePosition;
        }
        else
        {
            if (hoveringCells != null)
            {
                if (hoveringCells.Count != cellPrefabs.Count)
                {
                    transform.position = homePosition;
                    return;
                }
                Place();
            }
            else
            {
                transform.position = homePosition;
            }
        }
    }

    void Place()
    {
        if (cellPrefabs.Count > 3)
        {
            Score score = FindObjectOfType<Score>();
            if (score != null) score.UpdateScore(Mathf.CeilToInt(cellPrefabs.Count / 3));
        }
        FindObjectOfType<GridScript>().NumberOfPlacedPieces = FindObjectOfType<GridScript>().NumberOfPlacedPieces + 1;
        for (int i = 0; i < cellPrefabs.Count; i++)
        {
            var block = cellPrefabs[i];
            var cell = hoveringCells[i];
            block.transform.position = cell.transform.position;
            cell.SetOccupied(block.gameObject);
            block.didPlace = true;
            block.Place();
        }
        StartCoroutine(WaitAfterPlace());
    }

    IEnumerator WaitAfterPlace()
    {
        bool isWaiting = true;
        while (isWaiting)
        {
            yield return new WaitForSeconds(0.05f);
            bool FoundOneWaiting = false;
            foreach (var block in cellPrefabs)
            {
                if (!block.didPlace)
                {
                    FoundOneWaiting = true;
                    break;
                }
            }
            if (!FoundOneWaiting) isWaiting = false;
        }
        GenerateRandomBlock();
        FindObjectOfType<GridScript>().CheckIfComplete();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    void GenerateRandomBlock()
    {
        BlockPrefabs = FindObjectOfType<SpawnPoints>().blocksPrefabs;
        int rnd = Random.Range(0, BlockPrefabs.Length);
        GameObject block = Instantiate(BlockPrefabs[rnd], transform.parent.position, BlockPrefabs[rnd].transform.rotation);
        block.GetComponentInChildren<Movable>().isTheStartingOne = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("OkayCollider")) IsInBounds = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("OkayCollider")) IsInBounds = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("OkayCollider")) IsInBounds = false;
    }
}







































// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class Movable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
// {
//     private Vector3 offset;
//     private Camera mainCamera;
//     private Vector3 homePosition;
//     private bool IsInBounds = false;
//     private List<CellPrefab> cellPrefabs = new List<CellPrefab>();
//     private List<BlockPuzzleCell> hoveredCells = new List<BlockPuzzleCell>();
//     private BlockPuzzleCell[] allCells;

//     private Dictionary<CellPrefab, Vector2Int> blockOffsets = new Dictionary<CellPrefab, Vector2Int>();
//     private Vector3 anchorLocal = Vector3.zero;
//     private const float gridStep = 1.25f;
//     public int gridWidth = 5;
//     public int gridHeight = 5;
//     public List<bool> gridCells = new List<bool>();

//     public List<List<int>> Indexes = new List<List<int>>();
//     public GameObject[] BlockPrefabs;
//     public bool isLocked = false;
//     public bool isTheStartingOne = false;
//     public bool breakBool = false;

//     void OnValidate()
//     {
//         int requiredSize = gridWidth * gridHeight;
//         while (gridCells.Count < requiredSize)
//         {
//             gridCells.Add(false);
//         }
//         while (gridCells.Count > requiredSize)
//         {
//             gridCells.RemoveAt(gridCells.Count - 1);
//         }
//     }

//     private void Awake()
//     {
//         mainCamera = Camera.main;
//         homePosition = transform.position;

//         foreach (Transform child in GetComponentsInChildren<Transform>())
//         {
//             CellPrefab cellPrefab = child.GetComponent<CellPrefab>();
//             if (cellPrefab != null)
//             {
//                 cellPrefabs.Add(cellPrefab);
//             }
//         }
//         allCells = FindObjectsOfType<BlockPuzzleCell>();

//         if (cellPrefabs.Count > 0)
//         {
//             Dictionary<CellPrefab, Vector2Int> raw = new Dictionary<CellPrefab, Vector2Int>();
//             int minDx = int.MaxValue;
//             int minDy = int.MaxValue;
//             foreach (var block in cellPrefabs)
//             {
//                 Vector3 local = transform.InverseTransformPoint(block.transform.position);
//                 int dx = Mathf.RoundToInt(local.x / gridStep);
//                 int dy = Mathf.RoundToInt(local.y / gridStep);
//                 raw[block] = new Vector2Int(dx, dy);
//                 if (dx < minDx) minDx = dx;
//                 if (dy < minDy) minDy = dy;
//             }
//             foreach (var kv in raw)
//             {
//                 Vector2Int normalized = new Vector2Int(kv.Value.x - minDx, kv.Value.y - minDy);
//                 blockOffsets[kv.Key] = normalized;
//             }
//             anchorLocal = new Vector3(minDx * gridStep, minDy * gridStep, 0f);
//         }
        

        
//     }
//     public void Start()
//     {
//         if (gridCells[0])
//         {
//             Indexes.Add(new List<int> { -2, 2 });
//         }
//         if (gridCells[1])
//         {
//             Indexes.Add(new List<int> { -1, 2 });
//         }
//         if (gridCells[2])
//         {
//             Indexes.Add(new List<int> { 0, 2 });
//         }
//         if (gridCells[3])
//         {
//             Indexes.Add(new List<int> { 1, 2 });
//         }
//         if (gridCells[4])
//         {
//             Indexes.Add(new List<int> { 2, 2 });
//         }
//         if (gridCells[5])
//         {
//             Indexes.Add(new List<int> { -2, 1 });
//         }
//         if (gridCells[6])
//         {
//             Indexes.Add(new List<int> { -1, 1 });
//         }
//         if (gridCells[7])
//         {
//             Indexes.Add(new List<int> { 0, 1 });
//         }
//         if (gridCells[8])
//         {
//             Indexes.Add(new List<int> { 1, 1 });
//         }
//         if (gridCells[9])
//         {
//             Indexes.Add(new List<int> { 2, 1 });
//         }
//         if (gridCells[10])
//         {
//             Indexes.Add(new List<int> { -2, 0 });
//         }
//         if (gridCells[11])
//         {
//             Indexes.Add(new List<int> { -1, 0 });
//         }
//         if (gridCells[12])
//         {
//             Indexes.Add(new List<int> { 0, 0 });
//         }
//         if (gridCells[13])
//         {
//             Indexes.Add(new List<int> { 1, 0 });
//         }
//         if (gridCells[14])
//         {
//             Indexes.Add(new List<int> { 2, 0 });
//         }
//         if (gridCells[15])
//         {
//             Indexes.Add(new List<int> { -2, -1 });
//         }
//         if (gridCells[16])
//         {
//             Indexes.Add(new List<int> { -1, -1 });
//         }
//         if (gridCells[17])
//         {
//             Indexes.Add(new List<int> { 0, -1 });
//         }
//         if (gridCells[18])
//         {
//             Indexes.Add(new List<int> { 1, -1 });
//         }
//         if (gridCells[19])
//         {
//             Indexes.Add(new List<int> { 2, -1 });
//         }
//         if (gridCells[20])
//         {
//             Indexes.Add(new List<int> { -2, -2 });
//         }
//         if (gridCells[21])
//         {
//             Indexes.Add(new List<int> { -1, -2 });
//         }
//         if (gridCells[22])
//         {
//             Indexes.Add(new List<int> { 0, -2 });
//         }
//         if (gridCells[23])
//         {
//             Indexes.Add(new List<int> { 1, -2 });
//         }
//         if (gridCells[24])
//         {
//             Indexes.Add(new List<int> { 2, -2 });
//         }
//         FindObjectOfType<GridScript>().CheckIfCanPlace(this);
//         if (isTheStartingOne)
//         {
//             isLocked = false;
//         }
//     }
//     void Update()
//     {
//         if (isLocked)
//         {
//             breakBool = false;
//             foreach (var block in cellPrefabs)
//             {
//                 block.isDeactivated = true;
//             }
//         }else
//         {
//             if (!breakBool)
//             {
//                 foreach (var block in cellPrefabs)
//                 {
//                     block.isDeactivated = false;
//                 }
//                 breakBool = true;
//             }
//         }
//     }

//     public void OnPointerDown(PointerEventData eventData)
//     {
//         if (isLocked) return;
//         Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
//         offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         if (isLocked) return;
//         Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
//         transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + offset;

//         TryHighlightCells();

//         bool isOkay = hoveredCells.Count == cellPrefabs.Count;
//         foreach (var block in cellPrefabs)
//         {
//             block.isDeactivated = !isOkay;
//         }
//     }

//     public void OnPointerUp(PointerEventData eventData)
//     {
        
//         foreach (var block in cellPrefabs)
//         {
//             block.isDeactivated = false;
//         }
//         if (isLocked) return;
//         if (!IsInBounds)
//         {
//             ClearHovered();
//             transform.position = homePosition;
//         }
//         else
//         {
//             if (hoveredCells.Count != cellPrefabs.Count)
//             {
//                 ClearHovered();
//                 transform.position = homePosition;
//                 return;
//             }
//             Place();
//         }
//     }

//     void TryHighlightCells()
//     {
//         ClearHovered();
//         if (allCells == null || allCells.Length == 0) return;

//         Vector3 anchorWorld = transform.TransformPoint(anchorLocal);
//         BlockPuzzleCell anchorCell = FindNearestCell(anchorWorld);
//         if (anchorCell == null) return;

//         List<BlockPuzzleCell> temp = new List<BlockPuzzleCell>();
//         bool canPlaceAll = true;

//         foreach (var block in cellPrefabs)
//         {
//             if (!blockOffsets.ContainsKey(block))
//             {
//                 canPlaceAll = false;
//                 break;
//             }
//             Vector2Int off = blockOffsets[block];
//             int targetRow = anchorCell.Row - off.y;
//             int targetCol = anchorCell.Column + off.x;

//             BlockPuzzleCell targetCell = FindCellByRowCol(targetRow, targetCol);
//             if (targetCell == null || targetCell.IsOccupied || temp.Contains(targetCell))
//             {
//                 canPlaceAll = false;
//                 break;
//             }
//             temp.Add(targetCell);
//         }

//         if (canPlaceAll && temp.Count == cellPrefabs.Count)
//         {
//             hoveredCells = temp;
//             for (int i = 0; i < hoveredCells.Count; i++)
//             {
//                 if (hoveredCells[i] != null)
//                 {
//                     hoveredCells[i].ABlockIsMovingHere = true;
//                     hoveredCells[i].activatingCell = null;
//                 }
//             }
//         }
//         else
//         {
//             hoveredCells.Clear();
//         }
//     }

//     void Place()
//     {
//         for (int i = 0; i < cellPrefabs.Count; i++)
//         {
//             var block = cellPrefabs[i];
//             var cell = hoveredCells[i];

//             block.transform.position = cell.transform.position;
//             cell.SetOccupied(block.gameObject);

//             block.didPlace = true;
//             block.Place();
//         }
        
        
//         StartCoroutine(WaitAfterPlace());
//     }

//     IEnumerator WaitAfterPlace()
//     {
//         bool isWaiting = true;
//         while (isWaiting)
//         {
//             yield return new WaitForSeconds(0.05f);
//             bool FoundOneWaiting = false;
//             foreach (var block in cellPrefabs)
//             {
//                 if (!block.didPlace)
//                 {
//                     FoundOneWaiting = true;
//                     break;
//                 }
//             }
//             if (!FoundOneWaiting)
//             {
//                 isWaiting = false;
//             }
//         }
//         GenerateRandomBlock();
        
//         FindObjectOfType<GridScript>().CheckIfComplete();
//         GetComponent<Collider2D>().enabled = false;
//         this.enabled = false;
//     }

//     void GenerateRandomBlock()
//     {
//         int rnd = Random.Range(0, BlockPrefabs.Length);
//         GameObject block = Instantiate(BlockPrefabs[rnd], homePosition, BlockPrefabs[rnd].transform.rotation);
//         block.GetComponent<Movable>().isTheStartingOne = false;
//     }

//     private BlockPuzzleCell FindNearestCell(Vector3 position)
//     {
//         BlockPuzzleCell nearest = null;
//         float minDist = float.MaxValue;
//         foreach (var cell in allCells)
//         {
//             float d = (cell.transform.position - position).sqrMagnitude;
//             if (d < minDist)
//             {
//                 minDist = d;
//                 nearest = cell;
//             }
//         }
//         return nearest;
//     }

//     private BlockPuzzleCell FindCellByRowCol(int row, int col)
//     {
//         foreach (var cell in allCells)
//         {
//             if (cell.Row == row && cell.Column == col)
//                 return cell;
//         }
//         return null;
//     }

//     private void ClearHovered()
//     {
//         foreach (var cell in hoveredCells)
//         {
//             if (cell != null)
//             {
//                 cell.ABlockIsMovingHere = false;
//                 cell.activatingCell = null;
//             }
//         }
//         hoveredCells.Clear();
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("OutOfBounds"))
//         {
//             IsInBounds = false;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("OutOfBounds"))
//         {
//             IsInBounds = true;
//         }
//     }
// }

