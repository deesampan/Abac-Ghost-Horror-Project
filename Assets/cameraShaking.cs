    using UnityEngine;
    using System.Collections;

    public class cameraShaking : MonoBehaviour
    {
        public float shakeAmount = 0.02f;
        public float shakeDuration = 0.1f; // Duration for each individual shake pulse
        public float dampingSpeed = 0.1f; // How quickly the shake subsides

        private Vector3 originalPos;
        private Coroutine shakeCoroutine;

        void Awake()
        {
            originalPos = transform.localPosition;
        }

        public void StartShake()
        {
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
            }
            shakeCoroutine = StartCoroutine(ShakeCameraCo());
        }

        public void StopShake()
        {
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                shakeCoroutine = null;
                // Smoothly return to original position
                StartCoroutine(DampToOriginalPos());
            }
        }

        private IEnumerator ShakeCameraCo()
        {
            float currentShakeAmount = shakeAmount;
            while (true) // Continuous shake while walking
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * currentShakeAmount;
                yield return new WaitForSeconds(shakeDuration);
            }
        }

        private IEnumerator DampToOriginalPos()
        {
            while (Vector3.Distance(transform.localPosition, originalPos) > 0.001f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * dampingSpeed);
                yield return null;
            }
            transform.localPosition = originalPos; // Ensure it snaps to exact original position
        }
    }
