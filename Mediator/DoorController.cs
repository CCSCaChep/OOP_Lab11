/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;

namespace CampusSafetyHub.Mediator
{
    /// <summary>
    /// Bộ điều khiển cửa thoát hiểm phòng máy.
    /// </summary>
    public sealed class DoorController
    {
        public bool AreExitDoorsOpen { get; private set; }

        public void OpenExitDoors()
        {
            AreExitDoorsOpen = true;
            Console.WriteLine("[DOOR CONTROLLER] All exit doors UNLOCKED & OPENED.");
        }

        public void LockDoors()
        {
            AreExitDoorsOpen = false;
            Console.WriteLine("[DOOR CONTROLLER] Doors locked.");
        }
    }
}