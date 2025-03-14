using Character;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CrouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public CharacterBehaviour playerController;

        public void OnPointerDown(PointerEventData eventData)
        {
            playerController.HandleCrouch(true); // Se activa cuando el botón se presiona
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            playerController.HandleCrouch(false); // Se desactiva cuando el botón se suelta
        }
    }
}