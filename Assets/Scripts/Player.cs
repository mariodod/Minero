using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    // 2 - Store the movement and the component
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private Weapon[] weapons;

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        // Retrieve the weapon only once
        weapons = GetComponentsInChildren<Weapon>();
    }

    void Update()
    {
        // 3 - Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // 4 - Movement per direction
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);

        // 5 - Shooting
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        // Careful: For Mac users, ctrl + arrow is a bad idea

        if (shoot)
        {
            foreach (Weapon weapon in weapons)
            {
                // Auto-fire
                if (weapon != null && weapon.CanAttack)
                {
                    weapon.Attack(false);
                    SoundEffectsHelper.Instance.MakePlayerShotSound();
                }
            }
        }
        // 6 - Make sure we are not outside the camera bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );
    }

    void FixedUpdate()
    {
        // 6 - Move the game object good practice
        rigidbodyComponent.velocity = movement;
    }

    void OnDestroy()
    {
        // Game Over.
        var gameOver = FindObjectOfType<GameOver>();
        gameOver.ShowButtons();
    }

}
