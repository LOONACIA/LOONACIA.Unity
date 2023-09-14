using System;
using UnityEngine;

public class TransformObjectCommand : DebugCommandBase
{
	public enum TransformType
	{
		Scale,
		Rotation,
		Position
	}

	private TransformType _type;

	public TransformObjectCommand(string id, string description, string format, TransformType type) : base(id, description, format)
	{
		_type = type;
	}

	public override void Execute(object parameter = null)
	{
		if (parameter is not string paramString)
		{
			return;
		}

		var span = paramString.AsSpan();
		int index = span.IndexOf(' ');
		if (index == -1)
		{
			return;
		}

		if (!ArgumentParserBag.TryGameObject(span.Slice(0, index), out var go))
		{
			return;
		}

		if (!ArgumentParserBag.TryGetVector3(span.Slice(index + 1), out Vector3 vector))
		{
			return;
		}

		switch (_type)
		{
			case TransformType.Scale:
				go.transform.localScale = vector;
				break;
			case TransformType.Rotation:
				go.transform.rotation = Quaternion.Euler(vector);
				break;
			case TransformType.Position:
				go.transform.position = vector;
				break;
		}
	}
}