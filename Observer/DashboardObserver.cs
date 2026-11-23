/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using CampusSafetyHub.Models;

namespace CampusSafetyHub.Observer
{
    /// <summary>
    /// Observer hiển thị cảnh báo lên màn hình giám sát (Dashboard).
    /// </summary>
    public sealed class DashboardObserver : IAlertObserver
    {
        public void Update(AlertEvent alertEvent)
        {
            Console.WriteLine(
                $"[DASHBOARD] {alertEvent.SensorName}: {alertEvent.Value:F1}{alertEvent.Unit} [{alertEvent.Severity}]");
        }
    }
}