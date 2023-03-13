namespace GraphReview.Application.Constants
{
    public static class EmailConstants
    {
        public const string EmailSubject = "Performance Review Meeting";
        public const string EmailBody = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\t<title>Performance Review Meeting</title>\r\n</head>\r\n<body>\r\n\t<h1>Performance Review Meeting</h1>\r\n\t<p>Dear {0},</p>\r\n\t<p>I would like to schedule a performance review meeting with you to discuss your progress and goals. The meeting will take place on {1} at {2}.</p>\r\n\t<p>The purpose of the meeting is to review your performance over the past [time period] and discuss your future goals and objectives. We will also discuss any areas where you may need additional support or training.</p>\r\n\t<p>Please come prepared with any questions or concerns that you may have, and be ready to discuss your accomplishments and challenges over the past <b>six months</b>.</p>\r\n\t<p>If you are unable to attend the meeting at the scheduled time, please let me know and we will arrange for an alternative time.</p>\r\n\t<p>Thank you, and I look forward to meeting with you.</p>\r\n\t<p>Best regards,</p>\r\n\t<p>Your Manager</p>\r\n</body>\r\n</html>";
    }
}
