using Gtk;

namespace Fase1.src.gui
{
    public abstract class MyWindow : Window
    {
        private readonly Window? _contextParent;
        public MyWindow(string title, Window? contextParent = null) : base(title) { _contextParent = contextParent; }
        public void AplicarEstilos()
        {
            CssProvider cssProvider = new CssProvider();

            try
            {
                cssProvider.LoadFromPath("src/gui/styles/styles.css");
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

        public void OnDeleteEvent()
        {
            _contextParent?.ShowAll();
            Destroy();
        }
    }
}