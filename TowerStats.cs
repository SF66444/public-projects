using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class TowerStats : ScriptableObject
{
    [SerializeField] private int price;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bullet;

    public int Price { get => price; }
    public float FireRate { get => fireRate; }
    public float Damage { get => damage; }
    public float BulletSpeed { get => bulletSpeed; }
    public GameObject Bullet { get => bullet; }
}
