/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;

namespace CampusSafetyHub.Models
{
    /// <summary>
    /// Đối tượng dữ liệu bất biến mô tả một sự kiện cảnh báo.
    /// </summary>
    public sealed record AlertEvent(
        string SensorName,
        double Value,
        string Unit,
        string Severity,
        DateTime OccurredAt);
}