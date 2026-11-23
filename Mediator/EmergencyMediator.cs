/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using CampusSafetyHub.Models;

namespace CampusSafetyHub.Mediator
{
    /// <summary>
    /// Mediator điều phối Alarm, DoorController và VentilationController khi có sự cố cháy.
    /// </summary>
    public sealed class EmergencyMediator : IEmergencyMediator
    {
        private readonly Alarm alarm;
        private readonly DoorController doorController;
        private readonly VentilationController ventilationController;

        public EmergencyMediator(
            Alarm alarm,
            DoorController doorController,
            VentilationController ventilationController)
        {
            this.alarm = alarm ?? throw new ArgumentNullException(nameof(alarm));
            this.doorController = doorController ?? throw new ArgumentNullException(nameof(doorController));
            this.ventilationController = ventilationController ?? throw new ArgumentNullException(nameof(ventilationController));
        }

        public void Notify(string eventCode, AlertEvent alertEvent)
        {
            if (eventCode == "FIRE_DETECTED")
            {
                Console.WriteLine($"[MEDIATOR] Handling {eventCode} from {alertEvent.SensorName} ({alertEvent.Value:F1}{alertEvent.Unit})...");
                alarm.Activate();
                doorController.OpenExitDoors();
                ventilationController.Shutdown();
            }
        }
    }
}