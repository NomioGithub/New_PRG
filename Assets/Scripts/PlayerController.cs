using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //目標：ランダムエンカウント
    ///・敵にあったらバトルシーンにいく(Panelをだす)
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask encountLayer;

    [SerializeField] Battler battler;

    public UnityAction<Battler> OnEncounts; //Encountした時に実行したい関数を登録できる

    Animator animator;
    bool isMoving;

    public Battler Battler { get => battler; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        battler.Init();
    }



    void Update()
    {
        if (isMoving == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (x != 0) //斜め移動禁止
            {
                y = 0;
            }
            if(x !=0 || y != 0)
            {
                animator.SetFloat("InputX", x);
                animator.SetFloat("InputY", y);
                StartCoroutine(Move(new Vector2(x, y)));
            }

            
        }
        animator.SetBool("IsMoving", isMoving);
    }


    // 1マス徐々に近づける
    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 targetPos = transform.position + direction;　//targetPosは3次元の目標値を設定します。そのための計算としてtransform.positionは現在の位置、directionは移動の方向です。
        if(IsWalkable(targetPos) == false)
        {
            isMoving = false;
            yield break;
        }
        //現在とターゲットの場所が違うなら、近づけ続ける
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) //目標値targetPosに現在地transform.positionが届いていないので、
        {
            //近づける
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime); //(現在地、目標値、速度)
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        //敵にあうか調べる
        CheckForEncounts();

    }

    void CheckForEncounts()
    {

        //移動した地点に、敵がいるか判断する
        Collider2D encount = Physics2D.OverlapCircle(transform.position, 0.2f, encountLayer);
        //if( Physics2D.OverlapCircle(transform.position, 0.2f, encountLayer))
        if(encount)
        {
            if(Random.Range(0,100)<10) //0-99までの数字がランダムに鰓べれて、その数字が10より小さかったら
            {
                Battler enemy = encount.GetComponent<EncountArea>().GetRandomBattler();
                OnEncounts?.Invoke(enemy);//もしOnEncountsに関数が登録されていれば実行する
            }
        }
    }

    //今から移動するところに移動できるか判定するl関数
    bool IsWalkable(Vector3 targetPos)
    {
        //targetPosを中心に円形のRayを作る：SolidObjectsLayerにぶつかったらtrueが返ってくる.だからfalse
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) == false;
    }

}
