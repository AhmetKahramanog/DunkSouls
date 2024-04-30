using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 distance;
    private float mouseX, mouseY;
    [SerializeField] private float mouseSentitivity = 1f;
    [SerializeField] private float camFollowSpeed = 15f;
    [SerializeField] private List<Transform> targets;
    [SerializeField] private Vector3 offset;
    [SerializeField] private List<KnighHealth> knighHealths;

    private MonsterEnemyMovement lockedEnemy;

    private KnighHealth knighLockedEnemy;

    [SerializeField] private List<MonsterEnemyMovement> monsters;

    private float range;
    private bool isRange = false;
    private bool lockCam = false;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        lockedEnemy = FindAnyObjectByType<MonsterEnemyMovement>();
        knighLockedEnemy = FindAnyObjectByType<KnighHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToogleCameraLock();
        }

        if (lockCam)
        {
            RotateTowardTarget();
        }
        CheckDistance();
        CheckTargetLock();

    }

    private void LateUpdate()
    {
        //mouseX += Input.GetAxis("Mouse X") * mouseSentitivity;
        //mouseY += Input.GetAxis("Mouse Y") * mouseSentitivity;

        //transform.position = Vector3.Lerp(transform.position, player.transform.position + distance, camFollowSpeed * Time.deltaTime);
        //transform.eulerAngles = new Vector3(mouseY, mouseX, 0f);
        //mouseY = Mathf.Clamp(mouseY, -12f, 40f);

        if (!lockCam && !ShopInteract.IsShopOpen)
        {
            RotateCameraHandle();
        }
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + distance, camFollowSpeed * Time.deltaTime);

    }

    private void RotateCameraHandle()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSentitivity;
        mouseY += Input.GetAxis("Mouse Y") * mouseSentitivity;

        //transform.position = Vector3.Lerp(transform.position, player.transform.position + distance, camFollowSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + distance, camFollowSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0f);
        mouseY = Mathf.Clamp(mouseY, -12f, 40f);
    }

    private void ToogleCameraLock()
    {
        //lockCam = !lockCam;
        if (isRange)
        {
            lockCam = !lockCam;
        }
        else
        {
            lockCam = false;
        }
    }

    private void RotateTowardTarget()
    {
        //range = Vector3.Distance(target.transform.position, transform.position);
        //if (range < 5 && !MonsterEnemyMovement.isDie)
        //{
        //    targetLock.SetActive(true);
        //    transform?.LookAt(target.transform.position);
        //    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        //}
        TargetListRange();
    }

    private void TargetListRange()
    {
        foreach (var target in targets)
        {
            range = Vector3.Distance(target.transform.position, transform.position);
            if (range < 5 && !IsDieMonster())
            {
                lockedEnemy = target.GetComponent<MonsterEnemyMovement>();
                knighLockedEnemy = target.GetComponent<KnighHealth>();
                transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, camFollowSpeed * Time.deltaTime);
                transform?.LookAt(target.transform.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
        }
    }

    private void CheckDistance()
    {
        foreach (var target in targets)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget < 5)
            {
                isRange = true;
                break;
            }
            else
            {
                isRange = false;
            }
        }
    }

    private void CheckTargetLock()
    {
        foreach (var monster in monsters)
        {
            if (lockCam && monster == lockedEnemy)
            {
                monster.enemyHealthBar.gameObject.SetActive(true);
            }
            else
            {
                monster.enemyHealthBar.gameObject.SetActive(false);
            }
        }

        foreach (var knight in knighHealths)
        {
            if (lockCam && knight == knighLockedEnemy)
            {
                knight.knightEnemyHealthBar.gameObject.SetActive(true);
            }
            else
            {
                knight.knightEnemyHealthBar.gameObject.SetActive(false);
            }
        }

    }

    private bool IsDieMonster()
    {
        foreach (var monster in monsters)
        {
            if (monster.isDie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
