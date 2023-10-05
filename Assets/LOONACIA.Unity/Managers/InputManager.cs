using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace LOONACIA.Unity.Managers
{
	public class InputManager
	{
		private readonly Dictionary<string, IInputActionCollection2> _inputActions = new();

		public string CurrentControlScheme { get; private set; }

		public void RegisterInputActions<T>(T inputActions)
			where T : IInputActionCollection2
		{
			RegisterInputActions(inputActions, typeof(T).Name);
		}

		public void RegisterInputActions<T>(T inputActions, string key)
			where T : IInputActionCollection2
		{
			_inputActions[key] = inputActions;
		}

		public void Enable<T>()
			where T : IInputActionCollection2
		{
			Enable(typeof(T).Name);
		}

		public void Enable(string key)
		{
			if (_inputActions.TryGetValue(key, out var inputActions))
			{
				inputActions.Enable();
			}
		}

		public void Disable<T>()
			where T : IInputActionCollection2
		{
			Disable(typeof(T).Name);
		}

		public void Disable(string key)
		{
			if (_inputActions.TryGetValue(key, out var inputActions))
			{
				inputActions.Disable();
			}
		}

		internal void Init()
		{
			InputSystem.onActionChange -= OnActionChange;
			InputSystem.onActionChange += OnActionChange;
		}

		private void OnActionChange(object arg, InputActionChange phase)
		{
			if (phase != InputActionChange.ActionPerformed)
			{
				return;
			}

			if (arg is not InputAction inputAction)
			{
				return;
			}

			if (inputAction.GetBindingForControl(inputAction.activeControl) is { } binding)
			{
				CurrentControlScheme = binding.groups;
			}
		}
	}
}