import java.io.Console;

public class Start {
    public static void main(String[] args) {
        Console console = System.console();
        if(console == null) {
            System.err.println("No Console");
            System.exit(1);
        }
        MainData mainData = new MainData();
        mainData.run(console);
    }
}
