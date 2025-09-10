using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusColor : MonoBehaviour
{
    public enum VirusType
    {
        Red,
        Green,
        Blue,
        Magenta
    }
    public VirusType virusType;
    public SpriteRenderer faceSprite;
    public SpriteRenderer spikesSprite;
    public Sprite[] faceSprites;
    public Sprite[] spikesSprites;
    public bool isRandom = false;
    
    private void Awake()
    {
        if (isRandom)
        {
            SetRandomVirusType();
        }
        else
        {
            SetVisual();
        }

    }
    public void SetVisual()
    {
        switch (virusType)
        {
            case VirusType.Red:
                faceSprite.sprite = faceSprites[0];
                spikesSprite.sprite = spikesSprites[0];
                break;
            case VirusType.Green:
                faceSprite.sprite = faceSprites[1];
                spikesSprite.sprite = spikesSprites[1];
                break;
            case VirusType.Blue:
                faceSprite.sprite = faceSprites[2];
                spikesSprite.sprite = spikesSprites[2];
                break;
            case VirusType.Magenta:
                faceSprite.sprite = faceSprites[3];
                spikesSprite.sprite = spikesSprites[3];
                break;
        }
    }
    public void SetRandomVirusType()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(VirusType)).Length);
        virusType = (VirusType)randomIndex;
        SetVisual();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
