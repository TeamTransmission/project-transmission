using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform target;
        private float damping = 1;
        private float lookAheadFactor = 3;
        private float lookAheadReturnSpeed = 0.5f;
        private float lookAheadMoveThreshold = 0.1f;

        //private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        
        private float currentTargetXLoc;

        private int deadZoneWidth = 5;

        private CharacterSelector charSelect;

        private float cameraHeightAbovePlayer = 3f;

        public float minimumXLoc=0;
        public float maximumXLoc=0;

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

            float cameraMinByDeadZone = currentTargetXLoc - deadZoneWidth / 2;                       

            float cameraMaxByDeadZone = currentTargetXLoc + deadZoneWidth / 2;                     

            float newCameraPosition = currentCameraXLoc;

            //ensure the camera only moves once player has exited the dead zone
            if (currentCameraXLoc > cameraMaxByDeadZone)
            {
                newCameraPosition = cameraMaxByDeadZone;
            }
            else if (currentCameraXLoc < cameraMinByDeadZone)
            {
                newCameraPosition = cameraMinByDeadZone;
            }

            //ensure camera doesn't go past min/max values
            if (newCameraPosition < minimumXLoc)
            {
                newCameraPosition = minimumXLoc;
            }
            else if (newCameraPosition > maximumXLoc)
            {
                newCameraPosition = maximumXLoc;
            }

            //update camera pos           
            transform.position = new Vector3(newCameraPosition, transform.position.y, transform.position.z);  //leave fixed on y-axis for now
            
            m_LastTargetPosition = targetPos;

        }
    }
}
