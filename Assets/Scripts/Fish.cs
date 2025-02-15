using System;
using UnityEngine;

    public class Fish : MonoBehaviour
    {
        public Action<GameObject> onOut;
        private GameObject directionPoint;
        private float maxPos = 15;

        private int _score;
        public int Score
        {
            get => _score;
            set => _score = value;
        }
        
        private void Awake()
        {
            directionPoint = new GameObject("point");
            directionPoint.transform.SetParent(transform);
            directionPoint.transform.localPosition = new Vector3(-1, 0, 0);
        }

        private void OnDestroy()
        {
            onOut.Invoke(gameObject);
        }

        private void Update()
        {
            float sizeSpeed = 0.66f + Math.Abs(transform.localScale.x) / 3f;
            transform.Translate(((directionPoint.transform.position-transform.position).normalized)*Time.deltaTime * sizeSpeed);
            if (transform.position.x > maxPos || transform.position.x < -maxPos)
            {
                onOut.Invoke(gameObject);
                enabled = false;
            }
        }
    }
    
