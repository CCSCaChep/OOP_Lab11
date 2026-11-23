/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using CampusSafetyHub.Models;

namespace CampusSafetyHub.Observer
{
    /// <summary>
    /// Observer mô phỏng gửi email cảnh báo cho quản trị viên phòng máy.
    /// </summary>
    public sealed class EmailAlertObserver : IAlertObserver
    {
        public void Update(AlertEvent alertEvent)
        {
            Console.WriteLine(
                $"[EMAIL] To: admin@campus.edu | Subject: [{alertEvent.Severity}] {alertEvent.SensorName} ({alertEvent.Value:F1}{alertEvent.Unit})");
        }
    }
}