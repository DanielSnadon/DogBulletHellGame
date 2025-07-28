using System.Collections;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private bool canDefinePosition = true;
    public Vector3 playerPos;
    public GameObject bullet;
    public GameObject newestBullet;
    private Vector3 enemyPos;
    private Rigidbody enemyRb;
    public float perciseTime = 5.0f;
    public float speedForce = 3.0f;
    public float fireTime = 5.0f;
    public float bulletSpeed = 1.0f;
    public int bulletCount = 1;
    public float precision = 3.0f;
    public bool readyToShoot = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsByType<PlayerController>(FindObjectsSortMode.None).Length > 0)
        {
            enemyPos = transform.position;
            if (canDefinePosition)
            {
                DefinePlayerPosition();
            }
            RunToPos();
            if (readyToShoot)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    Shoot();
                }
                readyToShoot = false;
                StartCoroutine(WeaponReload(fireTime));
            }
        }
    }
    void Shoot()
    {
        Vector3 targetPos = CalculatePrecision((playerPos - enemyPos));
        newestBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(targetPos, Vector3.up));
        Rigidbody bulletRb = newestBullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(targetPos * bulletSpeed, ForceMode.Impulse);
    }
    Vector3 CalculatePrecision(Vector3 vector)
    {
        return (new Vector3(vector.x + Random.Range(-precision, precision), 0, vector.z + Random.Range(-precision, precision))).normalized;
    }
    IEnumerator WeaponReload(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        readyToShoot = true;
    }
    void RunToPos()
    {
        Vector3 lookVector = (playerPos - enemyPos).normalized;
        enemyRb.AddForce(lookVector * speedForce);
    }
    void DefinePlayerPosition()
    {
        playerPos = player.transform.position;
        canDefinePosition = false;
        StartCoroutine(FollowCooldown(perciseTime));
    }
    IEnumerator FollowCooldown(float perciseTime)
    {
        yield return new WaitForSeconds(perciseTime);
        canDefinePosition = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathPart"))
        {
            Destroy(gameObject);
        }
    }
}
