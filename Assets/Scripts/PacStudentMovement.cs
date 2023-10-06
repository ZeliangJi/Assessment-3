using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public float speed = 5f;  // 控制PacStudent的速度
    public AudioClip movingAudioClip;  // 移动时的音效
    private AudioSource audioSource;  // 音频源组件
    private Animator animator;  // 动画器组件
    private Vector3[] directions = { Vector3.right, Vector3.down, Vector3.left, Vector3.up };  // 移动方向数组
    private int currentDirectionIndex = 0;  // 当前的方向索引

    private Vector3[] corners = { new Vector3(1, 1, 0), new Vector3(5, 1, 0), new Vector3(5, 5, 0), new Vector3(1, 5, 0) };  // 角落位置数组
    private int currentCornerIndex = 0;  // 当前的角落索引

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

        // 如果PacStudent接近角落，则更改方向
        if (distanceToCorner < 0.1f)
        {
            currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;
            currentCornerIndex = (currentCornerIndex + 1) % corners.Length;
        }
    }

    void UpdateAnimationAndAudio(Vector3 direction)
    {
        // 更新动画
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        // 播放音效
        if (!audioSource.isPlaying)
        {
            audioSource.clip = movingAudioClip;
            audioSource.Play();
        }
    }
}
