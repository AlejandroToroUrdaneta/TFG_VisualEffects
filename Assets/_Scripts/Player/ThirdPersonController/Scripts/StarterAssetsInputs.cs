using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool ability;
		public bool block;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			Vector2 movement = block ? Vector2.zero : value.Get<Vector2>();
			MoveInput(movement);
		}

		public void OnLook(InputValue value)
		{
			Vector2 looking = cursorInputForLook && block? Vector2.zero : value.Get<Vector2>();
			LookInput(looking);
		}

		public void OnJump(InputValue value)
		{
			JumpInput(!block && value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(!block && value.isPressed);
		}

		public void OnAbility(InputValue value)
		{
			AbilityInput(!block && value.isPressed);
		}
		
		private void OnBlock(InputValue value)
		{
			BlockInput(value.isPressed);
			OnApplicationFocus(!value.isPressed);
		}
		
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AbilityInput(bool newAbilityState)
		{
			ability = newAbilityState;
		}

		public void BlockInput(bool newBlockState)
		{
			block = newBlockState;
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked && hasFocus);
		}

		
		
		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}