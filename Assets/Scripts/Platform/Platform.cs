    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Platform : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float distance = 5f; 
        [SerializeField] private bool moveHorizontally = true; 
        [SerializeField] private bool moveVertically = false;

        private Vector3 _startPosition;
        [SerializeField] private int direction = 1;
    

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (moveHorizontally)
            {
                transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

                if (Mathf.Abs(transform.position.x - _startPosition.x) >= distance)
                {
                    direction *= -1;
                }
            }

            // Platformun dikey hareketi
            if (moveVertically)
            {
                transform.Translate(Vector3.up * direction * speed * Time.deltaTime);

                if (Mathf.Abs(transform.position.y - _startPosition.y) >= distance)
                {
                    direction *= -1;
                }
            }
        }
    }