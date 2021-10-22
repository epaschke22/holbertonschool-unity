using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public CharacterController controller;
    public Transform cam;
    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public AudioSource grassWalk;
    public AudioSource stoneWalk;
    public AudioSource landing;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public bool canMove = false;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
		{
            velocity.y = -2f;
            playerAnimator.ResetTrigger("Respawn"); //fixes bug that causes this to persist after actually respawning
        }
        playerAnimator.SetBool("IsGrounded", isGrounded);


        if (canMove)
		{
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            playerAnimator.SetFloat("Speed", direction.magnitude);

            string matName = GetMaterialOnTriangle();

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);

                if (matName == "Green_leafs")
				{
                    if (!grassWalk.isPlaying)
					{
                        grassWalk.Play();
					}
                    if (stoneWalk.isPlaying)
                    {
                        stoneWalk.Stop();
                    }
                }
                else if (matName == "Wood")
				{
                    if (!stoneWalk.isPlaying)
                    {
                        stoneWalk.Play();
                    }
                    if (grassWalk.isPlaying)
                    {
                        grassWalk.Stop();
                    }
                }
				else
				{
                    if (grassWalk.isPlaying)
                    {
                        grassWalk.Stop();
                    }
                    if (stoneWalk.isPlaying)
                    {
                        stoneWalk.Stop();
                    }
                }
            }
			else
			{
                if (grassWalk.isPlaying)
                {
                    grassWalk.Stop();
                }
                if (stoneWalk.isPlaying)
                {
                    stoneWalk.Stop();
                }
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (transform.position.y < -10f)
		{
            transform.position = new Vector3(0, 30, 0);
            playerAnimator.SetTrigger("Respawn");
        }
    }

    string GetMaterialOnTriangle()
    {
        RaycastHit hit;

        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, 0.8f))
        {
            MeshCollider collider = hit.collider as MeshCollider;
            // Remember to handle case where collider is null because you hit a non-mesh primitive...

            Mesh mesh = collider.sharedMesh;

            // There are 3 indices stored per triangle
            int limit = hit.triangleIndex * 3;
            int submesh;
            for (submesh = 0; submesh < mesh.subMeshCount; submesh++)
            {
                int numIndices = mesh.GetTriangles(submesh).Length;
                if (numIndices > limit)
                    break;

                limit -= numIndices;
            }

            Material material = collider.GetComponent<MeshRenderer>().sharedMaterials[submesh];
            return material.name;
        }
        return null;
    }
}
