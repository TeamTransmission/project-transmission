using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        //private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        
        private float currentTargetXLoc;
        
        public int deadZoneWidth = 5;

        private CharacterSelector charSelect;

        private float cameraHeightAbovePlayer = 2f;

        // Use this for initialization
        private void Start()
        {

            charSelect = FindObjectOfType<CharacterSelector>();
            
            m_LastTargetPosition = transform.position;
            
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {

            target = charSelect.activePlayer.transform;

            Vector3 targetPos = new Vector3(target.position.x, target.position.y+ cameraHeightAbovePlayer, target.position.z);

            currentTargetXLoc = targetPos.x;
                        
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (targetPos - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = targetPos + m_LookAheadPos;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping * (Time.timeScale / 2));
            
            float currentCameraXLoc = transform.position.x;

            float cameraMin = currentTargetXLoc - deadZoneWidth / 2;
            float cameraMax = currentTargetXLoc + deadZoneWidth / 2;

            if (currentCameraXLoc > cameraMax)
            {
                transform.position = new Vector3(cameraMax, newPos.y, transform.position.z);
            }
            else if (currentCameraXLoc < cameraMin)
            {
                transform.position = new Vector3(cameraMin, newPos.y, transform.position.z);
            }

            m_LastTargetPosition = targetPos;

        }
    }
}
