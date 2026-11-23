/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;

namespace CampusSafetyHub.Mediator
{
    /// <summary>
    /// Thiết bị còi báo động khẩn cấp.
    /// </summary>
    public sealed class Alarm
    {
        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
            Console.WriteLine("[ALARM] Siren ACTIVATED!");
        }

        public void Deactivate()
        {
            IsActive = false;
            Console.WriteLine("[ALARM] Siren deactivated.");
        }
    }
}