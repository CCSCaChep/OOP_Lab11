/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

namespace CampusSafetyHub.Models
{
    /// <summary>
    /// Giao diện chuẩn cho các cảm biến trong hệ thống Campus Safety Hub.
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// Đọc giá trị hiện tại của cảm biến theo thang đo chuẩn.
        /// </summary>
        double ReadValue();

        /// <summary>
        /// Trả về đơn vị đo của cảm biến.
        /// </summary>
        string GetUnit();
    }
}