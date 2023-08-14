using CurvedUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BNG {
    public class CurvedUIPointer : MonoBehaviour {
        [Header("Visuals")]
        public LineRenderer LaserLine;

        [Tooltip("If specified will show this object at the end of the raycast")]
        public GameObject Cursor;

        [Header("Input")]
        [Tooltip("Input to send to CurvedUI to determine when a button is held down or not. Should typicall by RightTrigger or LeftTrigger (not to be confused with 'RightTriggerDown').")]
        public ControllerBinding ButtonInput = ControllerBinding.RightTrigger;

        [Header("Shown for Debug :")]
        public bool InputDown = false;

        // Cache these variables used in raycast
        private Ray _ray;
        private Vector3 _hitPosition;

        void Start() {
            if (LaserLine == null) {
                LaserLine = GetComponent<LineRenderer>();
            }
        }

        void Update() {

            // Update Curved UI ray position and button state
            CurvedUIInputModule.CustomControllerRay = new Ray(transform.position, transform.forward);
            CurvedUIInputModule.CustomControllerButtonState = InputDown = InputBridge.Instance.GetControllerBindingValue(ButtonInput) || Input.GetMouseButton(0);

            UpdateVisuals();
        }

        // Draw a LineRenderer between our start point and if we hit a Canvas
        // Based off of CurvedUI's 'CurvedUILaserBeam' example
        public void UpdateVisuals() {
            _ray = new Ray(transform.position, transform.forward);

            //make laser beam hit stuff it points at.
            if (LaserLine) {
                // Change the laser's length depending on where it hits
                bool hitFound = false;
                _hitPosition = transform.position;

                RaycastHit hit;
                if (Physics.Raycast(_ray, out hit, 10000f, CurvedUIInputModule.Instance.RaycastLayerMask)) {
                    _hitPosition = hit.point;

                    // Find if we hit a canvas
                    CurvedUISettings cuiSettings = hit.collider.GetComponentInParent<CurvedUISettings>();
                    if (cuiSettings != null) {

                        // Find if there are any canvas objects we're pointing at. We only want transforms with graphics to block the pointer. (that are drawn by canvas => depth not -1)
                        hitFound = cuiSettings.GetObjectsUnderPointer().FindAll(x => x != null && x.GetComponent<Graphic>() != null && x.GetComponent<Graphic>().depth != -1).Count > 0;
                    }
                }

                // Update the LineRenderer
                if (hitFound) {

                    LaserLine.enabled = true;

                    float lineDistance = Vector3.Distance(transform.position, _hitPosition);
                    LaserLine.useWorldSpace = false;
                    LaserLine.SetPosition(0, Vector3.zero);
                    LaserLine.SetPosition(1, new Vector3(0, 0, lineDistance));

                    //LaserLine.useWorldSpace = true;
                    //LaserLine.SetPosition(0, transform.position);
                    //LaserLine.SetPosition(1, _hitPosition);

                    if (Cursor) {
                        Cursor.SetActive(true);
                        Cursor.transform.position = _hitPosition;
                        Cursor.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    }
                }
                else {
                    LaserLine.enabled = false;

                    if (Cursor) {
                        Cursor.SetActive(false);
                    }
                }
            }
        }
    }
}

