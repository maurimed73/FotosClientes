using FotosClientes.Views;
using Microsoft.Maui.Platform;

namespace FotosClientes
{
    public partial class App : Application
    {
        public App()
        {
            CustomHandler();
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage());
        }

        private void CustomHandler()
        {
            EntryNoBorder();
            DatePickerNoBorder();


        }

        private static void EntryNoBorder()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoBorder", (handler, view) => {


#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

#endif


                //#if iOS || MACCATALYST
                //                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                //#endif

                //#if WINDOWS  //-  não funciona 100%
                //                handler.PlatformView.BorderThickness = new Thickness(0).ToPlatform();
                //#endif

            });
        }

        private static void DatePickerNoBorder()
        {
            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("NoBorder", (handler, view) => {


#if ANDROID
                                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

#endif


                //#if iOS || MACCATALYST
                //                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                //#endif

                //#if WINDOWS  //-  não funciona 100%
                //                handler.PlatformView.BorderThickness = new Thickness(0).ToPlatform();
                //#endif

            });
        }


    }
}
