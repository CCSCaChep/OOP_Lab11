/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using CampusSafetyHub.Models;

namespace CampusSafetyHub.Observer
{
    /// <summary>
    /// Giao diện cho các bên đăng ký nhận thông báo cảnh báo từ SensorMonitor.
    /// </summary>
    public interface IAlertObserver
    {
        /// <summary>
        /// Xử lý sự kiện cảnh báo mới nhận được.
        /// </summary>
        void Update(AlertEvent alertEvent);
    }
}