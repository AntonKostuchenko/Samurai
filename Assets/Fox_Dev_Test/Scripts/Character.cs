using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Samurai", menuName = "Create Samurai", order = 1)]
public class Character : ScriptableObject
{
    public Sprite model;

    public string name;
    public int maxHealth;
    public float speed;
    public float damage;
    public float attackSpeed;
}
