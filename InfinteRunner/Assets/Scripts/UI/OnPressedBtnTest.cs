using System;
using Character;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPressedBtnTest : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private CharacterBehaviour character;
    public bool isJump;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isJump)
        {
            character.HandleJump();
        }

        if (!isJump)
        {
            character.HandleDoubleTap();
        }
        
    }

}
