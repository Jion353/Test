using UnityEngine;

public class Enemie : MonoBehaviour
{
    Rigidbody2D rig;
    [SerializeField] Transform target;
    [SerializeField] float speed = 2;
    [SerializeField] float targetDistans = 3;
    [SerializeField] float attackDistans = 0.3f;
    [SerializeField] int heal = 50;
    public static int moveDir;
    EnemiesAnim anim;
    bool isLife = true;
    bool canMove = true;
    bool isAttack = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim= GetComponent<EnemiesAnim>();
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isLife)
        {
            if (canMove)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        float Dir = Vector2.SignedAngle(Vector2.up, target.position - transform.position);
        if (Dir <= 22.5 && Dir > -22.5)
        {
            moveDir = 1;
        }
        else if (Dir <= -22.5 && Dir > -67.5)
        {
            moveDir = 2;
        }
        else if (Dir <= -67.5 && Dir > -112.5)
        {
            moveDir = 3;
        }
        else if (Dir <= -112.5 && Dir > -157.5)
        {
            moveDir = 4;
        }
        else if (Dir <= -157.5 || Dir > 157.5)
        {
            moveDir = 5;
        }
        else if (Dir <= 157.5 && Dir > 112.5)
        {
            moveDir = 6;
        }
        else if (Dir <= 112.5 && Dir > 67.5)
        {
            moveDir = 7;
        }
        else if (Dir <= 67.5 && Dir > 22.5)
        {
            moveDir = 8;
        }
        if (Vector2.Distance(transform.position, target.position) > attackDistans && Vector2.Distance(transform.position, target.position) < targetDistans)
        {
            anim.Walk();
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) <= attackDistans)
        {
            moveDir = 0;
            anim.Walk();
            Attack();
        }
        else
        {
            moveDir = 0;
            anim.Walk();
        }
    }

    public void Hit()
    {
        heal -= 25;
        if(heal <=0)
        {
            isLife = false;
            anim.Death(!isLife);
            Settings.enKill++;
            Invoke("Death", 2);
        }
    }

    private void Attack()
    {
        if (!isAttack)
        {
            target.GetComponent<Player>().Hit();
            isAttack = true;
            anim.Attack(isAttack);
            canMove = false;
            Invoke("CanMove", 0.45f);
            Invoke("CanAttack", 2f);
        }
    }

    private void CanMove()
    {
        canMove = true;
        anim.Attack(!isAttack);
    }
    private void CanAttack()
    {
        isAttack = false;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
