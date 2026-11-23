/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;

namespace CampusSafetyHub.Mediator
{
    /// <summary>
    /// Bộ điều khiển hệ thống thông gió/điều hòa phòng máy.
    /// </summary>
    public sealed class VentilationController
    {
        public bool IsShutdown { get; private set; }

        public void Shutdown()
        {
            IsShutdown = true;
            Console.WriteLine("[VENTILATION] HVAC system SHUT DOWN to prevent smoke spread.");
        }

        public void Restart()
        {
            IsShutdown = false;
            Console.WriteLine("[VENTILATION] HVAC system restarted.");
        }
    }
}