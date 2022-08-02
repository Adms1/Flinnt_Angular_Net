using Flinnt.Business.Enums.General;

namespace Flinnt.Business.ViewModels.General
{
    public class DropMessageModel
    {
        public string Message { get; set; }

        public string Description { get; set; }

        public DropMessageType DropMessageType { get; set; }
    }
}