/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using CampusSafetyHub.Models;

namespace CampusSafetyHub.Adapter
{
    /// <summary>
    /// Adapter chuyển đổi LegacySmokeDetector sang giao diện ISensor chuẩn.
    /// </summary>
    public sealed class SmokeSensorAdapter : ISensor
    {
        private readonly LegacySmokeDetector legacy;

        /// <summary>
        /// Khởi tạo adapter bọc lấy thể hiện LegacySmokeDetector.
        /// </summary>
        public SmokeSensorAdapter(LegacySmokeDetector legacy)
        {
            this.legacy = legacy ?? throw new ArgumentNullException(nameof(legacy));
        }

        /// <summary>
        /// Chuyển đổi giá trị 0..1000 sang phần trăm 0..100.
        /// </summary>
        public double ReadValue()
        {
            return legacy.GetSmokeLevel() / 10.0;
        }

        public string GetUnit() => "% smoke";
    }
}