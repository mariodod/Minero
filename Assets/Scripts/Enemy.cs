using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class Enemy : MonoBehaviour
{
    private bool hasSpawn;
    private Move moveScript;
    private Weapon[] weapons;
    private Collider2D coliderComponent;
    private SpriteRenderer rendererComponent;

    void Awake()
    {
        // Retrieve the weapon only once
        weapons = GetComponentsInChildren<Weapon>();

        // Retrieve scripts to disable when not spawn
        moveScript = GetComponent<Move>();

        coliderComponent = GetComponent<Collider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();
    }

    // 1 - Disable everything
    void Start()
    {
        hasSpawn = false;

        // Disable everything
        // -- collider
        coliderComponent.enabled = false;
        // -- Moving
        moveScript.enabled = false;
        // -- Shooting
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        // 2 - Check if the enemy has spawned.
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            // Auto-fire
            foreach (Weapon weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                }
            }

            // 4 - Out of the camera ? Destroy the game object.
            if (rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    // 3 - Activate itself.
    private void Spawn()
    {
        hasSpawn = true;

        // Enable everything
        // -- Collider
        coliderComponent.enabled = true;
        // -- Moving
        moveScript.enabled = true;
        // -- Shooting
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}
