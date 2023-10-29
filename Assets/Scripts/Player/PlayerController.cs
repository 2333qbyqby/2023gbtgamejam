using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 inputDir;
    public float speed;
    private Vector2 LastInputDir;
    private Rigidbody rb;
    [Header("震动事件")]
    public UnityEvent shakeEvent;
    [Header("粒子效果")]
    public ParticleSystem hitParticle;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    private void Start()
    {

    }

    private void Update()
    {
        if(rb.velocity.magnitude<=0.1)
        {
            GameInput.Instance.EnablePlayInput();
            
            //this.transform.position -= new Vector3(LastInputDir.x * 0.2f, 0, LastInputDir.y * 0.2f);
        }
        //Debug.Log(rb.velocity);
    }
    /// <summary>
    /// 移动函数
    /// </summary>
    public void Move()
    {
        
       inputDir = GameInput.Instance.GetMoveDir();
       if(inputDir.x!=0  )
       {
           LastInputDir = new Vector2(inputDir.x, 0);
       }
       else if(inputDir.y != 0)
       {
           LastInputDir =new Vector2(0,inputDir.y);//记录上一次的移动方向
       }
       
        Vector3 realDir=new Vector3(LastInputDir.x,0,LastInputDir.y);
        this.rb.AddForce(realDir*speed*Time.fixedDeltaTime*10);
        //Debug.Log(realDir);
        //this.transform.Translate(realDir * speed * Time.fixedDeltaTime);
        if (LastInputDir != Vector2.zero)
        {
            GameInput.Instance.DisablePlayInput();
        }
    }

    private void FixedUpdate()
    {
        
        Move();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)//检测墙壁
        {
            rb.velocity = Vector3.zero;
            LastInputDir = Vector2.zero;
            AudioManager.instance.PlaySound(2);//触发音效
            shakeEvent?.Invoke();//调用震动事件
            PlayParticalEffect();            
            GameInput.Instance.EnablePlayInput();
        }
    }
    private void PlayParticalEffect()
    {
        var main = hitParticle.main;
        Color color = ColorController.instance.currentColor;
        if(color ==Color.black)
        {
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = new Color(color.r, color.g, color.b, 1);
        }
        hitParticle.Play();
    }
}
