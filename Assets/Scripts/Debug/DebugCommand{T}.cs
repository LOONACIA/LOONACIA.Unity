using System;
using UnityEngine;

public delegate bool DebugCommandParser<T>(ReadOnlySpan<char> chars, out T argument);

public class DebugCommand<T> : DebugCommandBase
{
	private readonly Action<T> _execute;

	private readonly DebugCommandParser<T> _parser;

	public DebugCommand(string id, string description, string format, Action<T> execute, DebugCommandParser<T> parser = null) : base(id, description, format)
	{
		_execute = execute;
		_parser = parser;
	}

	public void Execute(T parameter)
	{
		_execute?.Invoke(parameter);
	}

	public void Execute(ReadOnlySpan<char> parameter)
	{
		if (_parser == null)
		{
			Debug.LogError($"Argument parser is null.");
			return;
		}

		if (_parser(parameter, out var argument))
		{
			_execute?.Invoke(argument);
		}
	}

	public override void Execute(object parameter = null)
	{
		if (!TryGetCommandArgument(parameter, out T argument))
		{
			Debug.LogError($"Invalid argument: {parameter}, argument type is {typeof(T)}.");
		}

		Execute(argument);
	}

	private static bool TryGetCommandArgument(object parameter, out T argument)
	{
		if (parameter is null && default(T) is null)
		{
			argument = default;

			return true;
		}

		if (parameter is T result)
		{
			argument = result;

			return true;
		}

		argument = default;

		return false;
	}
}