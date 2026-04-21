using System;
using System.Collections.Generic;

namespace SmartAttendanceCore
{
    // 定義承攬商 (Contractor) 的資料模型
    public class Contractor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsAuthorized { get; set; }
        public DateTime LastAccessTime { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Smart Attendance Hub: 承攬商驗證模組測試 ===\n");

            // 模擬從資料庫載入的承攬商名單
            List<Contractor> database = new List<Contractor>
            {
                new Contractor { Id = "C001", Name = "Alpha 設備維護", IsAuthorized = true },
                new Contractor { Id = "C002", Name = "Beta 廠務機電", IsAuthorized = false }
            };

            // 模擬 YOLO 影像辨識模型傳來的 ID 結果
            string[] detectedIds = { "C001", "C003", "C002" };

            // 針對每一個偵測到的 ID 進行出勤與授權驗證
            foreach (var id in detectedIds)
            {
                Console.WriteLine($"[System Event] 影像辨識偵測到 ID: {id}");
                ValidateAccess(id, database);
                Console.WriteLine("--------------------------------------------------");
            }
        }

        // 驗證邏輯函數
        static void ValidateAccess(string detectedId, List<Contractor> db)
        {
            // 在資料庫中尋找是否有匹配的承攬商
            Contractor matchedContractor = db.Find(c => c.Id == detectedId);

            if (matchedContractor != null)
            {
                if (matchedContractor.IsAuthorized)
                {
                    matchedContractor.LastAccessTime = DateTime.Now;
                    Console.WriteLine($"[Success] 授權通過！允許 {matchedContractor.Name} 進入。打卡時間: {matchedContractor.LastAccessTime}");
                }
                else
                {
                    Console.WriteLine($"[Warning] 拒絕進入！{matchedContractor.Name} 缺乏有效授權或授權已過期。");
                }
            }
            else
            {
                Console.WriteLine($"[Alert] 警報：系統無此 ID ({detectedId}) 的承攬商資料，請聯絡工安中心。");
            }
        }
    }
}
