using WogView;

internal class Program {
    private static void Main(string[] args) {
        using (ViewWindow vw = new ViewWindow()){
            vw.Run();
        }
    }  
}