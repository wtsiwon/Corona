using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class Enemy : MonoBehaviour
{
    [Header("개체 속성")]
    [SerializeField] protected float spd;
    [SerializeField] protected float hp;

    [Header("공격 속성")]
    public float atkDmg;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Bullet bulletObj;
    [SerializeField] protected float bulletSpd;
    [SerializeField] protected float bulletInterval;

    [Header("연속 공격 속성")]
    [SerializeField] private bool isUnlimitShotcnt;
    [SerializeField] private int shotCnt;
    [SerializeField] private float continiousShotInterval;

    [Header("방사형 공격 속성")]
    [SerializeField] private int wayCnt;

    [Header("이펙트")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;

    protected Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isUnlimitShotcnt) Invoke("Attack", 1f);
        else InvokeRepeating("Attack", 1f, bulletInterval);

        Move();
    }
    protected abstract void Attack();
    private void Move()
    {
        rb.velocity = Vector3.back * spd;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Instantiate(hitEffect).transform.position = other.transform.position;

            hp = Mathf.Max(0, hp - other.GetComponent<Bullet>().dmg);
            if(hp == 0)
            {
                OnDie();
            }
        }

    }
    public void OnDie()
    {
        Instantiate(dieEffect).transform.position = transform.position;
        Destroy(gameObject);
    }

    protected void WayShot()
    {
        firePos.LookAt(GameObject.Find("Player").transform.position);

        Quaternion firstRotation = firePos.rotation;

        firePos.Rotate(Vector3.up * -5 * (wayCnt / 2));

        for (int i = 1; i < wayCnt; i++)
        {
            Bullet bullet = Instantiate(bulletObj);
            bullet.transform.position = firePos.position;
            bullet.SetBullet(atkDmg, firePos.forward, bulletSpd);
            firePos.Rotate(Vector3.up * 5);
        }

        firePos.rotation = firstRotation;
    }

    protected void TornadoShot()
    {
        StartCoroutine(TornadoShotCoroutine());
    }
    private IEnumerator TornadoShotCoroutine()
    {
        firePos.LookAt(GameObject.Find("Player").transform.position);

        while (true)
        {
            Quaternion firstRotation = firePos.rotation;

            for (int i = 1; i < wayCnt; i++)
            {
                Bullet bullet = Instantiate(bulletObj);
                bullet.transform.position = firePos.position;
                bullet.SetBullet(atkDmg, firePos.forward, bulletSpd);
                firePos.Rotate(Vector3.up * 360 / wayCnt);
            }

            firePos.rotation = firstRotation;
            firePos.Rotate(Vector3.up * 5);
        }
        yield return new WaitForSeconds(continiousShotInterval);
    }
}


