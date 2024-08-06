////2.Bài toán Lựa chọn hoạt động - Activity-Selection Problem


//using System;
//using System.Linq;

//class Bai02
//{
//    // Lớp để đại diện cho một hoạt động
//    public class Activity
//    {
//        public int Start { get; set; }
//        public int End { get; set; }

//        public Activity(int start, int end)
//        {
//            Start = start;
//            End = end;
//        }
//    }

//    static void Main()
//    {
//        // Khởi tạo mảng thời gian bắt đầu và kết thúc của các hoạt động
//        int[] startTimes = { 1, 3, 0, 5, 8, 5, 8, 7 };
//        int[] endTimes = { 4, 5, 6, 7, 9, 9, 10, 9 };

//        // Số lượng hoạt động
//        int n = startTimes.Length;

//        // Tạo mảng hoạt động từ mảng thời gian bắt đầu và kết thúc
//        var activities = new Activity[n];
//        for (int i = 0; i < n; i++)
//        {
//            activities[i] = new Activity(startTimes[i], endTimes[i]);
//        }

//        // Sắp xếp các hoạt động theo thời gian kết thúc
//        var sortedActivities = activities.OrderBy(a => a.End).ToArray();

//        // Tạo tập hợp để lưu trữ các hoạt động đã chọn
//        var selectedActivities = new System.Collections.Generic.List<Activity>();

//        // Biến để theo dõi thời gian kết thúc của hoạt động đã chọn gần đây nhất
//        int lastEndTime = -1;

//        // Lặp qua các hoạt động đã sắp xếp
//        foreach (var activity in sortedActivities)
//        {
//            // Nếu thời gian bắt đầu của hoạt động hiện tại >= thời gian kết thúc của hoạt động đã chọn gần đây nhất
//            if (activity.Start >= lastEndTime)
//            {
//                // Thêm hoạt động hiện tại vào tập hợp các hoạt động đã chọn
//                selectedActivities.Add(activity);
//                // Cập nhật thời gian kết thúc của hoạt động được chọn gần đây nhất
//                lastEndTime = activity.End;
//            }
//        }

//        // In kết quả
//        Console.WriteLine("Cac hoat dong duoc chon:");
//        foreach (var activity in selectedActivities)
//        {
//            Console.WriteLine($"({activity.Start}, {activity.End})");
//        }

//        Console.ReadKey();
//    }
//}