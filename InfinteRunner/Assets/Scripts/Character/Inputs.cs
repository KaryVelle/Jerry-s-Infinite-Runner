using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Character;

public class Inputs : MonoBehaviour
{
    public CharacterBehaviour player;

    [SerializeField]private bool isCrouching;
    private bool _doubleTapDetected = false; // Variable para bloquear el salto si se detecta doble tap
    private Coroutine _jumpCoroutine; // Referencia a la corrutina de salto

    public void OnHold(InputAction.CallbackContext context)
    {
        if (!player.isGrounded) return;

        if (context.performed)
        {
            isCrouching = true;
            //player.HandleCrouch(isCrouching);
        }
        else if (context.canceled)
        {
            isCrouching = false;
           // player.HandleCrouch(isCrouching);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isCrouching || _doubleTapDetected) return; 

            Debug.Log("Intentando saltar...");
            
            // Cancelamos la corrutina anterior si había una en ejecución
            if (_jumpCoroutine != null)
            {
                StopCoroutine(_jumpCoroutine);
            }

            _jumpCoroutine = StartCoroutine(DelayedJump()); // Esperar antes de confirmar el salto
        }
    }

    private IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(player.doubleTapTime); // Esperar el tiempo del doble tap

        if (!_doubleTapDetected) // Si no hubo doble tap, ejecutar el salto
        {
            if (!isCrouching)
            {
                Debug.Log("Jump");
                player.HandleJump();
            }
        }
        
        _jumpCoroutine = null; // Resetear la referencia de la corrutina
    }

    public void OnDoubleTap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!player.isGrounded) return;

            _doubleTapDetected = true; 

            Debug.Log("Double Tap");
            player.HandleDoubleTap();

            // Resetear inmediatamente la detección para que el salto no se ejecute después
            _doubleTapDetected = false; 
            
            // Cancelamos la corrutina del salto para asegurarnos de que no ocurra después del doble tap
            if (_jumpCoroutine != null)
            {
                StopCoroutine(_jumpCoroutine);
                _jumpCoroutine = null;
            }
        }
    }
}
