using Gtk;

namespace Fase1.src.gui
{
    public abstract class MyWindow : Window
    {
        public MyWindow(string title) : base(title) { }
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
    }
}