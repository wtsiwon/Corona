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
        //else InvokeRepeating("Attack", 1f, bulle);

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
}
