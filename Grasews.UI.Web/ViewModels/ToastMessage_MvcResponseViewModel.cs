using System;
using System.Collections.Generic;

namespace Grasews.Web.ViewModels
{
    [Serializable]
    public class ToastMessage_MvcResponseViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ToastTypeEnum ToastType { get; set; }
        public bool IsSticky { get; set; }
    }

    public enum ToastTypeEnum
    {
        Danger,
        Info,
        Success,
        Warning
    }

    [Serializable]
    public class Toastr
    {
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public List<ToastMessage_MvcResponseViewModel> ToastMessages { get; set; }
        public int Timeout { get; set; }

        public ToastMessage_MvcResponseViewModel AddToastMessage(string message, ToastTypeEnum toastType, int timeout)
        {
            Timeout = timeout;
            var toast = new ToastMessage_MvcResponseViewModel
            {
                Message = message,
                ToastType = toastType
            };
            ToastMessages.Add(toast);
            return toast;
        }

        public Toastr()
        {
            ToastMessages = new List<ToastMessage_MvcResponseViewModel>();
            ShowNewestOnTop = false;
            ShowCloseButton = false;
        }
    }
}