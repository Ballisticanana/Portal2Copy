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
		public bool fire;
        public bool menu;
		public bool up;
		public bool down;
		public bool left;
		public bool right;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

        public void OnFire(InputValue value)
        {
			
            FireInput(value.isPressed);
        }

		public void OnMenu(InputValue value)
        {
			
            MenuInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        public void OnUp(InputValue value)
        {
            UpInput(value.isPressed);
        }

        public void OnDown(InputValue value)
        {
            DownInput(value.isPressed);
        }

        public void OnLeft(InputValue value)
        {
            LeftInput(value.isPressed);
        }

        public void OnRight(InputValue value)
        {
            RightInput(value.isPressed);
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

        public void FireInput(bool newFireState)
        {
            fire = newFireState;
        }

        public void MenuInput(bool newMenuState)
        {
            menu = newMenuState;
        }

        public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void UpInput(bool newUpState)
        {
            up = newUpState;
        }

        public void DownInput(bool newDownState)
        {
            down = newDownState;
        }

        public void LeftInput(bool newLeftState)
        {
            left = newLeftState;
        }

        public void RightInput(bool newRightState)
        {
            right = newRightState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}