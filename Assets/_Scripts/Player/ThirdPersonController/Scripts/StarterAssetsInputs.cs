using UnityEngine;
using UnityEngine.Serialization;
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
	

		[Header("Shortcuts Input Values")]
		public bool levelUp;
		public bool levelDown;

		[FormerlySerializedAs("changeCameraRigth")]
		[Header("POV Input Values")]
		public bool changeCameraRight;
		public bool changeCameraLeft;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("PauseMenu")]
		public GameObject pauseMenu;
		

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
		
		public void OnBlock(InputValue value)
		{
			BlockInput(value.isPressed);
			OnApplicationFocus(!value.isPressed);
		}


		public void OnLevelUp(InputValue value)
		{
			LevelUpInput(value.isPressed);
		}
		
		public void OnLevelDown(InputValue value)
		{
			LevelDownInput(value.isPressed);
		}

		public void OnPause(InputValue value)
		{
			PauseInput(value.isPressed);
		}

		public void OnChangeCameraRight(InputValue value)
		{
			CCRInput(value.isPressed);
		}
		
		public void OnChangeCameraLeft(InputValue value)
		{
			CCLInput(value.isPressed);
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

		public void LevelUpInput(bool newLevelUpState)
		{
			levelUp = newLevelUpState;
		}
		public void LevelDownInput(bool newLevelDownState)
		{
			levelDown = newLevelDownState;
		}

		public void PauseInput(bool newPauseState)
		{
			if(newPauseState)
			{
				OnApplicationFocus(!newPauseState);
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
			}
			else
			{
				OnApplicationFocus(!newPauseState);
				pauseMenu.SetActive(false);
				Time.timeScale = 1;
			}
		}

		public void CCRInput(bool newccrState)
		{
			changeCameraRight = newccrState;
		}
		
		public void CCLInput(bool newcclState)
		{
			changeCameraLeft = newcclState;
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