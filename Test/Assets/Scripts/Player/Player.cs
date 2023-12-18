using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animation anim;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isHit = false;
    [SerializeField] private bool isLife = true;
    private Camera cam;
    public static int attackDir;
    [SerializeField] private LayerMask enemies;
    [SerializeField] int heal = 100;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        cam = Camera.main;
    }

    void Update()
    {
        if (isLife)
        {
            if (!isAttack)
            {
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
       if(isLife)
            Move();

    }

    private void Move()
    {
        float x;
        float y;
        if (!isAttack)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            x = 0;
            y = 0;
        }
        anim.Walk((int)x, (int)y);
        rigidbody.velocity = new Vector2(x * moveSpeed, y * moveSpeed);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttack = true;
            Vector3 mousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            float Dir = Vector2.SignedAngle(Vector2.up, mousPosition - transform.position);
            if(Dir <= 22.5 && Dir > -22.5)
            {
                attackDir = 1;
            }
            else if(Dir <= -22.5 && Dir > -67.5)
            {
                attackDir= 2;
            }
            else if(Dir <= -67.5 && Dir > -112.5)
            {
                attackDir= 3;
            }
            else if (Dir <= -112.5 && Dir > -157.5)
            {
                attackDir = 4;
            }
            else if (Dir <= -157.5 || Dir > 157.5)
            {
                attackDir = 5;
            }
            else if (Dir <= 157.5 && Dir > 112.5)
            {
                attackDir = 6;
            }
            else if (Dir <= 112.5 && Dir > 67.5)
            {
                attackDir = 7;
            }
            else if (Dir <= 67.5 && Dir > 22.5)
            {
                attackDir = 8;
            }
            anim.Attack();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousPosition - transform.position, 0.5f, enemies);
            if (hit.collider != null)
            {
                if(hit.collider.GetComponent<Enemie>())
                {
                    hit.collider.GetComponent<Enemie>().Hit();
                }
            }
            Invoke("StopAttack", 0.55f);
        }
    }
    
    public void Hit()
    {
        if (isLife)
        {
            if (!isHit)
            {
                heal -= 10;
                isHit = true;
                anim.Hit(isHit);
                Invoke("StopHit", 0.45f);
            }
            if (heal <= 0)
            {
                isLife = false;
                anim.Death(!isLife);
                Invoke("Reload", 2f);
            }
        }
    }

    private void Reload()
    {
        Settings.RelodScene();
    }

    private void StopAttack()
    {
        isAttack = false;
        anim.Attack();
    }

    private void StopHit()
    {
        isHit = false;
        anim.Hit(isHit);
    }
}
