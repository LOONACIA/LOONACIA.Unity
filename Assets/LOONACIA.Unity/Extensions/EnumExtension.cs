using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

// ReSharper disable once CheckNamespace
namespace System
{
	public static class EnumExtension
	{
		public static bool Has<T>(this T @enum, T flag)
			where T : unmanaged, Enum
		{
			return UnsafeUtility.SizeOf<T>() switch
			{
				1 => (UnsafeUtility.As<T, byte>(ref @enum) & UnsafeUtility.As<T, byte>(ref flag)) != 0,
				2 => (UnsafeUtility.As<T, short>(ref @enum) & UnsafeUtility.As<T, short>(ref flag)) != 0,
				4 => (UnsafeUtility.As<T, uint>(ref @enum) & UnsafeUtility.As<T, uint>(ref flag)) != 0,
				8 => (UnsafeUtility.As<T, ulong>(ref @enum) & UnsafeUtility.As<T, ulong>(ref flag)) != 0,
				_ => throw new NotSupportedException("Enum size does not match known underlying type.")
			};
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set<T>(this ref T @enum, T flag, bool value)
			where T : unmanaged, Enum
		{
			if (value)
			{
				TurnOn(ref @enum, flag);
			}
			else
			{
				TurnOff(ref @enum, flag);
			}
		}
    
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TurnOn<T>(ref T @enum, T flag)
			where T : unmanaged, Enum
		{
			switch (UnsafeUtility.SizeOf<T>())
			{
				case 1:
					UnsafeUtility.As<T, byte>(ref @enum) |= UnsafeUtility.As<T, byte>(ref flag);
					break;
				case 2:
					UnsafeUtility.As<T, short>(ref @enum) |= UnsafeUtility.As<T, short>(ref flag);
					break;
				case 4:
					UnsafeUtility.As<T, uint>(ref @enum) |= UnsafeUtility.As<T, uint>(ref flag);
					break;
				case 8:
					UnsafeUtility.As<T, ulong>(ref @enum) |= UnsafeUtility.As<T, ulong>(ref flag);
					break;
				default:
					throw new NotSupportedException("Enum size does not match known underlying type.");
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TurnOff<T>(ref T @enum, T flag)
			where T : unmanaged, Enum
		{
			switch (UnsafeUtility.SizeOf<T>())
			{
				case 1:
					UnsafeUtility.As<T, byte>(ref @enum) &= (byte)~UnsafeUtility.As<T, byte>(ref flag);
					break;
				case 2:
					UnsafeUtility.As<T, short>(ref @enum) &= (short)~UnsafeUtility.As<T, short>(ref flag);
					break;
				case 4:
					UnsafeUtility.As<T, uint>(ref @enum) &= ~UnsafeUtility.As<T, uint>(ref flag);
					break;
				case 8:
					UnsafeUtility.As<T, ulong>(ref @enum) &= ~UnsafeUtility.As<T, ulong>(ref flag);
					break;
				default:
					throw new NotSupportedException("Enum size does not match known underlying type.");
			}
		}
	}
}