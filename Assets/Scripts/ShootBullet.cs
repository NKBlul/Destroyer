using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    private float bulletSpeed = 1.5f;


    public Transform attackPoint;

    public float upwardForce;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 tarPoint;
            if (Physics.Raycast(raycast, out hit))
            {
                tarPoint = hit.point;
            }
            else
            {
                tarPoint = raycast.GetPoint(75);
            }

            Vector3 directionWithoutSpread = tarPoint - transform.position;

            float x = Random.Range(-1, 1);
            float y = Random.Range(-1, 1);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.up * upwardForce, ForceMode.Impulse);
            Destroy(bullet, 1.0f);
        }
    }
}
