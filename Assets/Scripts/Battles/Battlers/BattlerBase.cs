using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattlerBase : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] int maxHP;
    [SerializeField] int at;
    [SerializeField] Sprite sprite;

    public string Name { get => name; set => name = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int AT { get => at; set => at = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
}
