/****************/
// 202418929
// Hoàng Chí Kiệt
/****************/

using System;
using CampusSafetyHub.Adapter;
using CampusSafetyHub.Mediator;
using CampusSafetyHub.Models;
using CampusSafetyHub.Observer;

namespace CampusSafetyHub
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=== CAMPUS SAFETY HUB - DEMO ===");

            // 1. Khởi tạo cảm biến cũ và bọc bằng Adapter
            var legacyDetector = new LegacySmokeDetector(350);
            ISensor smokeSensor = new SmokeSensorAdapter(legacyDetector);

            // 2. Khởi tạo các thiết bị ứng phó khẩn cấp và Mediator
            var alarm = new Alarm();
            var doorController = new DoorController();
            var ventilationController = new VentilationController();
            IEmergencyMediator mediator = new EmergencyMediator(alarm, doorController, ventilationController);

            // 3. Khởi tạo Monitor và đăng ký các Observer
            var monitor = new SensorMonitor();
            var dashboard = new DashboardObserver();
            var email = new EmailAlertObserver();
            var auditLog = new AuditLogObserver();
            var emergencyBridge = new EmergencyBridgeObserver(mediator);

            monitor.Attach(dashboard);
            monitor.Attach(email);
            monitor.Attach(auditLog);
            monitor.Attach(emergencyBridge);

            // Kịch bản 1: Bình thường (< 50%)
            Console.WriteLine("\n--- Case 1: Normal (35.0%) ---");
            monitor.CheckSensor("Server Room A", smokeSensor);

            // Kịch bản 2: Cảnh báo WARNING (50% - <70%)
            Console.WriteLine("\n--- Case 2: Warning (58.0%) ---");
            legacyDetector.SetSmokeLevel(580);
            monitor.CheckSensor("Server Room A", smokeSensor);

            // Kịch bản 3: Cảnh báo CRITICAL (>= 70%) -> kích hoạt Mediator
            Console.WriteLine("\n--- Case 3: Critical (73.5%) ---");
            legacyDetector.SetSmokeLevel(735);
            monitor.CheckSensor("Server Room A", smokeSensor);

            // Hủy đăng ký EmailObserver và kiểm tra lại
            Console.WriteLine("\n--- Case 4: Detach EmailAlertObserver & Check (82.0%) ---");
            monitor.Detach(email);
            legacyDetector.SetSmokeLevel(820);
            monitor.CheckSensor("Server Room A", smokeSensor);

            Console.WriteLine("\n=== RUNNING AUTOMATED TESTS (T1 - T5) ===");
            RunTests();
        }

        private static void RunTests()
        {
            // T1: Adapter chuyển 735 -> 73.5 và đơn vị "% smoke"
            var legacy = new LegacySmokeDetector(735);
            ISensor adapter = new SmokeSensorAdapter(legacy);
            bool t1 = Math.Abs(adapter.ReadValue() - 73.5) < 0.001 && adapter.GetUnit() == "% smoke";
            PrintResult("T1", "Adapter chuyen doi 735 -> 73.5% smoke", t1);

            // T2: Dưới 50% không phát cảnh báo
            var countObserver = new CountingObserver();
            var monitor = new SensorMonitor();
            monitor.Attach(countObserver);
            monitor.Attach(countObserver); // Kiểm tra chống đăng ký trùng
            legacy.SetSmokeLevel(490);
            monitor.CheckSensor("Lab 1", adapter);
            bool t2 = countObserver.CallCount == 0;
            PrintResult("T2", "Duoi 50% khong phat canh bao", t2);

            // T3: Từ 50% đến <70% phát WARNING đúng 1 lần (không trùng observer)
            legacy.SetSmokeLevel(600);
            monitor.CheckSensor("Lab 1", adapter);
            bool t3 = countObserver.CallCount == 1 && countObserver.LastSeverity == "WARNING";
            PrintResult("T3", "60% phat WARNING dung 1 lan (chong trung observer)", t3);

            // T4: Một observer lỗi không làm gián đoạn observer khác
            var faultyObserver = new FaultyObserver();
            monitor.Attach(faultyObserver);
            legacy.SetSmokeLevel(650);
            monitor.CheckSensor("Lab 1", adapter);
            bool t4 = countObserver.CallCount == 2;
            PrintResult("T4", "Observer loi duoc co lap, khong anh huong observer khac", t4);

            // T5: >= 70% kích hoạt EmergencyMediator (Alarm, Door, Ventilation)
            var alarm = new Alarm();
            var door = new DoorController();
            var vent = new VentilationController();
            var mediator = new EmergencyMediator(alarm, door, vent);
            var bridge = new EmergencyBridgeObserver(mediator);

            var m2 = new SensorMonitor();
            m2.Attach(bridge);
            legacy.SetSmokeLevel(750);
            m2.CheckSensor("Lab 1", adapter);

            bool t5 = alarm.IsActive && door.AreExitDoorsOpen && vent.IsShutdown;
            PrintResult("T5", "CRITICAL (>=70%) kich hoat Alarm, DoorController va VentilationController", t5);
        }

        private static void PrintResult(string id, string desc, bool pass)
        {
            Console.WriteLine($"[{id}] {(pass ? "PASS" : "FAIL")} - {desc}");
        }

        // Observer phụ trợ cho kiểm thử
        private sealed class CountingObserver : IAlertObserver
        {
            public int CallCount { get; private set; }
            public string? LastSeverity { get; private set; }

            public void Update(AlertEvent alertEvent)
            {
                CallCount++;
                LastSeverity = alertEvent.Severity;
            }
        }

        private sealed class FaultyObserver : IAlertObserver
        {
            public void Update(AlertEvent alertEvent)
            {
                throw new InvalidOperationException("Simulated observer failure");
            }
        }
    }
}