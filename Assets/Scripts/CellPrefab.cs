using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellPrefab : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();
    public bool CanPlace
    {
        get { return colliders.Count > 0; }
    }
    public bool didPlace = false;
    public TextMeshProUGUI display;
    public string value;
    public CharacterType type;
    public GameObject Particles;
    private SpriteRenderer spriteRenderer;
    public bool isDeactivated = false;
    public bool CanBeDestroyed = false;
    public Vector2 Pos;
    public bool isTheCentreBlock = false;
    void Awake()
    {
        didPlace = false;
        CanBeDestroyed = false;
        transform.localPosition = Pos;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CharactersController charactersController = FindObjectOfType<CharactersController>();
        if (charactersController != null)
        {
            switch (type)
            {
                case CharacterType.CapitalLetter:
                    value = charactersController.GetRandomCapitalLetter();
                    break;
                case CharacterType.smallLetter:
                    value = charactersController.GetRandomSmallLetter();
                    break;
                case CharacterType.Number:
                    value = charactersController.GetRandomNumber();
                    break;
                case CharacterType.SpecialCharacter:
                    value = charactersController.GetRandomSpecialCharacter();
                    break;
                default:
                    value = "";
                    break;
            }
            display.text = value;
        }
    }

    public void DeactivateColor()
    {
        Color c = spriteRenderer.color;
        c.a = 0.5f;
        spriteRenderer.color = c;
    }

    public void ActivateColor()
    {
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }

    void Update()
    {
        if (isDeactivated)
        {
            DeactivateColor();
        }
        else
        {
            ActivateColor();
        }
        
        
    }

    public void Place()
    {
        CanBeDestroyed = true;
    }

    public Collider2D NearestCollider()
    {
        float ShortestDistance = float.MaxValue;
        Collider2D NearestCollider = null;
        foreach (Collider2D collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < ShortestDistance)
            {
                ShortestDistance = distance;
                NearestCollider = collider;
            }
        }
        return NearestCollider;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GridCell") && isTheCentreBlock)
        {
            BlockPuzzleCell cell = other.GetComponent<BlockPuzzleCell>();
            if (cell != null && !cell.IsOccupied)
            {
                if (!colliders.Contains(other))
                {
                    colliders.Add(other);
                }
            }
        }
        else if (other.CompareTag("DestroyCells") && didPlace && CanBeDestroyed)
        {
            Instantiate(Particles, transform.position, Particles.transform.rotation);
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GridCell") && isTheCentreBlock)
        {
            if (colliders.Contains(other))
            {
                colliders.Remove(other);
            }

        }
    }
}






// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;

// public class CellPrefab : MonoBehaviour
// {
//     private List<Collider2D> colliders = new List<Collider2D>();
//     public bool CanPlace
//     {
//         get { return colliders.Count > 0; }
//     }
//     public bool didPlace = false;
//     public TextMeshProUGUI display;
//     private string value;
//     public CharacterType type;
//     public GameObject Particles;
//     private SpriteRenderer spriteRenderer;
//     public bool isDeactivated = false;
//     public bool CanBeDestroyed = false;
//     public Vector2 Pos;
//     void Awake()
//     {
//         didPlace = false;
//         CanBeDestroyed = false;
//         transform.localPosition = Pos;
//     }
//     void Start()
//     {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         CharactersController charactersController = FindObjectOfType<CharactersController>();
//         if (charactersController != null)
//         {
//             switch (type)
//             {
//                 case CharacterType.CapitalLetter:
//                     value = charactersController.GetRandomCapitalLetter();
//                     break;
//                 case CharacterType.smallLetter:
//                     value = charactersController.GetRandomSmallLetter();
//                     break;
//                 case CharacterType.Number:
//                     value = charactersController.GetRandomNumber();
//                     break;
//                 case CharacterType.SpecialCharacter:
//                     value = charactersController.GetRandomSpecialCharacter();
//                     break;
//                 default:
//                     value = "";
//                     break;
//             }
//             display.text = value;
//         }
//     }

//     public void DeactivateColor()
//     {
//         Color c = spriteRenderer.color;
//         c.a = 0.5f;
//         spriteRenderer.color = c;
//     }

//     public void ActivateColor()
//     {
//         Color c = spriteRenderer.color;
//         c.a = 1f;
//         spriteRenderer.color = c;
//     }

//     void Update()
//     {
//         if (isDeactivated)
//         {
//             DeactivateColor();
//         }
//         else
//         {
//             ActivateColor();
//         }

//         Collider2D nearestCollider = NearestCollider();
//         if (nearestCollider != null)
//         {
//             BlockPuzzleCell cell = nearestCollider.GetComponent<BlockPuzzleCell>();
//             if (cell != null)
//             {
//                 if (cell.activatingCell == null || cell.activatingCell == this)
//                 {
//                     cell.ABlockIsMovingHere = true;
//                     cell.activatingCell = this;
//                 }
//             }
//         }
//     }

//     public void Place()
//     {
//         CanBeDestroyed = true;
//     }

//     public Collider2D NearestCollider()
//     {
//         float ShortestDistance = float.MaxValue;
//         Collider2D NearestCollider = null;
//         foreach (Collider2D collider in colliders)
//         {
//             float distance = Vector3.Distance(transform.position, collider.transform.position);
//             if (distance < ShortestDistance)
//             {
//                 ShortestDistance = distance;
//                 NearestCollider = collider;
//             }
//         }
//         return NearestCollider;
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("GridCell"))
//         {
//             BlockPuzzleCell cell = other.GetComponent<BlockPuzzleCell>();
//             if (cell != null && !cell.IsOccupied)
//             {
//                 if (!colliders.Contains(other))
//                 {
//                     colliders.Add(other);
//                 }
//             }
//         }
//         else if (other.CompareTag("DestroyCells") && didPlace && CanBeDestroyed)
//         {
//             GameObject _particles = Instantiate(Particles, transform.position, Particles.transform.rotation);
//             _particles.GetComponent<ParticleSystem>().startColor = spriteRenderer.color;
//             Destroy(gameObject);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("GridCell"))
//         {
//             if (colliders.Contains(other))
//             {
//                 colliders.Remove(other);
//             }

//             BlockPuzzleCell cell = other.GetComponent<BlockPuzzleCell>();
//             if (cell != null && cell.activatingCell == this)
//             {
//                 cell.ABlockIsMovingHere = false;
//                 cell.activatingCell = null;
//             }
//         }
//     }
// }

