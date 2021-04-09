using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    [SerializeField] GameObject bulletInsPosi;
    [SerializeField] int bulletNum = 30;
    [SerializeField] GameObject originBullet;
    List<GameObject> bullets = new List<GameObject>();
    int bulletIndex = 0;
    Quaternion bulletRotate;

    [SerializeField] float firingInterval = 0.3f;
    float intervalTime = 0;

    private void Awake()
    {
        for (int i = 0; i < bulletNum; i++)
        {
            bullets.Add(Instantiate(originBullet,bulletInsPosi.transform.position,Quaternion.identity));
            bullets[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        intervalTime += Time.deltaTime;
        if (intervalTime > firingInterval)
        {
            intervalTime = 0;
            bullets[bulletIndex].transform.position = bulletInsPosi.transform.position;
            //bulletRotate = bulletInsPosi.transform.rotation;
            bullets[bulletIndex].transform.rotation= bulletInsPosi.transform.rotation;
            bullets[bulletIndex].SetActive(true);
            bulletIndex++;
            if (bulletIndex >= bullets.Count)
            {
                bulletIndex = 0;
            }
        }
    }
}
