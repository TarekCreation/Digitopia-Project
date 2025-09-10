using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class PasswordStrengthChecker : MonoBehaviour
{


    public PasswordScore CheckStrength(string password)
    {
        int score = 0;

        if (string.IsNullOrWhiteSpace(password))
            return PasswordScore.Blank;

        if (Regex.IsMatch(password, @"[a-z]"))
            score++;
        if (Regex.IsMatch(password, @"[A-Z]"))
            score++;
        if (Regex.IsMatch(password, @"\d"))
            score++;
        if (Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
            score++;

        if (score <= 1) return PasswordScore.VeryWeak;
        if (score == 2) return PasswordScore.Weak;
        if (score == 3) return PasswordScore.Medium;
        if (score == 4) return PasswordScore.Strong;
        return PasswordScore.VeryStrong;
    }
}
public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5
}
