using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //�ڕW�F�����_���G���J�E���g
    ///�E�G�ɂ�������o�g���V�[���ɂ���(Panel������)
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask encountLayer;

    [SerializeField] Battler battler;

    public UnityAction<Battler> OnEncounts; //Encount�������Ɏ��s�������֐���o�^�ł���

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

            if (x != 0) //�΂߈ړ��֎~
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


    // 1�}�X���X�ɋ߂Â���
    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 targetPos = transform.position + direction;�@//targetPos��3�����̖ڕW�l��ݒ肵�܂��B���̂��߂̌v�Z�Ƃ���transform.position�͌��݂̈ʒu�Adirection�͈ړ��̕����ł��B
        if(IsWalkable(targetPos) == false)
        {
            isMoving = false;
            yield break;
        }
        //���݂ƃ^�[�Q�b�g�̏ꏊ���Ⴄ�Ȃ�A�߂Â�������
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) //�ڕW�ltargetPos�Ɍ��ݒntransform.position���͂��Ă��Ȃ��̂ŁA
        {
            //�߂Â���
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime); //(���ݒn�A�ڕW�l�A���x)
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        //�G�ɂ��������ׂ�
        CheckForEncounts();

    }

    void CheckForEncounts()
    {

        //�ړ������n�_�ɁA�G�����邩���f����
        Collider2D encount = Physics2D.OverlapCircle(transform.position, 0.2f, encountLayer);
        //if( Physics2D.OverlapCircle(transform.position, 0.2f, encountLayer))
        if(encount)
        {
            if(Random.Range(0,100)<10) //0-99�܂ł̐����������_�����҂ׂ�āA���̐�����10��菬����������
            {
                Battler enemy = encount.GetComponent<EncountArea>().GetRandomBattler();
                OnEncounts?.Invoke(enemy);//����OnEncounts�Ɋ֐����o�^����Ă���Ύ��s����
            }
        }
    }

    //������ړ�����Ƃ���Ɉړ��ł��邩���肷��l�֐�
    bool IsWalkable(Vector3 targetPos)
    {
        //targetPos�𒆐S�ɉ~�`��Ray�����FSolidObjectsLayer�ɂԂ�������true���Ԃ��Ă���.������false
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) == false;
    }

}
