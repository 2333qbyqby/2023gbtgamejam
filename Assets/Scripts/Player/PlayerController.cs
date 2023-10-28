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
    [Header("���¼�")]
    public UnityEvent shakeEvent;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    private void Start()
    {

    }
    /// <summary>
    /// �ƶ�����
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
           LastInputDir =new Vector2(0,inputDir.y);//��¼��һ�ε��ƶ�����
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
        if (collision.gameObject.layer == 6)//���ǽ��
        {
            rb.velocity = Vector3.zero;

            this.transform.position -= new Vector3(LastInputDir.x*0.1f, 0, LastInputDir.y*0.1f);
            LastInputDir = Vector2.zero;

            AudioManager.instance.PlaySound(2);//������Ч
            shakeEvent?.Invoke();//�������¼�
            GameInput.Instance.EnablePlayInput();
        }
    }
    
}
