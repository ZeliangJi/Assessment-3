using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public float speed = 5f;  // ����PacStudent���ٶ�
    public AudioClip movingAudioClip;  // �ƶ�ʱ����Ч
    private AudioSource audioSource;  // ��ƵԴ���
    private Animator animator;  // ���������
    private Vector3[] directions = { Vector3.right, Vector3.down, Vector3.left, Vector3.up };  // �ƶ���������
    private int currentDirectionIndex = 0;  // ��ǰ�ķ�������

    private Vector3[] corners = { new Vector3(1, 1, 0), new Vector3(5, 1, 0), new Vector3(5, 5, 0), new Vector3(1, 5, 0) };  // ����λ������
    private int currentCornerIndex = 0;  // ��ǰ�Ľ�������

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = directions[currentDirectionIndex];
        transform.position += direction * speed * Time.deltaTime;

        CheckForCornerAndChangeDirection();

        UpdateAnimationAndAudio(direction);
    }

    void CheckForCornerAndChangeDirection()
    {
        Vector3 currentCorner = corners[currentCornerIndex];
        float distanceToCorner = Vector3.Distance(transform.position, currentCorner);

        // ���PacStudent�ӽ����䣬����ķ���
        if (distanceToCorner < 0.1f)
        {
            currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;
            currentCornerIndex = (currentCornerIndex + 1) % corners.Length;
        }
    }

    void UpdateAnimationAndAudio(Vector3 direction)
    {
        // ���¶���
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        // ������Ч
        if (!audioSource.isPlaying)
        {
            audioSource.clip = movingAudioClip;
            audioSource.Play();
        }
    }
}
