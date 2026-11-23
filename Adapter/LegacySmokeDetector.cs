/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;

namespace CampusSafetyHub.Adapter
{
    /// <summary>
    /// Lớp cảm biến khói cũ có sẵn (giả định không được phép sửa đổi).
    /// </summary>
    public sealed class LegacySmokeDetector
    {
        private int smokeLevel;

        public LegacySmokeDetector(int smokeLevel)
        {
            this.smokeLevel = Math.Clamp(smokeLevel, 0, 1000);
        }

        /// <summary>
        /// Lấy mức khói thô trong khoảng 0..1000.
        /// </summary>
        public int GetSmokeLevel() => smokeLevel;

        public string GetDescription() => "Legacy smoke detector";

        /// <summary>
        /// Cập nhật mức khói thô (giới hạn trong 0..1000).
        /// </summary>
        public void SetSmokeLevel(int value)
        {
            smokeLevel = Math.Clamp(value, 0, 1000);
        }
    }
}