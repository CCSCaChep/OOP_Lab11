/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using CampusSafetyHub.Models;

namespace CampusSafetyHub.Observer
{
    /// <summary>
    /// Observer ghi nhận lịch sử cảnh báo phục vụ kiểm toán (Audit Log).
    /// </summary>
    public sealed class AuditLogObserver : IAlertObserver
    {
        public void Update(AlertEvent alertEvent)
        {
            Console.WriteLine(
                $"[AUDIT LOG] {alertEvent.OccurredAt:yyyy-MM-dd HH:mm:ss} | {alertEvent.SensorName} | {alertEvent.Value:F1}{alertEvent.Unit} | {alertEvent.Severity}");
        }
    }
}