using System;
using System.Runtime.InteropServices;
using AltV.Net.Data;
using AltV.Net.Elements.Args;
using AltV.Net.Elements.Entities;

namespace AltV.Net.Native
{
    internal static partial class AltNative
    {
        internal static class Player
        {
            // Entity

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ushort Player_GetID(IntPtr entityPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern uint Player_GetModel(IntPtr entityPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetModel(IntPtr entityPointer, uint model);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetPosition(IntPtr entityPointer, ref Position position);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetPosition(IntPtr entityPointer, Position position);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetRotation(IntPtr entityPointer, ref Rotation rotation);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetRotation(IntPtr entityPointer, Rotation rotation);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern short Player_GetDimension(IntPtr entityPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetDimension(IntPtr entityPointer, short dimension);

            [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetMetaData(IntPtr entityPointer, string key, ref MValue value);

            [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetMetaData(IntPtr entityPointer, string key, ref MValue value);

            [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = NativeCallingConvention)]
            internal static extern void
                Player_GetSyncedMetaData(IntPtr entityPointer, string key, ref MValue value);

            [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = NativeCallingConvention)]
            internal static extern void
                Player_SetSyncedMetaData(IntPtr entityPointer, string key, ref MValue value);

            // Player

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsConnected(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_Spawn(IntPtr playerPointer, Position pos, uint delayMs);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_Despawn(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetName(IntPtr playerPointer, ref IntPtr name);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetName(IntPtr playerPointer, IntPtr name);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ulong Player_GetSocialID(IntPtr playerPointer);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ulong Player_GetHwidHash(IntPtr playerPointer);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ulong Player_GetHwidExHash(IntPtr playerPointer);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetAuthToken(IntPtr playerPointer, IntPtr name);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ushort Player_GetHealth(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetHealth(IntPtr playerPointer, ushort health);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ushort Player_GetMaxHealth(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetMaxHealth(IntPtr playerPointer, ushort maxHealth);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetDateTime(IntPtr playerPointer, int day, int month, int year, int hour,
                int minute, int second);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetWeather(IntPtr playerPointer, uint weather);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GiveWeapon(IntPtr playerPointer, uint weapon, int ammo,
                bool selectWeapon);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_RemoveWeapon(IntPtr playerPointer, uint weapon);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_RemoveAllWeapons(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_AddWeaponComponent(IntPtr playerPointer, uint weapon, uint component);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_RemoveWeaponComponent(IntPtr playerPointer, uint weapon, uint component);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetCurrentWeaponComponents(IntPtr playerPointer,
                ref UIntArray weaponComponents);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetWeaponTintIndex(IntPtr playerPointer, uint weapon, byte tintIndex);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern byte Player_GetCurrentWeaponTintIndex(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern uint Player_GetCurrentWeapon(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetCurrentWeapon(IntPtr playerPointer, uint weapon);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsDead(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsJumping(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsInRagdoll(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsAiming(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsShooting(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsReloading(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ushort Player_GetArmor(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetArmor(IntPtr playerPointer, ushort armor);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ushort Player_GetMaxArmor(IntPtr playerPointer);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_SetMaxArmor(IntPtr playerPointer, ushort armor);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern float Player_GetMoveSpeed(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern uint Player_GetWeapon(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern ushort Player_GetAmmo(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetAimPos(IntPtr playerPointer, ref Position aimPosition);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetHeadRotation(IntPtr playerPointer, ref Rotation headRotation);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsInVehicle(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern IntPtr Player_GetVehicle(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern byte Player_GetSeat(IntPtr playerPointer);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern IntPtr Player_GetEntityAimingAt(IntPtr playerPointer, ref BaseObjectType type);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetEntityAimOffset(IntPtr playerPointer, ref Position position);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern bool Player_IsFlashlightActive(IntPtr playerPointer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_Kick(IntPtr playerPointer, IntPtr reason);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern uint Player_GetPing(IntPtr playerPointer);
            
            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_GetIP(IntPtr playerPointer, ref IntPtr ip);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_Copy(IntPtr playerPointer, ref ReadOnlyPlayer readOnlyPlayer);

            [DllImport(DllName, CallingConvention = NativeCallingConvention)]
            internal static extern void Player_Copy_Dispose(ref ReadOnlyPlayer readOnlyPlayer);
        }
    }
}