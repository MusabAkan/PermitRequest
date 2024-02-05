namespace PermitRequest.Application.Constants
{
    public static class Message
    {
        
        public static string NotFoundUser = "Kullanıcı bulunamadı..";
        public static string DateError = "Başlangıç tarih bitiş tarihden büyük yada eşit olmamalıdır..";
        public static string GuidTypId = "Id tipi Guid olmalıdır...";
        public static string AllCompleted = "Tüm işlemler tamamlanmıştır.";
        public static string PermissionPeriod = "{0} izin süresi aşıldı.";
        public static string NoData = "Veri yok!!";
        public static string LeavePeriodWasTaken = "{0} izin süresi %10 fazla alındı.";
    }
}
