using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEye : Enemy
{
    #region Varibles

    [SerializeField] private GameObject laserBeam;
    [SerializeField] private Animator animator;
    private float laserBeamAnimLength = 1.6f;
    private float eyeAnimLength = 0.75f;
    [SerializeField] private float stayOpenTime;
    [SerializeField] private float farthestLeft;
    [SerializeField] private float farthestRight;
    private Vector2 originalPosition;
    private Vector2 newPosition;
    public bool canShoot;
    public bool isMoving;

    #endregion

    #region Methods
    void Start() 
    {
        canShoot = true;
        isMoving = false;
        originalPosition = transform.position;
    }

    void Update() 
    {
        if (canShoot && stayOpenTime <= 2f)
        {
            StartCoroutine(ShootLaser());
            canShoot = false;
        }
        else if (!isMoving && stayOpenTime <= 0f)
        {
            StartCoroutine(MovePosition());
            isMoving = true;
        }
        if (stayOpenTime >= 0f)
            stayOpenTime -= Time.deltaTime;
    }

    IEnumerator ShootLaser()
    {
        laserBeam.SetActive(true);

        yield return new WaitForSeconds(laserBeamAnimLength);

        laserBeam.SetActive(false);
    }

    IEnumerator MovePosition()
    {
        animator.SetTrigger("isClosing");
        yield return new WaitForSeconds(eyeAnimLength);

        transform.position = originalPosition;
        float randomXCord = Random.Range(farthestLeft, farthestRight);
        transform.position = new Vector3(randomXCord, transform.position.y, 0f);

        animator.SetTrigger("isOpening");
        yield return new WaitForSeconds(eyeAnimLength);

        stayOpenTime = 3.5f;
        canShoot = true;
        isMoving = false;
    }

    #endregion
}
