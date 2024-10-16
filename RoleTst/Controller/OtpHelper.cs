namespace RoleTst.Controller;

public static class OTPHelper
{
    public static async Task GenerateOTP()
    {
        var code = new Random().Next(1000, 9999);
        var dict = new Dictionary<string, string>();

        dict.Add("receptor", "09350660038");
        dict.Add("sender", "100001669");
        dict.Add("message", $"خدمات پیام کوتاه کاوه نگار{code}$");

        var client = new HttpClient();
        var req = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.kavenegar.com/v1/317A4D70476370746A7739625867644A5153396F692B5654683442534C39676236396C58486B5949576B4D3D/sms/send.json")
            { Content = new FormUrlEncodedContent(dict) };
        var res = await client.SendAsync(req);
        Console.WriteLine(res);
    }

    public static bool IsOTPValid(string otp, string userOtp, DateTime otpIssueTime)
    {
        // OTP valid for 5 minutes
        return otp == userOtp && DateTime.Now.Subtract(otpIssueTime).TotalMinutes <= 5;
    }
}