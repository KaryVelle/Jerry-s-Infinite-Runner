using UnityEngine;

namespace Character
{
    public class CharacterBehaviour : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private float gravityScale = 1f;
        [SerializeField] public float doubleTapTime;
        [SerializeField] private float crouchScaleY = 0.5f;
        [SerializeField] private float fallGravityMultiplier = 2f;

        private Rigidbody2D _rb;
        [SerializeField] public bool isGrounded;
        private bool isCrouchingInAir = false; 
        private float originalScaleY;
        private float baseGravity; // Guardamos la gravedad original
        private bool isUp = true; // true = normal, false = invertida

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            baseGravity = gravityScale;
            _rb.gravityScale = baseGravity;
            originalScaleY = transform.localScale.y;
        }

        private void Update()
        {
            if (!isGrounded && isCrouchingInAir)
            {
                _rb.gravityScale = baseGravity * fallGravityMultiplier * (isUp ? 1 : -1); 
            }
            else
            {
                _rb.gravityScale = baseGravity * (isUp ? 1 : -1);
            }
        }

        public void HandleJump()
        {
            if (!isGrounded) return;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce * (isUp ? 1 : -1));
        }

        public void HandleDoubleTap()
        {
            if (!isGrounded) return;

            isUp = !isUp; // Invierte la dirección de la gravedad
            _rb.gravityScale = baseGravity * (isUp ? 1 : -1); // Aplica la gravedad correctamente

            if (Camera.main == null) return;

            var middleScreenY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0)).y;
            var position = transform.position;

            position.y = _rb.gravityScale switch
            {
                > 0 when position.y < middleScreenY => middleScreenY + 0.5f,
                < 0 when position.y > middleScreenY => middleScreenY - 0.5f,
                _ => position.y
            };

            transform.position = position;
        } 

        public void HandleCrouch(bool isCrouching)
        {
            if (isGrounded)
            {
                float targetScaleY = isCrouching ? crouchScaleY : originalScaleY;
                transform.localScale = new Vector3(transform.localScale.x, targetScaleY, transform.localScale.z);
            }
            else
            {
                isCrouchingInAir = isCrouching;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                isGrounded = true;
                isCrouchingInAir = false;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                isGrounded = false;
            }
        }
    }
}
