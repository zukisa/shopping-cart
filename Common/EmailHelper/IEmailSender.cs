using Common.EmailHelper.Classes;

namespace Common.EmailHelper
{
    public interface IEmailSender
    {
        bool SendEmail(Message message);
    }
}
