/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using CampusSafetyHub.Models;

namespace CampusSafetyHub.Mediator
{
    /// <summary>
    /// Hợp đồng điều phối các thiết bị ứng phó khẩn cấp.
    /// </summary>
    public interface IEmergencyMediator
    {
        /// <summary>
        /// Tiếp nhận sự kiện khẩn cấp và điều phối các thiết bị liên quan.
        /// </summary>
        void Notify(string eventCode, AlertEvent alertEvent);
    }
}