/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using CampusSafetyHub.Mediator;
using CampusSafetyHub.Models;

namespace CampusSafetyHub.Observer
{
    /// <summary>
    /// Observer cầu nối chuyển cảnh báo mức CRITICAL thành sự kiện cho EmergencyMediator.
    /// </summary>
    public sealed class EmergencyBridgeObserver : IAlertObserver
    {
        private readonly IEmergencyMediator mediator;

        public EmergencyBridgeObserver(IEmergencyMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void Update(AlertEvent alertEvent)
        {
            if (alertEvent.Severity == "CRITICAL")
            {
                mediator.Notify("FIRE_DETECTED", alertEvent);
            }
        }
    }
}