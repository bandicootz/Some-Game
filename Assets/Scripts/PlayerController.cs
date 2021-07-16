using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private JoystickShot joystickShot;

    [SerializeField]
    private GameObject bulletPrefab;
    private GameObject target;
    private Transform road;
    [SerializeField]
    private Button restartButton;

    private bool canMove = true;
    private bool isJumping = true;
    private bool isGameOver = false;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float stopDistance;
    [SerializeField]
    private float jumpAmlifier;
    [SerializeField]
    private int healthStart;
    [SerializeField]
    private float healthCurrent;
    private float hpLose;
    private float sizeAmplifier;
    [SerializeField]
    Vector3 ballStartScale;
    [SerializeField]
    Vector3 roadStartScale;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        joystickShot = GameObject.Find("JoystickShot").GetComponent<JoystickShot>();
        target = GameObject.Find("DoorFrame");
        road = GameObject.Find("RedCarpet").GetComponent<Transform>();

        transform.localScale = ballStartScale;
    }

    void FixedUpdate()
    {
        Raycasting();

        JumpAndMove();
    }

    void Raycasting()
    {
        Ray inspectingRay = new Ray(transform.position, target.transform.position);
        float rayRadius = transform.localScale.x / 2;

        if (Physics.SphereCast(inspectingRay, rayRadius, out RaycastHit hit, stopDistance))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                canMove = false;
            }
            if (!canMove & hit.collider.CompareTag("Bullet"))
            {
                canMove = false;
            }
        }
        else
        {
            canMove = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RedCarpet"))
        {
            isJumping = false;
        }
    }

    void JumpAndMove()
    {
        if (isJumping)
        {
            transform.Translate((target.transform.position - transform.position).normalized * speed * Time.deltaTime);
        }
        if (!isJumping & canMove & !isGameOver)
        {
            rb.AddForce(Vector3.up * jumpAmlifier);
            isJumping = true;
        }
    }

    public void Shot()
    {
        if (!isGameOver)
        {
            hpLose = joystickShot.framesTook;

            UpdateHealthAndSize();

            SpawnBullet();
        }
    }

    void UpdateHealthAndSize()
    {

        if (healthCurrent == healthStart)
        {
            healthCurrent = healthStart - hpLose;
            sizeAmplifier = healthCurrent / healthStart;
            transform.localScale = ballStartScale * sizeAmplifier;
        }
        else if (healthCurrent > 0)
        {
            healthCurrent -= hpLose;
            if (healthCurrent > 0)
            {
                sizeAmplifier = healthCurrent / healthStart;
                transform.localScale = ballStartScale * sizeAmplifier;
            }

            else if (healthCurrent <= 0)
            {
                sizeAmplifier = 0;
                transform.localScale *= sizeAmplifier;

                GameOver();
            }
        }

        UpdateRoadSize();
    }

    void SpawnBullet()
    {
        Vector3 newBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 9.2f);
        GameObject newBullet = Instantiate(bulletPrefab, newBulletPos, transform.rotation);
        newBullet.transform.forward = -transform.forward;
        newBullet.transform.localScale = transform.localScale * joystickShot.bulletSizeAmplifier;
    }

    void UpdateRoadSize()
    {
        Vector3 roadScaleNew = new Vector3(roadStartScale.x * sizeAmplifier, roadStartScale.y, roadStartScale.z);
        road.localScale = roadScaleNew;
    }

    public void GameOver()
    {
        isJumping = false;
        canMove = false;
        isGameOver = true;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
