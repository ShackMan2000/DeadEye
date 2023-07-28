using UnityEngine;
using UnityEngine.AI;

namespace TRavljen.UnitFormation.Demo
{
    /// <summary>
    /// Simple unit for demonstration of movement to target position
    /// with built-in Unity AI system. It also faces the angle of
    /// the formation after it reaches its destination.
    /// </summary>
    public class FormationUnit : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent agent;

        [SerializeField]
        private float facingAngle = 0f;

        [SerializeField]
        private bool faceOnDestination = false;

        [SerializeField, Tooltip("Speed with which the unit will rotate towards the formation facing angle.")]
        private float rotationSpeed = 100;

        public bool FacingRotationEnabled = true;

        public bool IsWithinStoppingDistance =>
            Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Vector3.Distance(agent.destination, transform.position) < agent.stoppingDistance &&
                faceOnDestination &&
                FacingRotationEnabled)
            {
                float currentAngle = transform.rotation.eulerAngles.y;
                var newAngle = Mathf.MoveTowardsAngle(currentAngle, facingAngle, rotationSpeed * Time.deltaTime);

                if (Mathf.Approximately(facingAngle, newAngle))
                {
                    faceOnDestination = false;
                }

                transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.up);
            }
            SetFacingAngleToCamera();
        }

        public void SetTargetDestination(Vector3 newTargetDestination, float newFacingAngle)
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }

            faceOnDestination = true;
            agent.destination = newTargetDestination;
            facingAngle = newFacingAngle;
        }

        public void SetFacingAngleToCamera()
        {
            Vector3 cameraDirection = Camera.main.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
            facingAngle = targetRotation.eulerAngles.y;
        }
    }
}