using Microsoft.JSInterop;
using System.Drawing;

namespace SIM.Services
{
    public interface ISweeAlert
    {
        void Message(IJSRuntime JS, string mensaje, eIcon icon, string title = "", bool allowOutsideClick = false, eAlign align = eAlign.center, bool backdrop = false, string confirmButtonText = "Aceptar", bool ir = false, string url = "");
        public Task<bool> Confirm(IJSRuntime JS, string mensaje, eIcon icon = eIcon.question, string confirmButtonText = "Confirmar", string cancelButtonText = "Cancelar", bool ShowCancelButton = true, string cancelButtonColor = "#1bd5ff", string iconColor = "#C22F00", bool reverseButtons = true, bool backdrop = false);
    }
    public class SweetAlert : ISweeAlert
    {
        public void Message(IJSRuntime JS, string mensaje, eIcon icon, string title = "", bool allowOutsideClick = false, eAlign align = eAlign.center, bool backdrop = false, string confirmButtonText = "Aceptar", bool ir = false, string url = "")
        {
            string confirmButtonColor = "";
            string backColor = "";
            switch (icon)
            {
                case eIcon.success:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.success;
                    break;
                case eIcon.error:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.error;
                    break;
                case eIcon.warning:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.warning;
                    break;
                case eIcon.info:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.info;
                    break;
                case eIcon.question:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.question;
                    break;
                default:
                    break;
            }
            _ = JS.InvokeVoidAsync("Message", mensaje, icon.ToString().ToLower(), title, allowOutsideClick, align.ToString().ToLower(), backdrop, backColor, confirmButtonText, confirmButtonColor, ir, url);
        }
        public async Task<bool> Confirm(IJSRuntime JS,string mensaje,eIcon icon=eIcon.question, string confirmButtonText="Confirmar",string cancelButtonText = "Cancelar",bool ShowCancelButton = true, string cancelButtonColor = "#1bd5ff",string iconColor = "#C22F00",bool reverseButtons = false,bool backdrop = false)
        {
            string confirmButtonColor = "";
            string backColor = "";
            switch (icon)
            {
                case eIcon.success:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.success;
                    iconColor = eColors.success;
                    break;
                case eIcon.error:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.error;
                    iconColor = eColors.error;
                    break;
                case eIcon.warning:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.warning;
                    iconColor = eColors.warning;
                    break;
                case eIcon.info:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.info;
                    iconColor = eColors.info;
                    break;
                case eIcon.question:
                    backColor = eColors.backColor;
                    confirmButtonColor = eColors.question;
                    iconColor = eColors.question;
                    break;
                default:
                    break;
            }
            return await JS.InvokeAsync<bool>("Confirm", mensaje,icon.ToString(),confirmButtonText,cancelButtonText,ShowCancelButton,confirmButtonColor,cancelButtonColor,iconColor,reverseButtons,backdrop,backColor);
        }
    }
    public enum eIcon
    {
        success,
        error,
        warning,
        info,
        question
    }
    public enum eAlign
    {
        left,
        center,
        right,
        justify
    }
    public static class eColors
    {
        public static string info { get; set; } = "#43b7ff";
        public static string success { get; set; } = "#88cc00";
        public static string warning { get; set; } = "#f8bb86";
        public static string error { get; set; } = "#f27474";
        public static string question { get; set; } = "#87adbd";
        public static string backColor { get; set; } = "rgba(224,225,226,1)";
    }
}
