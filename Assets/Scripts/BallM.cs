using System;
using UnityEngine;
//script defines ball movements
//ball is treated like a rigidbody to make the game more challenging. It wont instantly change directions. It should be treated like a vehicle that requires acceleration
namespace UnityStandardAssets.Vehicles.Ball
{
    public class BallM : MonoBehaviour
    {
        [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
        private Rigidbody m_Rigidbody;


        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }


        public void Move(Vector3 moveDirection)
        {
            m_Rigidbody.AddForce(moveDirection*m_MovePower);

        }

    }
}
