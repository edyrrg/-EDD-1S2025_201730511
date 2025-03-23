using System;
using Gtk;

namespace Fase2.src.views {
    public abstract class CustomWindow : Window
    {
        private readonly Window? _contextParent;
        public CustomWindow(string title, Window? contextParent = null) : base(title) { _contextParent = contextParent; }
        public void AplicarEstilos()
        {
            CssProvider cssProvider = new CssProvider();

            try
            {
                cssProvider.LoadFromPath("src/views/styles/styles.css");
                //Console.WriteLine("CSS cargado exitosamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al cargar CSS: {e.Message}");
            }

            StyleContext.AddProviderForScreen(
                Gdk.Screen.Default,
                cssProvider,
                (uint)StyleProviderPriority.Application
            );
        }

        public virtual void OnDeleteEvent()
        {
            _contextParent?.ShowAll();
            Destroy();
        }

        public void PopError(string txtError)
        {
            var dialogError = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, $"{txtError}");
            dialogError.Run();
            dialogError.Destroy();
        }

        public void PopSucess(string txtSuccess)
        {
            var dialogSuccess = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, $"{txtSuccess}");
            dialogSuccess.Run();
            dialogSuccess.Destroy();
        }
    }
}