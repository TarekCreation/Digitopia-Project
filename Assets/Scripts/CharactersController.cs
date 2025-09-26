using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    public List<string> CapitalLetters = new List<string>()
    {
        "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
    };
    public List<string> smallLetters = new List<string>()
    {
        "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"
    };
    public List<string> Numbers = new List<string>()
    {
        "0","1","2","3","4","5","6","7","8","9"
    };
    public List<string> SpecialCharacters = new List<string>()
    {
        "!","@","#","$","%","^","&","*","(",")","-","+","=","{","}","[","]",":",";","'","<",">",",",".","?","/","|"
    };

    public string GetRandomCapitalLetter()
    {
        int index = UnityEngine.Random.Range(0, CapitalLetters.Count);
        return CapitalLetters[index];
    }
    public string GetRandomSmallLetter()
    {
        int index = UnityEngine.Random.Range(0, smallLetters.Count);
        return smallLetters[index];
    }
    public string GetRandomNumber()
    {
        int index = UnityEngine.Random.Range(0, Numbers.Count);
        return Numbers[index];
    }
    public string GetRandomSpecialCharacter()
    {
        int index = UnityEngine.Random.Range(0, SpecialCharacters.Count);
        return SpecialCharacters[index];
    }

}
public enum CharacterType
{
    CapitalLetter,
    smallLetter,
    Number,
    SpecialCharacter
}
