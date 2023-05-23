using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New book",menuName = "Custom/Book")]
public class Book : ScriptableObject
{
    [Multiline] public List<string> sentences;
}
