/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using System.Collections.Generic;
using CampusSafetyHub.Models;

namespace CampusSafetyHub.Observer
{
    /// <summary>
    /// Subject quản lý các observer, đọc cảm biến, đánh giá ngưỡng và phát cảnh báo.
    /// </summary>
    public sealed class SensorMonitor
    {
        private readonly List<IAlertObserver> observers = new();

        /// <summary>
        /// Đăng ký observer mới (không cho phép đăng ký trùng lặp).
        /// </summary>
        public void Attach(IAlertObserver observer)
        {
            ArgumentNullException.ThrowIfNull(observer);
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        /// <summary>
        /// Hủy đăng ký một observer.
        /// </summary>
        public void Detach(IAlertObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// Đọc giá trị cảm biến, xác định mức độ cảnh báo và thông báo cho các observer.
        /// </summary>
        public void CheckSensor(string sensorName, ISensor sensor)
        {
            ArgumentNullException.ThrowIfNull(sensor);

            double value = sensor.ReadValue();
            string? severity = value switch
            {
                >= 70 => "CRITICAL",
                >= 50 => "WARNING",
                _ => null
            };

            if (severity is null)
            {
                Console.WriteLine($"[MONITOR] {sensorName}: {value:F1}{sensor.GetUnit()} [NORMAL - No alert]");
                return;
            }

            var alertEvent = new AlertEvent(
                sensorName,
                value,
                sensor.GetUnit(),
                severity,
                DateTime.Now);

            Notify(alertEvent);
        }

        /// <summary>
        /// Gửi cảnh báo trên bản sao danh sách observer và cô lập lỗi từng observer.
        /// </summary>
        private void Notify(AlertEvent alertEvent)
        {
            foreach (IAlertObserver observer in observers.ToArray())
            {
                try
                {
                    observer.Update(alertEvent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MONITOR ERROR] Observer {observer.GetType().Name} failed: {ex.Message}");
                }
            }
        }
    }
}